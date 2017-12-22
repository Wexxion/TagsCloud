using System;
using System.Collections.Generic;
using TagsCloud.TextAnalyzing;

namespace TagsCloud.Infrastructure
{
    public class None
    {
        private None() {}
    }

    public struct Result<T>
    {
        public Result(string error, Exception exception = null, T value = default(T))
        {
            Error = error;
            Value = value;
            Exception = exception;
        }
        public static implicit operator Result<T>(T v) => Result.Ok(v);

        public string Error { get; }
        internal T Value { get; }
        public Exception Exception { get; }
        public T GetValueOrThrow()
        {
            if (IsSuccess) return Value;
            throw new InvalidOperationException($"Error: {Error}\nException: {Exception}");
        }
        public bool IsSuccess => Error == null;
    }

    public static class Result
    {
        public static Result<T> AsResult<T>(this T value) => Ok(value);

        public static Result<T> Ok<T>(T value) => new Result<T>(null, null, value);

        public static Result<None> Ok() => Ok<None>(null);

        public static Result<T> Fail<T>(string error, Exception e=null) => new Result<T>(error, e);

        public static Result<T> Of<T>(Func<T> f, string error = null)
        {
            try
            {
                return Ok(f());
            }
            catch (Exception e)
            {
                return Fail<T>(error, e);
            }
        }

        public static Result<None> OfAction(Action f, string error = null)
        {
            try
            {
                f();
                return Ok();
            }
            catch (Exception e)
            {
                return Fail<None>(error, e);
            }
        }

        public static Result<TOutput> Then<TInput, TOutput>(this Result<TInput> input, 
            Func<TInput, TOutput> continuation) => input.Then(inp => Of(() => continuation(inp)));

        public static Result<None> Then<TInput>(this Result<TInput> input,
            Action<TInput> continuation) => input.Then(inp => OfAction(() => continuation(inp)));

        public static Result<TOutput> Then<TInput, TOutput>(this Result<TInput> input,
            Func<TInput, Result<TOutput>> continuation) => input.IsSuccess
            ? continuation(input.Value)
            : Fail<TOutput>(input.Error, input.Exception);

        public static Result<TInput> OnFail<TInput>(this Result<TInput> input,
            Action<string> handleError)
        {
            if (!input.IsSuccess) handleError(input.Error);
            return input;
        }

        public static Result<TInput> ReplaceError<TInput>(this Result<TInput> input,
            Func<string, string> replaceError) => input.IsSuccess ? input : Fail<TInput>(replaceError(input.Error), input.Exception);

        public static Result<TInput> RefineError<TInput>(this Result<TInput> input,
            string errorMessage) => input.ReplaceError(err => errorMessage + err);
    }
}