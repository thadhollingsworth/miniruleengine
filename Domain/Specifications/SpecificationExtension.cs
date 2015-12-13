using RuleEngineCoordinator.Domain.Models.Chain;

namespace RuleEngineCoordinator.Domain.Specification
{
    public static class SpecificationExtension
    {
        public static ISpecification<TContextModel> And<TContextModel>(
            this ISpecification<TContextModel> left,
            ISpecification<TContextModel> right)
            where TContextModel : ChainContextModelBase
        {
            return new AndSpecification<TContextModel>(left, right);
        }

        public static ISpecification<TContextModel> Or<TContextModel>(
            this ISpecification<TContextModel> left,
            ISpecification<TContextModel> right)
            where TContextModel : ChainContextModelBase
        {
            return new OrSpecification<TContextModel>(left, right);
        }

        public static ISpecification<TContextModel> Not<TContextModel>(this ISpecification<TContextModel> inner)
            where TContextModel : ChainContextModelBase
        {
            return new NotSpecification<TContextModel>(inner);
        }
    }
}
