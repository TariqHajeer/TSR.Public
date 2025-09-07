namespace Application.Dtos.Common;

public record class BrandDto
{
    public int Id { get; set; }
    public string NameAr { get; set; }
    public string NameEn { get; set; }
    public string ExistingImagePath { get; set; }
}
