namespace Application.Comment.CommandAndQueries;

public record DeleteCommentCommand(long CommentId, long UserId) ;