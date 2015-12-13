using RuleEngineCoordinator.Domain.DomainServices;

namespace RuleEngineCoordinator.Services.Email
{
    public class EmailDomainService : IEmailDomainService
    {
        public void Save(Domain.Models.EmailToSendModel model)
        {
            //don't need to do anything just for testing.
        }
    }
}
