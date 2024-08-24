namespace Application.Banner.CommandAndQueries;

public class CreateBannerCommand
{
    public string Link { get; set; }
    public IFormFile ImageFile { get; set; }
    public BannerPosition Position { get; set; }
}