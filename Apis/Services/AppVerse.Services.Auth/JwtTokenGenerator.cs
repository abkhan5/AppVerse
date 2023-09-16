

namespace AppVerse.Service.Authentication;

internal class JwtTokenGenerator
{
    private readonly AppVerseDbContext _context;
    private readonly IOptions<JwtSettings> jwtSettingsOption;

    public JwtTokenGenerator(IOptions<JwtSettings> jwtSettingsOption, AppVerseDbContext context)
    {
        _context = context;
        this.jwtSettingsOption = jwtSettingsOption;
    }

    public async Task<AuthenticationResponseModel> GenerateToken(ClaimsPrincipal claimsPrincipal, string profileId,
        CancellationToken cancellationToken)
    {
        var jwtSettings = jwtSettingsOption.Value;

        var key = Encoding.UTF8.GetBytes(jwtSettings.Secret);
        var expiryDateTime = DateTime.UtcNow.Add(jwtSettings.TokenLifetime);

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(claimsPrincipal.Claims),
            IssuedAt = DateTime.UtcNow,
            Issuer = jwtSettingsOption.Value.Issuer,
            Audience = jwtSettingsOption.Value.Audience,
            Expires = expiryDateTime,
            SigningCredentials =
                new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        };
        var tokenHandler = new JwtSecurityTokenHandler();
        var token = tokenHandler.CreateToken(tokenDescriptor);
        var refreshToken = new RefreshToken
        {
            JwtId = token.Id,
            ProfileId = profileId,
            CreationDate = DateTime.UtcNow,
            ExpiryDate = DateTime.UtcNow.AddMonths(6)
        };

        await _context.Set<RefreshToken>().AddAsync(refreshToken, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);

        return new AuthenticationResponseModel
        {
            Token = tokenHandler.WriteToken(token),
            RefreshToken = refreshToken.Token
        };
    }
}