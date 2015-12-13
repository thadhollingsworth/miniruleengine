using RuleEngineCoordinator.Chain;
using RuleEngineCoordinator.Domain.Models.Chain;

namespace RuleEngineCoordinator.Services.Hold
{
    public class ExceptionHoldChainManger : ChainManager<HoldChainModel>
    {
        public ExceptionHoldChainManger(IGenerateHoldExceptionChainLinks chainLinkGenerator) : base(chainLinkGenerator)
        {
        }

    }
}
