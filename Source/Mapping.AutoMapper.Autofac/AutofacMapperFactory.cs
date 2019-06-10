using System;
using Autofac;

namespace Affecto.Mapping.AutoMapper.Autofac
{
    public class AutofacMapperFactory : IMapperFactory
    {
        protected readonly IComponentContext componentContext;

        public AutofacMapperFactory(IComponentContext componentContext)
        {
            this.componentContext = componentContext ?? throw new ArgumentNullException(nameof(componentContext));
        }

        public virtual IMapper<TSource, TDestination> Create<TSource, TDestination>()
        {
            return componentContext.Resolve<IMapper<TSource, TDestination>>();
        }
    }
}