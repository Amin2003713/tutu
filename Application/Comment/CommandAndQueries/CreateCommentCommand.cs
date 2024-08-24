namespace Application.Comment.CommandAndQueries;

public class CreateCommentCommand 
{
    public long UserId { get; set; }
    public long ProductId { get; set; }
    public string Text { get; set; }
    public string Disadvantages { get; set; }
    public string Advantages { get; set; }
    public UserRecommendedStatus UserRecommendedStatus { get; set; }
    public decimal Rate { get; set; } = 0;

};