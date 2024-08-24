namespace Application.Comment.CommandAndQueries;

public record EditCommentCommand(long CommentId, string Text, long UserId);