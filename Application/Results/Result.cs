using Application.Results.Abstractions;
using System.Threading.Tasks;

namespace Application.Results
{
    #region >>> Result <<<
    public class Result : IResult
    {

        #region >>> IResult => Implementation <<<
        public string Message { get; set; }
        public bool Succeeded { get; set; }

        #endregion

        #region >>> Failed Result <<<
        public bool Failed => !Succeeded;

        public static IResult Fail()
        {
            return new Result { Succeeded = false };
        }
        public static IResult Fail(string message)
        {
            return new Result { Succeeded = false, Message = message };
        }

        public static Task<IResult> FailAsync()
        {
            return Task.FromResult(Fail());
        }

        public static Task<IResult> FailAsync(string message)
        {
            return Task.FromResult(Fail(message));
        }
        #endregion

        #region >>> Success Result <<<

        public static IResult Success()
        {
            return new Result { Succeeded = true };
        }

        public static IResult Success(string message)
        {
            return new Result { Succeeded = true, Message = message };
        }

        public static Task<IResult> SuccessAsync()
        {
            return Task.FromResult(Success());
        }

        public static Task<IResult> SuccessAsync(string message)
        {
            return Task.FromResult(Success(message));
        }
        #endregion

    }
    #endregion

    #region >>> Result<T> <<<
    public class Result<T> : Result, IResult<T>
    {
        #region >>> Properties <<<
        public T Data { get; set; }

        #endregion

        #region >>> Failed Result <<<
        public static new Result<T> Fail()
        {
            return new Result<T> { Succeeded = false };
        }

        public static new Result<T> Fail(string message)
        {
            return new Result<T> { Succeeded = false, Message = message };
        }

        public static new Task<Result<T>> FailAsync()
        {
            return Task.FromResult(Fail());
        }

        public static new Task<Result<T>> FailAsync(string message)
        {
            return Task.FromResult(Fail(message));
        }

        #endregion

        #region >>> Success Result <<<

        public static new Result<T> Success()
        {
            return new Result<T> { Succeeded = true };
        }

        public static new Result<T> Success(string message)
        {
            return new Result<T> { Succeeded = true, Message = message };
        }

        public static Result<T> Success(T data)
        {
            return new Result<T> { Succeeded = true, Data = data };
        }

        public static Result<T> Success(T data, string message)
        {
            return new Result<T> { Succeeded = true, Data = data, Message = message };
        }

        public static new Task<Result<T>> SuccessAsync()
        {
            return Task.FromResult(Success());
        }

        public static new Task<Result<T>> SuccessAsync(string message)
        {
            return Task.FromResult(Success(message));
        }

        public static Task<Result<T>> SuccessAsync(T data)
        {
            return Task.FromResult(Success(data));
        }

        public static Task<Result<T>> SuccessAsync(T data, string message)
        {
            return Task.FromResult(Success(data, message));
        }
        #endregion
    }
    #endregion
}
