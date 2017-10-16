﻿using AutoMapper;
using System;
using System.Diagnostics.CodeAnalysis;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace al.performancemanagement.DAL
{
    public class AutoMapVisitor<TSource, TDestination> :ExpressionVisitor
    {
        private readonly ParameterExpression _newParameter;
        private readonly TypeMap _typeMap = Mapper.FindTypeMapFor<TSource, TDestination>();

        /// <summary>
        /// Initialises a new instance of the <see cref="AutoMapVisitor{TSource, TDestination}"/> class.
        /// </summary>
        /// <param name="newParameter">The new <see cref="ParameterExpression"/> to access.</param>
        public AutoMapVisitor(ParameterExpression newParameter)
        {
            Contract.Requires(newParameter != null);

            _newParameter = newParameter;
            //Contract.Assume(_typeMap != null);
        }

        [ContractInvariantMethod]
        [SuppressMessage("Microsoft.Performance", "CA1822:MarkMembersAsStatic", Justification = "Required for code contracts.")]
        private void ObjectInvariant()
        {
            Contract.Invariant(_typeMap != null);
            Contract.Invariant(_newParameter != null);
        }

        /// <summary>
        /// Visits the children of the <see cref="T:System.Linq.Expressions.MemberExpression"/>.
        /// </summary>
        /// <returns>
        /// The modified expression, if it or any subexpression was modified; otherwise, returns the original expression.
        /// </returns>
        /// <param name="node">The expression to visit.</param>
        protected override Expression VisitMember(MemberExpression node)
        {
            var propertyMaps = _typeMap.GetPropertyMaps();
            Contract.Assume(propertyMaps != null);

            // Find any mapping for this member
            var propertyMap = propertyMaps.SingleOrDefault(map => map.SourceMember == node.Member);
            if (propertyMap == null)
                return base.VisitMember(node);

            var destinationProperty = propertyMap.DestinationProperty;

            Contract.Assume(destinationProperty != null);
            var destinationMember = destinationProperty.MemberInfo;

            Contract.Assume(destinationMember != null);

            // Check the new member is a property too
            var property = destinationMember as PropertyInfo;
            if (property == null)
                return base.VisitMember(node);

            // Access the new property
            var newPropertyAccess = Expression.Property(_newParameter, property);
            return base.VisitMember(newPropertyAccess);
        }

        protected override Expression VisitUnary(UnaryExpression node)
        {
            if (node.NodeType == ExpressionType.Convert && node.Operand.Type.BaseType.Name == typeof(Enum).Name)
            {
                return Visit(node.Operand);
            }

            return base.VisitUnary(node);
        }
    }
}
