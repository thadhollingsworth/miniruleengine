
namespace RuleEngineCoordinator.Domain.Models.Chain
{
    public class OrderChainModel : ChainContextModelBase
    {
        public int Id { get; set; }
        public string DecisionPivot { get; set; }
    }
}
