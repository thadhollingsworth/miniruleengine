using RuleEngineCoordinator.Chain;
using RuleEngineCoordinator.Domain.Enum;
using RuleEngineCoordinator.Domain.Models;
using RuleEngineCoordinator.Domain.Models.Chain;
using RuleEngineCoordinator.Domain.Specifications.HoldException;

namespace RuleEngineCoordinator.Services.Hold.Links
{
    public class DeleteDuplicateBeforeCutoffTimeSpecificationChainLink : ILinkHandler<HoldChainModel>
    {
        private ILinkHandler<HoldChainModel> _nextHandler;
        private DeleteDuplicateBeforeCutoffTimeSpecification _spec;

        public DeleteDuplicateBeforeCutoffTimeSpecification DeleteDuplicateBeforeCutoffTimeSpecification
        {
            get
            {
                if (_spec == null)
                {
                    _spec = new DeleteDuplicateBeforeCutoffTimeSpecification();
                }
                return _spec;
            }
            set
            {
                { _spec = value; }
            }
        }

        public ILinkHandler<HoldChainModel> NextHandler
        {
            set { _nextHandler = value; }
        }

        public HoldChainModel ProcessLink(HoldChainModel model)
        {
            if (Handles(model))
            {
                model.EmailModels.Add(new EmailToSendModel
                                      {
                                          From = "from1@from1.com",
                                          To = "to1@to.com",
                                          Template = EmailTemplate.HoldTemplate1
                                      });
                model.LinkProcessed = Order;
                return model;
            }

            return _nextHandler != null ? _nextHandler.ProcessLink(model) : model;
        }

        public bool Handles(HoldChainModel model)
        {
            return (DeleteDuplicateBeforeCutoffTimeSpecification.IsSatisfiedBy(model));
        }

        public int Order
        {
            get { return 1; }
        }
    }
}
