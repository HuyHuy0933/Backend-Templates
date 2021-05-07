using System;

namespace Infrastructure.ResultUtil
{
	public class Result<T>
	{
		public bool Failure
		{
			get => !Success;
		}

		public bool Success { get; }
		public T Data { get; }
		public int ResultCount { get; set; }
		public Error Error { get; }
		protected Result(T data, bool success, Error error)
		{
			Data = data;
			Success = success;
			Error = error;
		}

		protected Result(T data, bool success, Error error, int resultCount)
		{
			Data = data;
			Success = success;
			Error = error;
			ResultCount = resultCount;
		}

		public static Result<T> Ok(T data) => new Result<T>(data, true, default);
		public static Result<T> Ok(T data, int resultCount) => new Result<T>(data, true, default, resultCount);
		public static Result<T> Fail(Error err) => new Result<T>(default, false, err);

		public static implicit operator Result<T>(Error err) => new Result<T>(default, false, err);
	}

	public class ListResult<T> : Result<T>
	{
		public ListResult(T data, bool success, Error error, int resultCount) : base(data, success, error)
		{
			ResultCount = resultCount;
		}

		public int ResultCount { get; }

		public static ListResult<T> Ok(T data, int resultCount) => new ListResult<T>(data, true, default, resultCount);
	}
}