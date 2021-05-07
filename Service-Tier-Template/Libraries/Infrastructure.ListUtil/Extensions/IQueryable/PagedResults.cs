using System.Collections.Generic;

namespace Infrastructure.ListUtil.Extensions.IQueryable
{
	public class PagedResults<TViewModel>
	{
		public PagedResults(
			IEnumerable<TViewModel> results,
			int resultCount)
		{
			ResultCount = resultCount;
			Results = results;
		}

		public int ResultCount { get; }
		public IEnumerable<TViewModel> Results { get; }
	}

	public static class PagedResultsExtensions
	{
		public static PagedResults<TViewModel> ToPagedResults<TViewModel>(
			this IEnumerable<TViewModel> results,
			int resultCount)
		{
			return new PagedResults<TViewModel>(
				results,
				resultCount);
		}
	}
}
