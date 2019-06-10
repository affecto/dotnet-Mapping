using System.Reflection;
using AutoMapper;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Affecto.Mapping.AutoMapper.Tests
{
    [TestClass]
    public class MapperConfigurationFactoryTests
    {
        private MapperConfigurationFactory sut;

        [TestInitialize]
        public void Setup()
        {
            sut = new MapperConfigurationFactory();
        }

        [TestMethod]
        public void MapperIsConfiguredWithAssemblyProfiles()
        {
            TestMappingProfile1 testMappingProfile = new TestMappingProfile1();
            MapperConfiguration mapperConfiguration = sut.CreateMapperConfiguration(Assembly.GetExecutingAssembly());

            IMapper<Class1, Class2> mapper = testMappingProfile.CreateMapper(mapperConfiguration.CreateMapper());
            Class2 result = mapper.Map(new Class1 { Prop = "Value" });

            Assert.IsNotNull(mapper);
            Assert.IsNotNull(result);
            Assert.AreEqual("Value", result.Prop);
        }

        [TestMethod]
        public void MapperWithParametersIsConfiguredWithAssemblyProfiles()
        {
            TestMappingWithParametersProfile testMappingProfile = new TestMappingWithParametersProfile();
            MapperConfiguration mapperConfiguration = sut.CreateMapperConfiguration(Assembly.GetExecutingAssembly());

            IMapper<Class1, Class3> mapper = testMappingProfile.CreateMapper(mapperConfiguration.CreateMapper());
            Class3 result = mapper.Map(new Class1 { Prop = "Value" }, ("Parameter1", "Parameter1Value"));

            Assert.IsNotNull(mapper);
            Assert.IsNotNull(result);
            Assert.AreEqual("Value", result.Prop);
            Assert.AreEqual("Parameter1Value", result.PropFromParameter);
        }
    }

    public class TestMappingProfile : MappingProfile<Class1, Class2>
    {
        protected override void ConfigureMapping(IMappingExpression<Class1, Class2> map)
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
