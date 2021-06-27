using Infrastructure.ListUtil.Extensions.IQueryable;
using Infrastructure.ResultUtil;
using LinqKit;
using MediatR;
using Project.Application.Models.Responses.Modules;
using Project.Core.Entities;
using Project.Core.Helpers;
using System;
using System.Collections.Generic;
using System.Text;

namespace Project.Application.Models.Requests.Modules
{
	public class GetModuleListRequest : BaseListRequest, IRequest<Result<GetModuleListResponse>>
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
