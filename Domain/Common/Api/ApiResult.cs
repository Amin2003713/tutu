namespace Domain.Common.Api;

public class ApiResult
{
    public bool IsSuccess { get; set; }
    public MetaData MetaData { get; set; }
}

public class ApiResult<TData>
{
    public bool IsSuccess { get; set; }
    public TData Data { get; set; }
    public MetaData MetaData { get; set; }
}