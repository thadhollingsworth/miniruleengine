﻿using RuleEngineCoordinator.Chain;
using RuleEngineCoordinator.Domain.Enum;
using RuleEngineCoordinator.Domain.Models;
using RuleEngineCoordinator.Domain.Models.Chain;
using RuleEngineCoordinator.Domain.Specifications.HoldException;

namespace RuleEngineCoordinator.Services.Hold.Links
{
    public class SomeOtherReasonSpecificationChainLink : ILinkHandler<HoldChainModel>
    {
        private ILinkHandler<HoldChainModel> _nextHandler;

        public ILinkHandler<HoldChainModel> NextHandler
        {
            set { _nextHandler = value; }
        }

        private SomeOtherReasonSpecification _someOtherReasonSpecification;

        public SomeOtherReasonSpecification SomeOtherReasonSpecification
        {
            get
            {
                if (_someOtherReasonSpecification == null)
                {
                    _someOtherReasonSpecification = new SomeOtherReasonSpecification();
                }
                return _someOtherReasonSpecification;
            }
            set
            {
                { _someOtherReasonSpecification = value; }
            }
        }

        public HoldChainModel ProcessLink(HoldChainModel model)
        {
            if (Handles(model))
            {
                model.EmailModels.Add(new EmailToSendModel
                {
                    From = "from2@from2.com",
                    To = "to2@to.com",
                    Template = EmailTemplate.HoldTemplate2
                });
                model.LinkProcessed = Order;
                return model;
            }

            return _nextHandler != null ? _nextHandler.ProcessLink(model) : model;

        }

        public bool Handles(HoldChainModel model)
        {
            var spec = SomeOtherReasonSpecification;
            return spec.IsSatisfiedBy(model);
        }

        public int Order
        {
            get { return 2; }
        }
    }
}
