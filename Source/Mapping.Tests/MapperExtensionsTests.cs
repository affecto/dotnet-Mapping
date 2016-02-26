using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;

namespace Affecto.Mapping.Tests
{
    [TestClass]
    public class MapperExtensionsTests
    {
        private ITwoWayMapper<string, int> mapper;
        private const string String1 = "Test source value 1";
        private const string String2 = "Test source value 2";
        private const int Int1 = 100;
        private const int Int2 = 200;
        
        [TestInitialize]
        public void Setup()
        {
            mapper = Substitute.For<ITwoWayMapper<string, int>>();
            mapper.Map(String1).Returns(Int1);
            mapper.Map(String2).Returns(Int2);
            mapper.Map(Int1).Returns(String1);
            mapper.Map(Int2).Returns(String2);
        }

        [TestMethod]
        public void MapNullSourceToEmptyListOneWay()
        {
            ICollection<int> results = mapper.Map((IEnumerable<string>) null);

            Assert.IsNotNull(results);
            Assert.AreEqual(0, results.Count);
            mapper.DidNotReceiveWithAnyArgs().Map(Arg.Any<string>());
        }

        [TestMethod]
        public void MapNullSourceToEmptyListTwoWay()
        {
            ICollection<string> results = mapper.Map((IEnumerable<int>) null);

            Assert.IsNotNull(results);
            Assert.AreEqual(0, results.Count);
            mapper.DidNotReceiveWithAnyArgs().Map(Arg.Any<int>());
        }

        [TestMethod]
        public void MapSourceListToDestinationListOneWay()
        {
            var source = new List<string> { String1, String2 };

            ICollection<int> results = mapper.Map(source);

            Assert.AreEqual(2, results.Count);
            Assert.AreEqual(Int1, results.ElementAt(0));
            Assert.AreEqual(Int2, results.ElementAt(1));
        }

        [TestMethod]
        public void MapSourceListToDestinationListTwoWay()
        {
            var source = new List<int> { Int1, Int2 };

            ICollection<string> results = mapper.Map(source);

            Assert.AreEqual(2, results.Count);
            Assert.AreEqual(String1, results.ElementAt(0));
            Assert.AreEqual(String2, results.ElementAt(1));
        }
    }
}