using RuleEngineCoordinator.Domain.Models.Chain;
using System;
using System.Linq.Expressions;

namespace RuleEngineCoordinator.Domain.Specification
{
    public abstract class SpecificationBase<TContextModel> : ISpecification<TContextModel>
        where TContextModel : ChainContextModelBase
    {
        private Func<TContextModel, bool> _compiledExpression;

        private Func<TContextModel, bool> CompiledExpression
        {
            get { return _compiledExpression ?? (_compiledExpression = SpecExpression.Compile()); }
        }

        public abstract Expression<Func<TContextModel, bool>> SpecExpression { get; }

        public virtual bool IsSatisfiedBy(TContextModel obj)
        {
            return CompiledExpression(obj);
        }
    }
}
