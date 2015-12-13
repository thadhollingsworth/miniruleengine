using RuleEngineCoordinator.Domain.Models.Chain;
using System;
using System.Linq.Expressions;

namespace RuleEngineCoordinator.Domain.Specification
{
    public class NotSpecification<TContextModel> : SpecificationBase<TContextModel>
        where TContextModel : ChainContextModelBase
    {
        private readonly ISpecification<TContextModel> _expression;

        public NotSpecification(ISpecification<TContextModel> expression)
        {
            _expression = expression;
        }

        public override Expression<Func<TContextModel, bool>> SpecExpression
        {
            get
            {
                var objParam = Expression.Parameter(typeof(TContextModel), "obj");

                var newExpr = Expression.Lambda<Func<TContextModel, bool>>(
                    Expression.Not(
                        Expression.Invoke(_expression.SpecExpression, objParam)
                    ),
                    objParam
                );

                return newExpr;
            }
        }
    }
}
