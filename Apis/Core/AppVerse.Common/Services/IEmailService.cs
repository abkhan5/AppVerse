using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppVerse.Services;

public interface IEmailService
{
    Task Send(SendEmailDto email, IEnumerable<string> profileIds, CancellationToken cancellationToken);
    Task Send(SendEmailDto mail, IEnumerable<SenderMetadata> profiles, CancellationToken cancellationToken);
}

public record SendEmailDto
{
    public ShortEmaillSubjectDto Subject { get; set; }

    public ShortEmailMessageDto EmailMessages { get; set; }
    public ShortEmailActionLinkDto Link { get; set; }

    public string EmailTemplateId { get; set; }
    public string EmailTemplateCode { get; set; }
}

public record CustomEmailDto : SendEmailDto
{
    public const string ShortUpdateEmail = "shortUpdateEmail";
    public List<string> ProfileIds { get; set; }
    public IReadOnlyList<SenderMetadata> ReceiverEmailIds { get; set; }
}
public record SenderMetadata
{
    public string EmailID { get; set; }

    public string DisplayName { get; set; }
}

public record ShortEmailMessageDto
{
    public string MainMessage { get; set; }
    public string SecondaryMessage { get; set; }
    public string FooterMessage { get; set; }
    public string MessageReason { get; set; }
}
public record ShortEmailActionLinkDto(string Link, string Title = null);

public record ShortEmaillSubjectDto(string Preview, string Title);