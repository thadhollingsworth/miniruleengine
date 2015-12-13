using RuleEngineCoordinator.Domain.Enum;
using System;
using System.Collections.Generic;

namespace RuleEngineCoordinator.Domain.Models.Chain
{
    public class HoldChainModel : ChainContextModelBase
    {
        public HoldChainModel()
        {
            EmailModels = new List<EmailToSendModel>();
        }

        public ExceptionHoldAction ActionPerformed { get; set; }
        public ExceptionHoldExceptionType ExceptionType { get; set; }
        public DateTime CutoffTime { get; set; }
        public IList<EmailToSendModel> EmailModels { get; set; }
        public int LinkProcessed { get; set; }
    }
}
