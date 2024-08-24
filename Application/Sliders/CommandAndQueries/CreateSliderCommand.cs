namespace Application.Sliders.CommandAndQueries;

public class CreateSliderCommand
{
    public string Link { get; set; }
    public IFormFile ImageFile { get; set; }
    public string Title { get; set; }
}