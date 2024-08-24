namespace Application.Comment.CommandAndQueries;

public record ChangeCommentStatusCommand(long Id, CommentStatus Status) ;