using Infrastructure.ListUtil.Extensions.IQueryable;
using LinqKit;
using System;
using System.Collections.Generic;
using System.Text;
using Project.Core.Entities;
using Project.Core.Helpers;

namespace Project.Application.Models.Request.Modules
{
	public class GetModuleListRequest : BaseListRequest
	{

	}

    public static class ModuleListRequestExtensions
    {
        private static readonly FilterPredicate<Module, GetModuleListRequest>[] FILTERS = new[] {
      new FilterPredicate<Module, GetModuleListRequest>(
        isApplicable: input => input.searchText != null,
        predicate: (p, input) =>
          EntityExpressions.HasValueAndMatches.Invoke(p.Name, input.searchText)
      )
    };

        private static readonly List<string> DEFAULT_SORT_CRITERIA = new List<string>() {
            "Id".AsAscendingSorter()
        };

        private static readonly OrderByString<Module> ORDER_BY_STRING = new OrderByString<Module>
        {
            ["Name"] = p => p.Name
        };

        public static IFilterSortPageConfig<Module, GetModuleListRequest, int> AsFilterSortPageConfig(this GetModuleListRequest input) =>
          new FilterSortPageConfigBuilder<Module, GetModuleListRequest, int>(
            primaryKeySelector: n => n.Id,
            pageSize: input.pageSize,
            pageNumber: input.pageNumber)
            .WithFiltering(FILTERS)
            .WithSorting(
                sortCriteria: input.sortCriteria ?? DEFAULT_SORT_CRITERIA,
                orderByStringKeySelectors: ORDER_BY_STRING
                )
            .Build();
    }
}
