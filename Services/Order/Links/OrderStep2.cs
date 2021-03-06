﻿using RuleEngineCoordinator.Chain;
using RuleEngineCoordinator.Domain.Models.Chain;
using System;

namespace RuleEngineCoordinator.Services.Order.Links
{
    public class OrderStep2 : ILinkHandler<OrderChainModel>
    {
        private ILinkHandler<OrderChainModel> _nextHandler;

        public ILinkHandler<OrderChainModel> NextHandler
        {
            set { _nextHandler = value; }
        }

        public int Order
        {
            get
            {
                return 3;
            }
        }

        public bool Handles(OrderChainModel model)
        {
            throw new NotImplementedException();
        }

        public OrderChainModel ProcessLink(OrderChainModel model)
        {
            throw new NotImplementedException();
        }
    }
}
