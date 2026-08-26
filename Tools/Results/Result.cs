using System;
using System.Collections.Generic;
using System.Text;

namespace Tools.Results
{
    public class Result
    {
        public static implicit operator Result(Error error)
        {
            return Failure(error);
        }

        public static Result Success()
        {
            return new Result(true, Error.None);
        }

        public static Result Failure(Error error)
        {
            return new Result(false, error);
        }

        public bool IsSuccess { get; }
        public bool IsFailure => !IsSuccess;
        public Error Error { get; }

        private Result(bool isSuccess, Error error)
        {
            IsSuccess = isSuccess;
            Error = error;
        }
    }

    public class Result<TResult>
    {
        public static implicit operator Result<TResult>(Error error)
        {
            return Failure(error);
        }

        public static Result<TResult> Success(TResult data)
        {
            return new Result<TResult>(true, data, Error.None);
        }

        public static Result<TResult> Failure(Error error)
        {
            return new Result<TResult>(false, default!, error);
        }

        public bool IsSuccess { get; }
        public bool IsFailure => !IsSuccess;
        public Error Error { get; }
        public TResult Data { get; }

        private Result(bool isSuccess, TResult data, Error error)
        {
            IsSuccess = isSuccess;
            Error = error;
            Data = data;
        }
    }
}
