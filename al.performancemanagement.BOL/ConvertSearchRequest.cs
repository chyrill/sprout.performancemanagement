using al.performancemanagement.DAL;
using al.performancemanagement.DAL.Helpers;
using Microsoft.Data.OData.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web.Http.OData.Query;

namespace al.performancemanagement.BOL
{
    public class ConvertSearchRequest<TModel,TData>
    {
        public void ConvertToDataSearchRequest(SearchAllRequest<TModel> request, SearchAllRequest<TData> result)
        {
            if (request.Filter != null)
                result.Filter = request.Filter.RemapForType<TModel, TData, bool>();

            if (request.Sort != null && request.Sort.Count() > 0)
            {
                List<SortExpression<TData>> sortList = new List<SortExpression<TData>>();
                foreach (var sortExpression in request.Sort.ToList())
                {
                    var sort = sortExpression.Sort.RemapForType<TModel, TData, object>();
                    sortList.Add(new SortExpression<TData>(sort, sortExpression.IsAscending));
                }
                result.Sort = sortList.ToArray();
            }
        }

        public SearchRequest<TModel> ConvertToSearchRequest<TModel>(ODataQueryOptions<TModel> queryOptions) where TModel : class
        {
            SearchRequest<TModel> request = new SearchRequest<TModel>();
            request.Filter = ConvertToFilterExpression<TModel>(queryOptions.Filter);

            if (queryOptions.OrderBy != null)
            {
                request.Sort = ConvertToSortExpression<TModel>(queryOptions.OrderBy);
            }

            if (queryOptions.Top != null && queryOptions.Top.Value > 0)
            {
                request.PageSize = queryOptions.Top.Value;
            }

            if (queryOptions.Skip != null && queryOptions.Skip.Value > 0)
            {
                request.PageIndex = queryOptions.Skip.Value / request.PageSize;
            }

            return request;
        }

        public Expression<Func<T, bool>> ConvertToFilterExpression<T>(FilterQueryOption filter) where T : class
        {
            var enumerable = Enumerable.Empty<T>().AsQueryable();
            var param = Expression.Parameter(typeof(T));
            if (filter != null)
            {
                enumerable = (IQueryable<T>)filter.ApplyTo(enumerable, new ODataQuerySettings()
                {
                    HandleNullPropagation = HandleNullPropagationOption.False,
                    EnsureStableOrdering = false,
                });

                var mce = enumerable.Expression as MethodCallExpression;
                if (mce != null)
                {
                    var quote = mce.Arguments[1] as UnaryExpression;
                    if (quote != null)
                    {
                        return quote.Operand as Expression<Func<T, bool>>;
                    }
                }
            }
            return null;
        }

        public SortExpression<T>[] ConvertToSortExpression<T>(OrderByQueryOption orderBy) where T : class
        {
            if (orderBy == null)
                return null;

            var param = Expression.Parameter(typeof(T));
            var sortExpressions = new List<SortExpression<T>>();
            var parameters = orderBy.RawValue.Split(',');

            Expression field = null;
            var nodeCtr = 0;
            foreach (var p in parameters)
            {
                var raw = p.Replace(" asc", "").Replace(" desc", "");
                var props = raw.Split('/');

                if (props.Length < 2)
                {
                    field = Expression.Property(param, raw);
                }
                else
                {
                    field = param;
                    foreach (var sub in props)
                    {
                        field = Expression.Property(field, sub);
                    }
                }
                var converted = Expression.Lambda<Func<T, object>>(Expression.Convert(field, typeof(object)), param);
                sortExpressions.Add(new SortExpression<T>(converted, orderBy.OrderByNodes[nodeCtr].Direction == OrderByDirection.Ascending));
                nodeCtr++;
            }

            return sortExpressions.ToArray();
        }
    }
}
