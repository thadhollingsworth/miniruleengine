using System.Linq;
using RuleEngineCoordinator.Chain;
using RuleEngineCoordinator.Domain.Models.Chain;
using RuleEngineCoordinator.Domain.DomainServices;
using RuleEngineCoordinator.Services.Email;

namespace RuleEngineCoordinator.Services.Hold.Links
{
    public class HoldExceptionFinalLink : ILinkHandler<HoldChainModel>
    {
        private ILinkHandler<HoldChainModel> _nextHandler;
        private IEmailDomainService _emailDomainService;

        public ILinkHandler<HoldChainModel> NextHandler
        {
            set { _nextHandler = value; }
        }

        public IEmailDomainService EmailDomainService
        {
            get
            {
                if (_emailDomainService == null)
                {
                    _emailDomainService = new EmailDomainService();
                }
                return _emailDomainService;
            }
            set { _emailDomainService = value; }
        }

        public HoldChainModel ProcessLink(HoldChainModel model)
        {
            if (Handles(model))
            {
                model.EmailModels.ToList().ForEach(o => EmailDomainService.Save(o));
                model.LinkProcessed = Order;
                return _nextHandler != null ? _nextHandler.ProcessLink(model) : model;
            }
            return model;
        }

        public bool Handles(HoldChainModel model)
        {
            return true;
        }

        public int Order
        {
            get { return 4; }
        }
    }
}
