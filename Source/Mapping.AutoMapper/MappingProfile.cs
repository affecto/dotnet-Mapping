using System;
using AutoMapper;

namespace Affecto.Mapping.AutoMapper
{
    public abstract class MappingProfile : Profile
    {
    }

    public abstract class MappingProfile<TSource, TDestination> : MappingProfile, IMappingProfile<TSource, TDestination>
    {
        protected MappingProfile()
        {
            IMappingExpression<TSource, TDestination> map = CreateMap<TSource, TDestination>();
            ConfigureMapping(map);
        }

        public virtual IMapper<TSource, TDestination> CreateMapper(IMapper mapper)
        {
            return new Mapper(mapper);
        }

        protected abstract void ConfigureMapping(IMappingExpression<TSource, TDestination> map);

        public class Mapper : IMapper<TSource, TDestination>
        {
            protected readonly IMapper mapper;

            public Mapper(IMapper mapper)
            {
                this.mapper = mapper ?? throw new ArgumentNullException("mapper");
            }

            public virtual TDestination Map(TSource source)
            {
                return mapper.Map<TSource, TDestination>(source);
            }

            public virtual TDestination Map(TSource source, params (string parameterName, object parameterValue)[] parameters)
            {
                return mapper.Map<TSource, TDestination>(source, opt =>
                {
                    if (parameters != null)
                    {
                        foreach ((string parameterName, object parameterValue) parameter in parameters)
                        {
                            if (!string.IsNullOrWhiteSpace(parameter.parameterName) && parameter.parameterValue != null)
                            {
                                opt.Items.Add(parameter.parameterName, parameter.parameterValue);
                            }
                        }
                    }
                });
            }
        }
    }
}