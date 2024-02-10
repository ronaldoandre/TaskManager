namespace TaskManager.MVC.Mappers;
static class CommentMapper
{
    public static CommentEntity MapperTo(this CommentInsertDto comment)
    {
        return new()
        {
            Value = comment.Value,
        };
    }

    public static IList<CommentResponseDto> MapperTo(this IList<CommentEntity> comments)
    {
        return comments.Select(comment => comment.MapperTo()).ToList();
    }

    public static CommentResponseDto MapperTo(this CommentEntity comment)
    {
        return new()
        {
            Id = comment.Id,
            Value = comment.Value,
        };
    }

    public static CommentEntity MapperTo(this CommentUpdateDto comment)
    {
        return new()
        {
            Id = comment.Id,
            Value = comment.Value,
        };
    }
}
