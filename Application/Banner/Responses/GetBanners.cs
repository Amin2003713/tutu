namespace Application.Banner.Responses;

public record GetBannerResponses(long Id, DateTime CreationDate, string Link, string ImageName, int Position);
