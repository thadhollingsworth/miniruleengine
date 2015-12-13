using RuleEngineCoordinator.Chain;
using RuleEngineCoordinator.Domain.Models.Chain;

namespace RuleEngineCoordinator.Services.Order
{
    public class OrderChainManager : ChainManager<OrderChainModel>
    {
        public OrderChainManager(IGenerateOrderChainLinks chainLinkGenerator) : base(chainLinkGenerator)
        {
        }
    }
}
