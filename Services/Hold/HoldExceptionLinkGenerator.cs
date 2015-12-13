using System.Collections.Generic;
using System.Linq;
using RuleEngineCoordinator.Chain;
using RuleEngineCoordinator.Domain.Models.Chain;

namespace RuleEngineCoordinator.Services.Hold
{
    public class HoldExceptionLinkGenerator : IGenerateHoldExceptionChainLinks
    {
        private readonly IList<ILinkHandler<HoldChainModel>> _links;

        public HoldExceptionLinkGenerator(IList<ILinkHandler<HoldChainModel>> links)
        {
            _links = links;
        }

        public IList<ILinkHandler<HoldChainModel>> GenerateChainLinks()
        {
            return _links.ToList();
        }
    }
}
