using RuleEngineCoordinator.Domain.Models.Chain;
using System.Collections.Generic;

namespace RuleEngineCoordinator.Chain
{
    public interface IGenerateChainLinks<TContextModel>
        where TContextModel : ChainContextModelBase
    {
        IList<ILinkHandler<TContextModel>> GenerateChainLinks();
    }
}
