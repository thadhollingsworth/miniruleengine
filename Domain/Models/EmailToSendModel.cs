
using RuleEngineCoordinator.Domain.Enum;

namespace RuleEngineCoordinator.Domain.Models
{
    public class EmailToSendModel
    {
        public string To { get; set; }
        public string From { get; set; }
        public EmailTemplate Template { get; set; }
    }
}
