using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Affecto.Mapping.AutoMapper.Tests
{
    [TestClass]
    public class MapperFactoryTests
    {
        private MapperFactory sut;

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void CannotInstantiateFactoryWithoutProfiles()
        {
            sut = new MapperFactory(new List<MappingProfile>());
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void CannotInstantiateFactoryWithNullProfiles()
        {
            sut = new MapperFactory((MappingProfile) null);
        }

        [TestMethod]
        public void RegisteredMappersAreCreated()
        {
            sut = new MapperFactory(new TestMappingProfile1(), new TestMappingProfile2());

            IMapper<Class1, Class2> mapper1 = sut.Create<Class1, Class2>();
            IMapper<Class2, Class1> mapper2 = sut.Create<Class2, Class1>();

            Assert.IsNotNull(mapper1);
            Assert.IsNotNull(mapper2);
            Assert.IsInstanceOfType(mapper1, typeof(TestMappingProfile1.Mapper));
            Assert.IsInstanceOfType(mapper2, typeof(TestMappingProfile2.Mapper));
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void UnregisteredMappersAreNotCreated()
        {
            sut = new MapperFactory(new TestMappingProfile2());

            sut.Create<Class1, Class2>();
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void CannotRegisterMultipleMappersForSameTypes()
        {
            sut = new MapperFactory(new TestMappingProfile1(), new TestMappingProfile1());

            sut.Create<Class1, Class2>();
        }
    }
}