using Microsoft.VisualStudio.TestTools.UnitTesting;
using RuleEngineCoordinator.Chain;
using RuleEngineCoordinator.Domain.Models.Chain;
using RuleEngineCoordinator.Services.Hold.Links;
using RuleEngineCoordinator.Services.IoC;
using RuleEngineCoordinator.Services.Order.Links;
using System.Linq;

namespace RuleEngineCoordinator.UnitTests.ServicesTests
{
    [TestClass]
    public class ExceptionHoldChainManagerIoCTests
    {
        [TestMethod]
        public void VerifyOrderManager()
        {
            var bs = new WindsorBootstrapper();
            var container = bs.BootstrapContainer();
            var resolved = container.Resolve<IChainManager<OrderChainModel>>();
            var links = resolved.Links;

            Assert.IsTrue(links.Count == 2);

            Assert.IsTrue(links.Any(o => o.GetType().Equals(typeof(OrderStep1))));
            Assert.IsTrue(links.Any(o => o.GetType().Equals(typeof(OrderStep2))));

        }

        [TestMethod]
        public void VerifyHoldChainManger()
        {
            var bs = new WindsorBootstrapper();
            var container = bs.BootstrapContainer();
            var resolved = container.Resolve<IChainManager<HoldChainModel>>();

            Assert.IsTrue(resolved.Links.Count == 4);

            var deleteDupLink = resolved.Links.Where(o => o.Order == 1).FirstOrDefault();
            Assert.IsNotNull(deleteDupLink);
            var castedDeleteDupLink = deleteDupLink as DeleteDuplicateBeforeCutoffTimeSpecificationChainLink;
            Assert.IsNotNull(castedDeleteDupLink);
            Assert.IsNotNull(castedDeleteDupLink.DeleteDuplicateBeforeCutoffTimeSpecification);

            var someotherLink = resolved.Links.Where(o => o.Order == 2).FirstOrDefault();
            Assert.IsNotNull(someotherLink);
            var castedsomeotherLink = someotherLink as SomeOtherReasonSpecificationChainLink;
            Assert.IsNotNull(castedsomeotherLink);
            Assert.IsNotNull(castedsomeotherLink.SomeOtherReasonSpecification);

            var comboLink = resolved.Links.Where(o => o.Order == 3).FirstOrDefault();
            Assert.IsNotNull(comboLink);
            var castedcomboLink = comboLink as HoldExceptionCombinationLink;
            Assert.IsNotNull(castedcomboLink);
            Assert.IsNotNull(castedcomboLink.DeleteDuplicateBeforeCutoffTimeSpecification);
            Assert.IsNotNull(castedcomboLink.SomeOtherReasonSpecification);

            var finalLink = resolved.Links.Where(o => o.Order == 4).FirstOrDefault();
            Assert.IsNotNull(finalLink);
            var castedfinalLink = finalLink as HoldExceptionFinalLink;
            Assert.IsNotNull(castedfinalLink);
            Assert.IsNotNull(castedfinalLink.EmailDomainService);

        }
    }
}
