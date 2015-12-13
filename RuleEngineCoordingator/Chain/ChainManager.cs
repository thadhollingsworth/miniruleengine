using RuleEngineCoordinator.Domain.Models.Chain;
using System;
using System.Collections.Generic;
using System.Linq;

namespace RuleEngineCoordinator.Chain
{
    public abstract class ChainManager<TContextModel> : IChainManager<TContextModel>
        where TContextModel : ChainContextModelBase
    {
        private readonly IGenerateChainLinks<TContextModel> _chainLinkGenerator;

        public ChainManager(IGenerateChainLinks<TContextModel> chainLinkGenerator)
        {
            _chainLinkGenerator = chainLinkGenerator;
        }

        private ILinkHandler<TContextModel> _nextHandler;

        protected virtual void Validate(IList<ILinkHandler<TContextModel>> explicitHandlers)
        {
            if (explicitHandlers.Count.Equals(0))
            {
                throw new ArgumentException(string.Format("No IChainHandler<{0}> found in executing assembly",
                    typeof(TContextModel).Name));
            }

            for (var iInstance = 0; iInstance < explicitHandlers.Count; iInstance++)
            {
                if (explicitHandlers.Skip(iInstance + 1).Any(o => o.Order.Equals(explicitHandlers[iInstance].Order)))
                {
                    throw new ArgumentException(
                        string.Format("Duplicate Order: {0}",
                            explicitHandlers[iInstance].Order), "explicitHandlers");
                }
            }

        }

        protected virtual IList<ILinkHandler<TContextModel>> GenerateChainLinks()
        {
            return _chainLinkGenerator.GenerateChainLinks();
        }

        public virtual TContextModel ProcessLinks(TContextModel chainContextModel)
        {
            var explicitHandlers = GenerateChainLinks();

            Validate(explicitHandlers);

            explicitHandlers = explicitHandlers.OrderBy(o => o.Order).ToList();

            for (var iLinkInstance = 0; iLinkInstance < explicitHandlers.Count() - 1; iLinkInstance++)
            {
                var chainHandler = explicitHandlers[iLinkInstance];

                if (chainHandler != null)
                {
                    chainHandler.NextHandler = (explicitHandlers[iLinkInstance + 1]);
                }
            }
            NextHandler = explicitHandlers[0];
            chainContextModel = ProcessLink(chainContextModel);
            return chainContextModel;
        }

        public ILinkHandler<TContextModel> NextHandler
        {
            set { _nextHandler = value; }
        }

        public IList<ILinkHandler<TContextModel>> Links
        {
            get
            {
                return _chainLinkGenerator.GenerateChainLinks();
            }
        }

        public virtual TContextModel ProcessLink(TContextModel model)
        {
            return _nextHandler != null ? _nextHandler.ProcessLink(model) : model;
        }


    }
}
