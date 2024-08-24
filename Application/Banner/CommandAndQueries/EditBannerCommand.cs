namespace Application.Banner.CommandAndQueries;

public class EditBannerCommand
{
    public long Id { get; set; }
    public string Link { get; set; }
    public IFormFile? ImageFile { get; set; }
    public BannerPosition Position { get; set; }
}