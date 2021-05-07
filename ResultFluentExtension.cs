using SalaryCalculator.Core.Framework;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SalaryCalculator.Core.Common.Extensions
{
	// Apply fluent pattern
	public static class ResultFluentExtension
	{
		public static Result<R> Map<T, R>(this Result<T> result, Func<Result<R>> func)
		{
			return result.Failure ? result.Error : func();
		}

		public static Result<R> Map<T, R>(this Result<T> result, Func<T, R> func)
		{
			return result.Failure ? result.Error : Result<R>.Ok(func(result.Data));
		}

		public static async Task<Result<R>> MapAsync<T, R>(this Result<T> result, Func<T, Task<R>> func)
		{
			return result.Failure ? result.Error : Result<R>.Ok((await func(result.Data)));
		}

		public static async Task<Result<R>> MapAsync<T, R>(this Task<Result<T>> resultTask, Func<T, R> func)
		{
			var result = await resultTask;
			return result.Failure ? result.Error : Result<R>.Ok(func(result.Data));
		}

		public static async Task<Result<R>> MapAsync<T, R>(this Result<T> result, Func<T, Task<Result<R>>> func)
		{
			if (result.Failure) return result.Error;
			var funcResult = await func(result.Data);
			return funcResult.Failure ? funcResult.Error : funcResult;
		}

		public static async Task<Result<R>> MapAsync<T, R>(this Task<Result<T>> resultTask, Func<T, Task<Result<R>>> func)
		{
			var result = await resultTask;
			if (result.Failure) return result.Error;
			var funcResult = await func(result.Data);
			return funcResult.Failure ? funcResult.Error : funcResult;
		}

		// return source result
		public static async Task<Result<T>> ExecuteAsync<T, R>(this Result<T> result, Func<T, Task<Result<R>>> func)
		{
			if (result.Failure) return result.Error;
			var funcResult = await func(result.Data);
			return funcResult.Failure ? funcResult.Error : result;
		}

		public static async Task<Result<T>> ExecuteAsync<T, R>(this Task<Result<T>> resultTask, Func<T, Task<Result<R>>> func)
		{
			var result = await resultTask;
			if (result.Failure) return result.Error;
			var funcResult = await func(result.Data);
			return funcResult.Failure ? funcResult.Error : result;
		}
	}
}
