# Mapping
* **Affecto.Mapping**
  * Interfaces and extension methods for one-way and two-way mappers.
  * NuGet: https://www.nuget.org/packages/Affecto.Mapping
* **Affecto.Mapping.AutoMapper**
  * Implementation for one-way and two-way mappers using AutoMapper profiles.
  * NuGet: https://www.nuget.org/packages/Affecto.Mapping.AutoMapper
* **Affecto.Mapping.AutoMapper.Autofac**
  * Extension methods for registering AutoMapper profiles to Autofac container and configuring AutoMapper to use them.
  * NuGet: https://www.nuget.org/packages/Affecto.Mapping.AutoMapper.Autofac

### Build status

[![Build status](https://ci.appveyor.com/api/projects/status/v99lxtuud9r3fvl7?svg=true)](https://ci.appveyor.com/project/affecto/dotnet-mapping)


## AutoMapper & Autofac code examples

#### Creating a basic mapping profile with out-of-the-box mapper implementation

```csharp
internal class PersonProfile : MappingProfile<IPerson, Person>
{
    protected override void ConfigureMapping(IMappingExpression<IPerson, Person> map)
    {
        // Properties with matching names are mapped automatically.
        map.ForMember(dest => dest.Id, opt => opt.MapFrom(source => source.Identifier));
        map.ForMember(dest => dest.HomeAddress, opt => opt.MapFrom(source => source.Address.Home));
    }
}
```

#### Creating a mapping profile with custom mapper implementation

```csharp
internal class PersonProfile : MappingProfile<IPerson, Person>
{
    public override IMapper<IPerson, Person> CreateMapper(IMapper mapper)
    {
        return new PersonMapper(mapper);
    }

    protected override void ConfigureMapping(IMappingExpression<IPerson, Person> map)
    {
        // Properties with matching names are mapped automatically.
        map.ForMember(dest => dest.Id, opt => opt.MapFrom(source => source.Identifier));
        map.ForMember(dest => dest.HomeAddress, opt => opt.MapFrom(source => source.Address.Home));
    }

    public class PersonMapper : Mapper
    {
        public PersonMapper(IMapper mapper)
            : base(mapper)
        {
        }

        public override Person Map(IPerson source)
        {
            // Perform complicated custom mapping here if it cannot be done with AutoMapper
            return base.Map(source);
        }
    }
}
```

#### Registering mapping profiles to Autofac container using extension method

```csharp
public class MappingModule : Module
{
    protected override void Load(ContainerBuilder builder)
    {
        base.Load(builder);
        builder.RegisterMappingProfile<PersonProfile, IPerson, Person>();
    }
}
```

#### Configuring AutoMapper to use registered mapping profiles

```csharp
public class ApplicationModule : Module
{
    protected override void Load(ContainerBuilder builder)
    {
        base.Load(builder);
        builder.ConfigureAutoMapper();
    }
}
```

#### Configuring AutoMapper to use registered mapping profiles with custom configuration

```csharp
public class ApplicationModule : Module
{
    protected override void Load(ContainerBuilder builder)
    {
        base.Load(builder);
        builder.ConfigureAutoMapper(new CustomMapperConfigurationFactory());
    }
}

internal class CustomMapperConfigurationFactory : MapperConfigurationFactory
{
    protected override void AddCustomConfiguration(IMapperConfigurationExpression configuration)
    {
        base.AddCustomConfiguration(configuration);
        configuration.DestinationMemberNamingConvention = new LowerUnderscoreNamingConvention();
    }
}
```