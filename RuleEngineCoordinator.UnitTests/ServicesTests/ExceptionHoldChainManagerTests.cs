using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using RuleEngineCoordinator.Chain;
using System.Collections.Generic;
using System.Linq;
using System;
using RuleEngineCoordinator.Domain.Models.Chain;
using RuleEngineCoordinator.Services.Hold.Links;
using RuleEngineCoordinator.Services.Hold;
using RuleEngineCoordinator.Domain.DomainServices;
using RuleEngineCoordinator.Domain.Specifications.HoldException;
using System.Linq.Expressions;

namespace RuleEngineCoordinator.UnitTests.ServicesTests
{
    [TestClass]
    public class ExceptionHoldChainManagerTests
    {
        private Mock<IEmailDomainService> _mockedEmailDomainService;
        private Mock<IGenerateHoldExceptionChainLinks> _mockedLinkGenerator;

        private Mock<ILinkHandler<HoldChainModel>> _link1;
        private Mock<ILinkHandler<HoldChainModel>> _link2;
        private Mock<ILinkHandler<HoldChainModel>> _link3;
        private Mock<ILinkHandler<HoldChainModel>> _link4;

        private Mock<DeleteDuplicateBeforeCutoffTimeSpecification> _mockedSpecForDeleteLink;
        private Mock<SomeOtherReasonSpecification> _mockedSomeOtherSpec;


        [TestInitialize]
        public void Setup()
        {
            _mockedEmailDomainService = new Mock<IEmailDomainService>();
            _mockedLinkGenerator = new Mock<IGenerateHoldExceptionChainLinks>();

            _link1 = new Mock<ILinkHandler<HoldChainModel>>();
            _link2 = new Mock<ILinkHandler<HoldChainModel>>();
            _link3 = new Mock<ILinkHandler<HoldChainModel>>();
            _link4 = new Mock<ILinkHandler<HoldChainModel>>();

            _mockedSpecForDeleteLink = new Mock<DeleteDuplicateBeforeCutoffTimeSpecification>();
            _mockedSomeOtherSpec = new Mock<SomeOtherReasonSpecification>();
        }

        private IEmailDomainService MockedEmailDomainService
        {
            get { return _mockedEmailDomainService.Object; }
        }

        private IGenerateHoldExceptionChainLinks MockedLinkGenerator
        {
            get
            {
                return _mockedLinkGenerator.Object;
            }
        }

        private ExceptionHoldChainManger ClassUnderTest
        {
            get
            {
                var links = new List<ILinkHandler<HoldChainModel>>
                   {
                       _link1.Object,
                       _link2.Object,
                       _link3.Object,
                       _link4.Object
                   }.ToList();


                _mockedLinkGenerator.Setup(o => o.GenerateChainLinks())
                    .Returns(links);

                var classUnderTest = new ExceptionHoldChainManger(MockedLinkGenerator);

                return classUnderTest;
            }
        }

        private DeleteDuplicateBeforeCutoffTimeSpecificationChainLink DeleteLink
        {
            get
            {
                var link = new DeleteDuplicateBeforeCutoffTimeSpecificationChainLink();

                link.DeleteDuplicateBeforeCutoffTimeSpecification = 
                    _mockedSpecForDeleteLink.Object;

                return link;
            }
        }

        private HoldExceptionCombinationLink HoldComboLink
        {
            get
            {
                var link = new HoldExceptionCombinationLink();

                link.DeleteDuplicateBeforeCutoffTimeSpecification = _mockedSpecForDeleteLink.Object;

                link.SomeOtherReasonSpecification = _mockedSomeOtherSpec.Object;

                return link;
            }
        }

        private SomeOtherReasonSpecificationChainLink SomeOtherLink
        {
            get
            {
                var link = new SomeOtherReasonSpecificationChainLink();

                link.SomeOtherReasonSpecification =
                    _mockedSomeOtherSpec.Object as SomeOtherReasonSpecification;

                return link;
            }
        }

        private HoldExceptionFinalLink FinalLink
        {
            get
            {
                var link = new HoldExceptionFinalLink();
                return link;
            }
        }


        private ExceptionHoldChainManger ClassUnderTestWithRealLinks
        {
            get
            {
                var links = new List<ILinkHandler<HoldChainModel>>
                   {
                        DeleteLink,
                        HoldComboLink,
                        SomeOtherLink,
                        FinalLink
                   }.ToList();


                _mockedLinkGenerator.Setup(o => o.GenerateChainLinks())
                    .Returns(links);

                var classUnderTest = new ExceptionHoldChainManger(MockedLinkGenerator);

                return classUnderTest;
            }
        }


        [TestMethod]
        public void VerifyDuplicateLinkOrderThrowsError()
        {
            _link1.Setup(o => o.Order).Returns(1);
            _link2.Setup(o => o.Order).Returns(1);

            var classUnderTest = ClassUnderTest;
            var contextModel = new HoldChainModel();

            try
            {
                classUnderTest.ProcessLinks(contextModel);
            }
            catch (Exception excp)
            {
                Assert.IsTrue(excp.Message.Contains("Duplicate"));
            }

            _link1.Verify(o => o.ProcessLink(It.IsAny<HoldChainModel>()), Times.Never);
        }

        [TestMethod]
        public void VerifyFirstStepShortCircuits()
        {
            _mockedSpecForDeleteLink.Setup(o => o.IsSatisfiedBy(It.IsAny<HoldChainModel>())).Returns(true);

            var classUnderTest = ClassUnderTestWithRealLinks;
            var contextModel = new HoldChainModel();


            var model = classUnderTest.ProcessLinks(contextModel);

            Assert.IsTrue(model.LinkProcessed == 1);
        }

        
        [TestMethod]
        public void VerifyProcessesAllSteps()
        {
            var contextModel = new HoldChainModel();

            _mockedSpecForDeleteLink.Setup(o => o.IsSatisfiedBy(It.IsAny<HoldChainModel>())).Returns(false);
            _mockedSomeOtherSpec.Setup(o => o.IsSatisfiedBy(It.IsAny<HoldChainModel>())).Returns(false);


            Expression<Func<HoldChainModel, bool>> expression = srv => false;
            
            _mockedSpecForDeleteLink.Setup(o => o.SpecExpression).Returns(expression);
            _mockedSomeOtherSpec.Setup(o => o.SpecExpression).Returns(expression);


            var classUnderTest = ClassUnderTestWithRealLinks;
            var result = classUnderTest.ProcessLinks(contextModel);

            Assert.IsTrue(result.LinkProcessed == 4);

        }

    }
}
