using System;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Linq.Expressions;

namespace al.performancemanagement.DAL
{
    public static class ExpressionExtensions
    {
        public static Expression<Func<TDestination, TResult>> RemapForType<TSource, TDestination, TResult>(this Expression<Func<TSource, TResult>> expression)
        {

            Contract.Requires(expression != null);
            Contract.Ensures(Contract.Result<Expression<Func<TDestination, TResult>>>() != null);

            var newParameter = Expression.Parameter(typeof(TDestination));

            Contract.Assume(newParameter != null);
            var visitor = new AutoMapVisitor<TSource, TDestination>(newParameter);
            var remappedBody = visitor.Visit(expression.Body);
            if (remappedBody == null)
            {
                var msg = "Unable to remap expression";
                throw new InvalidOperationException(msg);
            }

            return Expression.Lambda<Func<TDestination, TResult>>(remappedBody, newParameter);


        }

        public static IOrderedQueryable<T> ObjectSort<T>(this IQueryable<T> entities, Expression<Func<T, object>> expression, bool isAscending, bool isOrdered)
        {

            var unaryExpression = expression.Body as UnaryExpression;
            if (unaryExpression != null)
            {
                var propertyExpression = unaryExpression.Operand as MemberExpression;
                if (propertyExpression != null)
                {
                    var parameters = expression.Parameters;
                    if (propertyExpression.Type == typeof(string))
                    {
                        var newExpression = Expression.Lambda<Func<T, string>>(propertyExpression, parameters);
                        if (isOrdered)
                        {
                            return isAscending ? (entities as IOrderedQueryable<T>).ThenBy(newExpression) : (entities as IOrderedQueryable<T>).ThenByDescending(newExpression);
                        }
                        else
                        {
                            return isAscending ? entities.OrderBy(newExpression) : entities.OrderByDescending(newExpression);
                        }
                    }
                    if (propertyExpression.Type == typeof(bool))
                    {
                        var newExpression = Expression.Lambda<Func<T, bool>>(propertyExpression, parameters);
                        if (isOrdered)
                        {
                            return isAscending ? (entities as IOrderedQueryable<T>).ThenBy(newExpression) : (entities as IOrderedQueryable<T>).ThenByDescending(newExpression);
                        }
                        else
                        {
                            return isAscending ? entities.OrderBy(newExpression) : entities.OrderByDescending(newExpression);
                        }
                    }
                    if (propertyExpression.Type == typeof(bool?))
                    {
                        var newExpression = Expression.Lambda<Func<T, bool?>>(propertyExpression, parameters);
                        if (isOrdered)
                        {
                            return isAscending ? (entities as IOrderedQueryable<T>).ThenBy(newExpression) : (entities as IOrderedQueryable<T>).ThenByDescending(newExpression);
                        }
                        else
                        {
                            return isAscending ? entities.OrderBy(newExpression) : entities.OrderByDescending(newExpression);
                        }
                    }
                    if (propertyExpression.Type == typeof(byte))
                    {
                        var newExpression = Expression.Lambda<Func<T, byte>>(propertyExpression, parameters);
                        if (isOrdered)
                        {
                            return isAscending ? (entities as IOrderedQueryable<T>).ThenBy(newExpression) : (entities as IOrderedQueryable<T>).ThenByDescending(newExpression);
                        }
                        else
                        {
                            return isAscending ? entities.OrderBy(newExpression) : entities.OrderByDescending(newExpression);
                        }
                    }
                    if (propertyExpression.Type == typeof(byte?))
                    {
                        var newExpression = Expression.Lambda<Func<T, byte?>>(propertyExpression, parameters);
                        if (isOrdered)
                        {
                            return isAscending ? (entities as IOrderedQueryable<T>).ThenBy(newExpression) : (entities as IOrderedQueryable<T>).ThenByDescending(newExpression);
                        }
                        else
                        {
                            return isAscending ? entities.OrderBy(newExpression) : entities.OrderByDescending(newExpression);
                        }
                    }
                    if (propertyExpression.Type == typeof(sbyte))
                    {
                        var newExpression = Expression.Lambda<Func<T, sbyte>>(propertyExpression, parameters);
                        if (isOrdered)
                        {
                            return isAscending ? (entities as IOrderedQueryable<T>).ThenBy(newExpression) : (entities as IOrderedQueryable<T>).ThenByDescending(newExpression);
                        }
                        else
                        {
                            return isAscending ? entities.OrderBy(newExpression) : entities.OrderByDescending(newExpression);
                        }
                    }
                    if (propertyExpression.Type == typeof(sbyte?))
                    {
                        var newExpression = Expression.Lambda<Func<T, sbyte?>>(propertyExpression, parameters);
                        if (isOrdered)
                        {
                            return isAscending ? (entities as IOrderedQueryable<T>).ThenBy(newExpression) : (entities as IOrderedQueryable<T>).ThenByDescending(newExpression);
                        }
                        else
                        {
                            return isAscending ? entities.OrderBy(newExpression) : entities.OrderByDescending(newExpression);
                        }
                    }
                    if (propertyExpression.Type == typeof(char))
                    {
                        var newExpression = Expression.Lambda<Func<T, char>>(propertyExpression, parameters);
                        if (isOrdered)
                        {
                            return isAscending ? (entities as IOrderedQueryable<T>).ThenBy(newExpression) : (entities as IOrderedQueryable<T>).ThenByDescending(newExpression);
                        }
                        else
                        {
                            return isAscending ? entities.OrderBy(newExpression) : entities.OrderByDescending(newExpression);
                        }
                    }
                    if (propertyExpression.Type == typeof(char?))
                    {
                        var newExpression = Expression.Lambda<Func<T, char?>>(propertyExpression, parameters);
                        if (isOrdered)
                        {
                            return isAscending ? (entities as IOrderedQueryable<T>).ThenBy(newExpression) : (entities as IOrderedQueryable<T>).ThenByDescending(newExpression);
                        }
                        else
                        {
                            return isAscending ? entities.OrderBy(newExpression) : entities.OrderByDescending(newExpression);
                        }
                    }
                    if (propertyExpression.Type == typeof(decimal))
                    {
                        var newExpression = Expression.Lambda<Func<T, decimal>>(propertyExpression, parameters);
                        if (isOrdered)
                        {
                            return isAscending ? (entities as IOrderedQueryable<T>).ThenBy(newExpression) : (entities as IOrderedQueryable<T>).ThenByDescending(newExpression);
                        }
                        else
                        {
                            return isAscending ? entities.OrderBy(newExpression) : entities.OrderByDescending(newExpression);
                        }
                    }
                    if (propertyExpression.Type == typeof(decimal?))
                    {
                        var newExpression = Expression.Lambda<Func<T, decimal?>>(propertyExpression, parameters);
                        if (isOrdered)
                        {
                            return isAscending ? (entities as IOrderedQueryable<T>).ThenBy(newExpression) : (entities as IOrderedQueryable<T>).ThenByDescending(newExpression);
                        }
                        else
                        {
                            return isAscending ? entities.OrderBy(newExpression) : entities.OrderByDescending(newExpression);
                        }
                    }
                    if (propertyExpression.Type == typeof(double))
                    {
                        var newExpression = Expression.Lambda<Func<T, double>>(propertyExpression, parameters);
                        if (isOrdered)
                        {
                            return isAscending ? (entities as IOrderedQueryable<T>).ThenBy(newExpression) : (entities as IOrderedQueryable<T>).ThenByDescending(newExpression);
                        }
                        else
                        {
                            return isAscending ? entities.OrderBy(newExpression) : entities.OrderByDescending(newExpression);
                        }
                    }
                    if (propertyExpression.Type == typeof(double?))
                    {
                        var newExpression = Expression.Lambda<Func<T, double?>>(propertyExpression, parameters);
                        if (isOrdered)
                        {
                            return isAscending ? (entities as IOrderedQueryable<T>).ThenBy(newExpression) : (entities as IOrderedQueryable<T>).ThenByDescending(newExpression);
                        }
                        else
                        {
                            return isAscending ? entities.OrderBy(newExpression) : entities.OrderByDescending(newExpression);
                        }
                    }
                    if (propertyExpression.Type == typeof(float))
                    {
                        var newExpression = Expression.Lambda<Func<T, float>>(propertyExpression, parameters);
                        if (isOrdered)
                        {
                            return isAscending ? (entities as IOrderedQueryable<T>).ThenBy(newExpression) : (entities as IOrderedQueryable<T>).ThenByDescending(newExpression);
                        }
                        else
                        {
                            return isAscending ? entities.OrderBy(newExpression) : entities.OrderByDescending(newExpression);
                        }
                    }
                    if (propertyExpression.Type == typeof(float?))
                    {
                        var newExpression = Expression.Lambda<Func<T, float?>>(propertyExpression, parameters);
                        if (isOrdered)
                        {
                            return isAscending ? (entities as IOrderedQueryable<T>).ThenBy(newExpression) : (entities as IOrderedQueryable<T>).ThenByDescending(newExpression);
                        }
                        else
                        {
                            return isAscending ? entities.OrderBy(newExpression) : entities.OrderByDescending(newExpression);
                        }
                    }
                    if (propertyExpression.Type == typeof(int))
                    {
                        var newExpression = Expression.Lambda<Func<T, int>>(propertyExpression, parameters);
                        if (isOrdered)
                        {
                            return isAscending ? (entities as IOrderedQueryable<T>).ThenBy(newExpression) : (entities as IOrderedQueryable<T>).ThenByDescending(newExpression);
                        }
                        else
                        {
                            return isAscending ? entities.OrderBy(newExpression) : entities.OrderByDescending(newExpression);
                        }
                    }
                    if (propertyExpression.Type == typeof(int))
                    {
                        var newExpression = Expression.Lambda<Func<T, int?>>(propertyExpression, parameters);
                        if (isOrdered)
                        {
                            return isAscending ? (entities as IOrderedQueryable<T>).ThenBy(newExpression) : (entities as IOrderedQueryable<T>).ThenByDescending(newExpression);
                        }
                        else
                        {
                            return isAscending ? entities.OrderBy(newExpression) : entities.OrderByDescending(newExpression);
                        }
                    }
                    if (propertyExpression.Type == typeof(uint))
                    {
                        var newExpression = Expression.Lambda<Func<T, uint>>(propertyExpression, parameters);
                        if (isOrdered)
                        {
                            return isAscending ? (entities as IOrderedQueryable<T>).ThenBy(newExpression) : (entities as IOrderedQueryable<T>).ThenByDescending(newExpression);
                        }
                        else
                        {
                            return isAscending ? entities.OrderBy(newExpression) : entities.OrderByDescending(newExpression);
                        }
                    }
                    if (propertyExpression.Type == typeof(uint))
                    {
                        var newExpression = Expression.Lambda<Func<T, uint?>>(propertyExpression, parameters);
                        if (isOrdered)
                        {
                            return isAscending ? (entities as IOrderedQueryable<T>).ThenBy(newExpression) : (entities as IOrderedQueryable<T>).ThenByDescending(newExpression);
                        }
                        else
                        {
                            return isAscending ? entities.OrderBy(newExpression) : entities.OrderByDescending(newExpression);
                        }
                    }
                    if (propertyExpression.Type == typeof(long))
                    {
                        var newExpression = Expression.Lambda<Func<T, long>>(propertyExpression, parameters);
                        if (isOrdered)
                        {
                            return isAscending ? (entities as IOrderedQueryable<T>).ThenBy(newExpression) : (entities as IOrderedQueryable<T>).ThenByDescending(newExpression);
                        }
                        else
                        {
                            return isAscending ? entities.OrderBy(newExpression) : entities.OrderByDescending(newExpression);
                        }
                    }
                    if (propertyExpression.Type == typeof(long?))
                    {
                        var newExpression = Expression.Lambda<Func<T, long?>>(propertyExpression, parameters);
                        if (isOrdered)
                        {
                            return isAscending ? (entities as IOrderedQueryable<T>).ThenBy(newExpression) : (entities as IOrderedQueryable<T>).ThenByDescending(newExpression);
                        }
                        else
                        {
                            return isAscending ? entities.OrderBy(newExpression) : entities.OrderByDescending(newExpression);
                        }
                    }
                    if (propertyExpression.Type == typeof(ulong))
                    {
                        var newExpression = Expression.Lambda<Func<T, ulong>>(propertyExpression, parameters);
                        if (isOrdered)
                        {
                            return isAscending ? (entities as IOrderedQueryable<T>).ThenBy(newExpression) : (entities as IOrderedQueryable<T>).ThenByDescending(newExpression);
                        }
                        else
                        {
                            return isAscending ? entities.OrderBy(newExpression) : entities.OrderByDescending(newExpression);
                        }
                    }
                    if (propertyExpression.Type == typeof(ulong?))
                    {
                        var newExpression = Expression.Lambda<Func<T, ulong?>>(propertyExpression, parameters);
                        if (isOrdered)
                        {
                            return isAscending ? (entities as IOrderedQueryable<T>).ThenBy(newExpression) : (entities as IOrderedQueryable<T>).ThenByDescending(newExpression);
                        }
                        else
                        {
                            return isAscending ? entities.OrderBy(newExpression) : entities.OrderByDescending(newExpression);
                        }
                    }
                    if (propertyExpression.Type == typeof(short))
                    {
                        var newExpression = Expression.Lambda<Func<T, short>>(propertyExpression, parameters);
                        if (isOrdered)
                        {
                            return isAscending ? (entities as IOrderedQueryable<T>).ThenBy(newExpression) : (entities as IOrderedQueryable<T>).ThenByDescending(newExpression);
                        }
                        else
                        {
                            return isAscending ? entities.OrderBy(newExpression) : entities.OrderByDescending(newExpression);
                        }
                    }
                    if (propertyExpression.Type == typeof(short?))
                    {
                        var newExpression = Expression.Lambda<Func<T, short?>>(propertyExpression, parameters);
                        if (isOrdered)
                        {
                            return isAscending ? (entities as IOrderedQueryable<T>).ThenBy(newExpression) : (entities as IOrderedQueryable<T>).ThenByDescending(newExpression);
                        }
                        else
                        {
                            return isAscending ? entities.OrderBy(newExpression) : entities.OrderByDescending(newExpression);
                        }
                    }
                    if (propertyExpression.Type == typeof(ushort))
                    {
                        var newExpression = Expression.Lambda<Func<T, ushort>>(propertyExpression, parameters);
                        if (isOrdered)
                        {
                            return isAscending ? (entities as IOrderedQueryable<T>).ThenBy(newExpression) : (entities as IOrderedQueryable<T>).ThenByDescending(newExpression);
                        }
                        else
                        {
                            return isAscending ? entities.OrderBy(newExpression) : entities.OrderByDescending(newExpression);
                        }
                    }
                    if (propertyExpression.Type == typeof(ushort?))
                    {
                        var newExpression = Expression.Lambda<Func<T, ushort?>>(propertyExpression, parameters);
                        if (isOrdered)
                        {
                            return isAscending ? (entities as IOrderedQueryable<T>).ThenBy(newExpression) : (entities as IOrderedQueryable<T>).ThenByDescending(newExpression);
                        }
                        else
                        {
                            return isAscending ? entities.OrderBy(newExpression) : entities.OrderByDescending(newExpression);
                        }
                    }
                    if (propertyExpression.Type == typeof(DateTime))
                    {
                        var newExpression = Expression.Lambda<Func<T, DateTime>>(propertyExpression, parameters);
                        if (isOrdered)
                        {
                            return isAscending ? (entities as IOrderedQueryable<T>).ThenBy(newExpression) : (entities as IOrderedQueryable<T>).ThenByDescending(newExpression);
                        }
                        else
                        {
                            return isAscending ? entities.OrderBy(newExpression) : entities.OrderByDescending(newExpression);
                        }
                    }
                    if (propertyExpression.Type == typeof(DateTime?))
                    {
                        var newExpression = Expression.Lambda<Func<T, DateTime?>>(propertyExpression, parameters);
                        if (isOrdered)
                        {
                            return isAscending ? (entities as IOrderedQueryable<T>).ThenBy(newExpression) : (entities as IOrderedQueryable<T>).ThenByDescending(newExpression);
                        }
                        else
                        {
                            return isAscending ? entities.OrderBy(newExpression) : entities.OrderByDescending(newExpression);
                        }
                    }
                }
                else
                {
                    if (isOrdered)
                    {
                        return isAscending ? (entities as IOrderedQueryable<T>).ThenBy(expression) : (entities as IOrderedQueryable<T>).ThenByDescending(expression);
                    }
                    else
                    {
                        return isAscending ? entities.OrderBy(expression) : entities.OrderByDescending(expression);
                    }
                }


                //throw new NotSupportedException(string.Format("Object type resolution not implemented for {0}.", propertyExpression.Type.Name));
            }

            if (isOrdered)
            {
                return isAscending ? (entities as IOrderedQueryable<T>).ThenBy(expression) : (entities as IOrderedQueryable<T>).ThenByDescending(expression);
            }
            else
            {
                return isAscending ? entities.OrderBy(expression) : entities.OrderByDescending(expression);
            }

        }
    }
}
