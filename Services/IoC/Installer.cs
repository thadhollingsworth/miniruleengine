using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using RuleEngineCoordinator.Chain;
using Castle.MicroKernel.Resolvers.SpecializedResolvers;
using RuleEngineCoordinator.Domain.DomainServices;
using RuleEngineCoordinator.Services.Hold;
using RuleEngineCoordinator.Services.Email;
using RuleEngineCoordinator.Domain.Specifications.HoldException;
using RuleEngineCoordinator.Domain.Specification;
using RuleEngineCoordinator.Domain.Models.Chain;

namespace RuleEngineCoordinator.Services.IoC
{
    public class Installer : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Kernel.Resolver.AddSubResolver(new CollectionResolver(container.Kernel));

            //Register Misc. supporting Services
            container.Register(
                Component.For<IEmailDomainService>()
                    .ImplementedBy<EmailDomainService>()
                    .LifeStyle
                    .Transient
            );

            //Register Specs
            container.Register(Classes.FromThisAssembly()
                .BasedOn(typeof(ISpecification<>))
                .WithServiceAllInterfaces()
                .LifestyleTransient());

            //Register Links
            container.Register(Classes.FromThisAssembly()
                .BasedOn(typeof(ILinkHandler<>))
                .WithServiceAllInterfaces()
                .LifestyleTransient());
            
            //Register Link Generators
            container.Register(
                Component.For<IGenerateHoldExceptionChainLinks>()
                    .ImplementedBy<HoldExceptionLinkGenerator>()
                    .LifeStyle
                    .Transient
            );

            container.Register(
                Component.For<IGenerateOrderChainLinks>()
                    .ImplementedBy<OrderLinkGenerator>()
                    .LifeStyle
                    .Transient
            );

            //Register Managers
            container.Register(Classes.FromThisAssembly()
                .BasedOn(typeof(IChainManager<>))
                .WithServiceAllInterfaces()
                .LifestyleTransient());
            


        }
    }
}
