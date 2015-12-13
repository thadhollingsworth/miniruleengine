using RuleEngineCoordinator.Domain.Models.Chain;

namespace RuleEngineCoordinator.Chain
{
    public interface ILinkHandler<TContextModel>
        where TContextModel : ChainContextModelBase
    {
        ILinkHandler<TContextModel> NextHandler { set; }
        TContextModel ProcessLink(TContextModel model);
        bool Handles(TContextModel model);
        int Order { get; }
    }
}
