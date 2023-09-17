

using AppVerse.Conference.MsSql.Entity;
using Microsoft.EntityFrameworkCore;

namespace AppVerse.Service.AzureCommunicationSevice;

internal sealed class AzureEmailService : IEmailService
{
    private readonly AzureEmailClientService client;
    private readonly IFileService fileService;
    private readonly DbContext context;
    public AzureEmailService( IFileService fileService, AzureEmailClientService client, DbContext context)
    {
        this.fileService = fileService;
        this.client = client;
        this.context = context;
    }
    async Task IEmailService.Send(SendEmailDto mail, IEnumerable<SenderMetadata> profiles, CancellationToken cancellationToken)
    {
        var templateId = mail.EmailTemplateId ?? mail.EmailTemplateCode;
        templateId = templateId.ToLower();
        EmailTemplateDto emailTemplate = new();
        string htmlFile = await GetHtml(mail, emailTemplate, cancellationToken);
        await client.SendEmail(mail.Subject.Title, htmlFile, emailTemplate, profiles.Select(item => new EmailAddress(item.EmailID, item.DisplayName)), cancellationToken);
    }

    async Task IEmailService.Send(SendEmailDto shortEmail, IEnumerable<string> profileIds, CancellationToken cancellationToken)
    {
        var templateId = shortEmail.EmailTemplateId ?? shortEmail.EmailTemplateCode;
        templateId = templateId.ToLower();
        EmailTemplateDto emailTemplate = new();
        string htmlFile = await GetHtml(shortEmail, emailTemplate, cancellationToken);
        await client.SendEmail(shortEmail.Subject.Title, htmlFile, emailTemplate, GetProfiles(profileIds), cancellationToken);
    }

    private async Task<string> GetHtml(SendEmailDto shortEmail, EmailTemplateDto emailTemplate, CancellationToken cancellationToken)
    {
        var htmlBytes = await fileService.GetFile(new BlobStorageItemDto
        {
            ContainerName = EmailTemplateDto.EmailContainer,
            ContentType = "text/html;charset=UTF-8",
            FileName = emailTemplate.EmailTemplatePath,
            Metadata = new Dictionary<string, string>
            {
                [EmailTemplateDto.TemplateIdField] = shortEmail.EmailTemplateId ?? shortEmail.EmailTemplateCode
            }
        }, cancellationToken);
        var htmlFile = Encoding.UTF8.GetString(htmlBytes);
        if (shortEmail.Link != null)
        {
            htmlFile = htmlFile.Replace(EmailTemplateDto.Link, shortEmail.Link.Link);
            htmlFile = htmlFile.Replace(EmailTemplateDto.LinkTitle, shortEmail.Link.Title);
        }

        if (shortEmail.EmailMessages != null)
        {
            htmlFile = htmlFile.Replace(EmailTemplateDto.mainMessage, shortEmail.EmailMessages.MainMessage);
            htmlFile = htmlFile.Replace(EmailTemplateDto.secondaryMessage, shortEmail.EmailMessages.SecondaryMessage);
            htmlFile = htmlFile.Replace(EmailTemplateDto.footerMessage, shortEmail.EmailMessages.FooterMessage);
        }

        return htmlFile;
    }

    private IEnumerable<EmailAddress> GetProfiles(IEnumerable<string> profileIds)
    {
        List<EmailAddress> profiles = new();
        foreach (var profileIdChunk in profileIds.Chunk(50))
        {

            var subProfileIds = profileIdChunk.ToList();
            var sqlProfiles = context.Set<AppVerseUser>().Where(x => subProfileIds.Contains(x.Id)).Select(item => new
            {
                item.Email,
                item.DisplayName
            });
            foreach (var profileItem in sqlProfiles)
                profiles.Add(new EmailAddress(profileItem.Email, profileItem.DisplayName));
        }
        return profiles;
    }
}
