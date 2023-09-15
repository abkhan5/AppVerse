namespace AppVerse.Database.MsSql.Entities;
public record Country : BaseEntity<int>
{
    public string ISOCode { get; set; }
    public string Name { get; set; }
    public string Continents { get; set; }
    public string CapitalName { get; set; }
    public string CurrencyCode { get; set; }
    public string DialingCode { get; set; }

    public override int Id { get; set; }

    public string Code { get; set; }

    public string Description { get; set; }
    public DateTime CreatedOn { get; set; }

    public string ToolTip { get; set; }
    public string ImageUrl { get; set; }
}