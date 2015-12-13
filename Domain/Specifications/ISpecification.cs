using RuleEngineCoordinator.Domain.Models.Chain;
using System;
using System.Linq.Expressions;

namespace RuleEngineCoordinator.Domain.Specification
{
    public interface ISpecification<TContextModel>
        where TContextModel : ChainContextModelBase
    {
        Expression<Func<TContextModel, bool>> SpecExpression { get; }
        bool IsSatisfiedBy(TContextModel obj);
    }
}
