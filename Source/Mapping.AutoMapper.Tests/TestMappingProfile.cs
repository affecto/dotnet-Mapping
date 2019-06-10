using AutoMapper;

namespace Affecto.Mapping.AutoMapper.Tests
{
    internal class TestMappingProfile1 : MappingProfile<Class1, Class2>
    {
        protected override void ConfigureMapping(IMappingExpression<Class1, Class2> map)
        {
        }
    }

    internal class TestMappingProfile2 : MappingProfile<Class2, Class1>
    {
        protected override void ConfigureMapping(IMappingExpression<Class2, Class1> map)
        {
        }
    }

    public class TestMappingWithParametersProfile : MappingProfile<Class1, Class3>
    {
        protected override void ConfigureMapping(IMappingExpression<Class1, Class3> map)
        {
            map.ForMember(dest => dest.PropFromParameter, opt => opt.ResolveUsing((src, dest, destMember, context) => context.Items["Parameter1"]));
        }
    }

    public class Class1
    {
        public string Prop { get; set; }
    }

    public class Class2
    {
        public string Prop { get; set; }
    }

    public class Class3
    {
        public string Prop { get; set; }
        public string PropFromParameter { get; set; }
    }
}