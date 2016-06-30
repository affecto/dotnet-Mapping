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
            TestMappingProfile testMappingProfile = new TestMappingProfile();
            MapperConfiguration mapperConfiguration = sut.CreateMapperConfiguration(Assembly.GetExecutingAssembly());
            IMapper<Class1, Class2> mapper = testMappingProfile.CreateMapper(mapperConfiguration.CreateMapper());

            Assert.IsNotNull(mapper);
        }
    }

    internal class TestMappingProfile : MappingProfile<Class1, Class2>
    {
        protected override void ConfigureMapping(IMappingExpression<Class1, Class2> map)
        {
        }
    }

    internal class Class1
    {
        public string Prop { get; set; }
    }

    internal class Class2
    {
        public string Prop { get; set; }
    }
}