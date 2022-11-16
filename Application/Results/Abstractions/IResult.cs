namespace Application.Results.Abstractions
{
    #region >>> IResult <<<

    public interface IResult
    {
        string Message { get; set; }

        bool Succeeded { get; set; }
    }
    #endregion

    #region >>> IResult<T> <<<
    public interface IResult<out T> : IResult
    {
        T Data { get; }
    }
    #endregion
}
