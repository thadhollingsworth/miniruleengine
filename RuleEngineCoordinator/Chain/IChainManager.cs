using RuleEngineCoordinator.Domain.Models.Chain;
using System.Collections.Generic;

namespace RuleEngineCoordinator.Chain
{
    public interface IChainManager<TContextModel>
        where TContextModel : ChainContextModelBase
    {
        TContextModel ProcessLink(TContextModel model);
        ILinkHandler<TContextModel> NextHandler { set; }
        IList<ILinkHandler<TContextModel>> Links { get; }
    }
}
