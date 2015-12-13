using RuleEngineCoordinator.Domain.Models.Chain;
using System;
using System.Linq.Expressions;

namespace RuleEngineCoordinator.Domain.Specification
{
    public class OrSpecification<TContextModel> : SpecificationBase<TContextModel>
        where TContextModel : ChainContextModelBase
    {
        private readonly ISpecification<TContextModel> _left;
        private readonly ISpecification<TContextModel> _right;

        public OrSpecification(ISpecification<TContextModel> left,
            ISpecification<TContextModel> right)
        {
            _left = left;
            _right = right;
        }

        public override Expression<Func<TContextModel, bool>> SpecExpression
        {
            get
            {
                var objParam = Expression.Parameter(typeof(TContextModel), "obj");

                var newExpr = Expression.Lambda<Func<TContextModel, bool>>(
                    Expression.OrElse(
                        Expression.Invoke(_left.SpecExpression, objParam),
                        Expression.Invoke(_right.SpecExpression, objParam)
                    ),
                    objParam
                );

                return newExpr;
            }
        }
    }
}
