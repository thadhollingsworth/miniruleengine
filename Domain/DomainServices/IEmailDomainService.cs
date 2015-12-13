using RuleEngineCoordinator.Domain.Models;

namespace RuleEngineCoordinator.Domain.DomainServices
{
    public interface IEmailDomainService
    {
        void Save(EmailToSendModel model);
    }
}
