namespace Infra.Comment;

public static class CommentRoutes
{
    public static string GetCommentsList = "api/Comment?";
    public static string GetProductComments = "api/Comment/productComments?";
    public static string GetCommentById = "api/Comment/";

    public static string PostComment = "api/Comment";
    public static string PutComment = "api/Comment";
    public static string PutCommentStatusChange = "api/Comment/changeStatus";

    public static string DeleteCommentById = "api/Comment/";
}