﻿namespace AppVerse.Service.Authentication;

public class JwtSettings
{
    public const string JwtOptionsName = "JwtSettings";
    public string Secret { get; set; }

    public string Issuer { get; set; }

    public string Audience { get; set; }
    public TimeSpan TokenLifetime { get; set; }
}

