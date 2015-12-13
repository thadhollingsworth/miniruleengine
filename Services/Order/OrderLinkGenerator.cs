using System.Collections.Generic;
using System.Linq;
using RuleEngineCoordinator.Chain;
using RuleEngineCoordinator.Domain.Models.Chain;

namespace RuleEngineCoordinator.Services.Hold
{
    public class OrderLinkGenerator : IGenerateOrderChainLinks
    {
        private readonly IList<ILinkHandler<OrderChainModel>> _links;

        public OrderLinkGenerator(IList<ILinkHandler<OrderChainModel>> links)
        {
            _links = links;
        }

        public IList<ILinkHandler<OrderChainModel>> GenerateChainLinks()
        {
            return _links.ToList();
        }
    }
}
