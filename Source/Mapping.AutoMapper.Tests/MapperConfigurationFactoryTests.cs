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
    }
}