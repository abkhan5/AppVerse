namespace AppVerse.Service.AzureCommunicationSevice;

internal sealed class AzureEmailClientService
{
    private readonly EmailClient emailClient;
    private readonly ILogger logger;
    public AzureEmailClientService(EmailClient emailClient, ILogger<AzureEmailClientService> logger)
    {
        this.emailClient = emailClient;
        this.logger = logger;
    }

    public async Task SendEmail(string title, string htmlContent, EmailTemplateDto emailTemplate, IEnumerable<EmailAddress> receiverEmailIds, CancellationToken cancellationToken)
    {

        try
        {
            var recipientsEmails = new EmailRecipients(receiverEmailIds);
            var emailContent = new EmailContent(title)
            {
                Html = htmlContent
            };
            var emailMessage = new EmailMessage(emailTemplate.SenderEmailId, recipientsEmails, emailContent);
            emailMessage.ReplyTo.Add(new EmailAddress(emailTemplate.SenderEmailId, emailTemplate.SenderName));
            EmailSendOperation emailSendOperation = await emailClient.SendAsync(Azure.WaitUntil.Started, emailMessage, cancellationToken: cancellationToken);
            //EmailSendResult statusMonitor = emailSendOperation.Value;

            //Console.WriteLine($"Email Sent. Status = {emailSendOperation.Value.Status}");

            ///// Get the OperationId so that it can be used for tracking the message for troubleshooting
            //string operationId = emailSendOperation.Id;
            //Console.WriteLine($"Email operation id = {operationId}");
        }
        catch (RequestFailedException ex)
        {
            var message = $"Email send operation failed with error code: {ex.ErrorCode}, message: {ex.Message}";
            logger.LogCritical(ex, message);
        }
    }
}