using Castle.Windsor;
using Castle.Windsor.Installer;

namespace RuleEngineCoordinator.Services.IoC
{
    public class WindsorBootstrapper
    {
        public IWindsorContainer BootstrapContainer()
        {
            return new WindsorContainer()
                .Install(FromAssembly.This());
        }
    }
}
