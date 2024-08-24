namespace Application.Comment.Responses;

public class CommentDto : BaseDto
{
    public long ProductId { get; set; }
    public long UserId { get; set; }
    public string UserFullName { get; set; }
    public string ProductTitle { get; set; }
    public string Text { get; set; }
    public CommentStatus Status { get; set; }
    public decimal Rate { get; set; }
    public string[] Disadvantages { get; set; }
    public string[] Advantages { get; set; }
    public UserRecommendedStatus UserRecommendedStatus { get; set; }
}