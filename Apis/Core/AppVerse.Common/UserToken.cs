namespace AppVerse;

public record UserToken
{
    public UserToken(string createUserToken, bool isPhoneNumber)
    {
        CreateUserToken = createUserToken;
        IsPhoneNumber = isPhoneNumber;
    }
    public bool IsPhoneNumber { get; set; }
    public string CreateUserToken { get; set; }
}

public record EmailTemplateDto : BaseDto
{
    #region Email parser
    public const string Preview = "preview";
    public const string PreviewTitle = "previewtitle";
    public const string Link = "link";
    public const string LinkTitle = "{{link.title}}";
    public const string mainMessage = "{{emailMessages.mainMessage";
    public const string secondaryMessage = "{{emailMessages.secondaryMessage}}";
    public const string footerMessage = "{{emailMessages.footerMessage}}";
    public const string messageReason = "{{emailMessages.messageReason}}";
    #endregion

    public const string EmailContainer = "emailtemplates";
    public const string TemplateIdField = "templateid";
    public const string TemplateCodeField = "templatecode";
    public string EmailTemplatePath { get; set; }
    public string TemplateCode { get; set; }
    public string TemplateDescription { get; set; }
    public string SenderEmailId { get; set; }
    public string SenderName { get; set; }
    public string FileName { get; set; }
    public bool CanDelete { get; set; }

}
