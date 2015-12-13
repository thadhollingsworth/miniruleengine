using RuleEngineCoordinator.Domain.Enum;
using RuleEngineCoordinator.Domain.Models.Chain;
using RuleEngineCoordinator.Domain.Specification;
using System;
using System.Linq.Expressions;

namespace RuleEngineCoordinator.Domain.Specifications.HoldException
{
    public class DeleteDuplicateBeforeCutoffTimeSpecification : SpecificationBase<HoldChainModel>
    {
        public override Expression<Func<HoldChainModel, bool>> SpecExpression
        {
            get
            {
                return o => o.ActionPerformed.Equals(ExceptionHoldAction.Accept) &&
                            o.ExceptionType.Equals(ExceptionHoldExceptionType.ReasonA) &&
                            o.CutoffTime < DateTime.Now;
            }
        }
    }
}
