using RuleEngineCoordinator.Domain.Enum;
using RuleEngineCoordinator.Domain.Models.Chain;

namespace RuleEngineCoordinator.UnitTests.Builders
{
    public static class HoldChainModelBuilder
    {
        public static HoldChainModel Build()
        {
            return new HoldChainModel
                   {
                       ActionPerformed = ExceptionHoldAction.Accept,
                       ExceptionType = ExceptionHoldExceptionType.ReasonA
                   };
        }
    }
}
