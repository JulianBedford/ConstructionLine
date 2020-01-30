using System;
using System.Collections.Generic;
using NUnit.Framework;

namespace ConstructionLine.CodingChallenge.Tests
{
    [TestFixture]
    public class SearchEngineTests : SearchEngineTestsBase
    {
        private List<Shirt> _shirts;
        private SearchEngine _searchEngine;

        [SetUp]
        public void Setup()
        {
            _shirts = new List<Shirt>
            {
                new Shirt(Guid.NewGuid(), "Red - Small", Size.Small, Color.Red),
                new Shirt(Guid.NewGuid(), "Black - Medium", Size.Medium, Color.Black),
                new Shirt(Guid.NewGuid(), "Blue - Large", Size.Large, Color.Blue),
                new Shirt(Guid.NewGuid(), "Red - Large", Size.Large, Color.Red),
                new Shirt(Guid.NewGuid(), "White - Large", Size.Large, Color.White),
            };
            _searchEngine = new SearchEngine(_shirts);
        }


        [Test]
        public void Test()
        {
            var shirts = new List<Shirt>
            {
                new Shirt(Guid.NewGuid(), "Red - Small", Size.Small, Color.Red),
                new Shirt(Guid.NewGuid(), "Black - Medium", Size.Medium, Color.Black),
                new Shirt(Guid.NewGuid(), "Blue - Large", Size.Large, Color.Blue),
            };

            var searchEngine = new SearchEngine(shirts);

            var searchOptions = new SearchOptions
            {
                Colors = new List<Color> {Color.Red},
                Sizes = new List<Size> {Size.Small}
            };

            var results = searchEngine.Search(searchOptions);

            Assert.AreEqual(1, results.ColorCounts.Find(c => c.Color == Color.Red).Count);
            Assert.AreEqual(1, results.SizeCounts.Find(s => s.Size == Size.Small).Count);

            AssertResults(results.Shirts, searchOptions);
            AssertSizeCounts(results.Shirts, searchOptions, results.SizeCounts);
            AssertColorCounts(results.Shirts, searchOptions, results.ColorCounts);
        }

        [Test]
        public void GivenSelection_WhenSearchForNothing_ThenFindCorrectCounts()
        {
            var searchOptions = new SearchOptions{};
            var results = _searchEngine.Search(searchOptions);

            AssertResults(results.Shirts, searchOptions);
            AssertSizeCounts(_shirts, searchOptions, results.SizeCounts);
            AssertColorCounts(_shirts, searchOptions, results.ColorCounts);
        }

        [Test]
        public void GivenSelection_WhenSearchForAll_ThenFindCorrectCounts()
        {
            var searchOptions = new SearchOptions
            {
                Colors = new List<Color> { Color.Red, Color.White, Color.Yellow, Color.Blue, Color.Black },
                Sizes = new List<Size> { Size.Medium, Size.Large, Size.Small },
            };

            var results = _searchEngine.Search(searchOptions);

            AssertResults(results.Shirts, searchOptions);
            AssertSizeCounts(_shirts, searchOptions, results.SizeCounts);
            AssertColorCounts(_shirts, searchOptions, results.ColorCounts);
        }

        [Test]
        public void GivenSelection_WhenSearchForOnlyColors_ThenFindCorrectSizeCounts()
        {
            var searchOptions = new SearchOptions
            {
                Colors = new List<Color> { Color.Red, Color.White },
            };

            var results = _searchEngine.Search(searchOptions);

            AssertResults(results.Shirts, searchOptions);
            AssertSizeCounts(_shirts, searchOptions, results.SizeCounts);
            AssertColorCounts(_shirts, searchOptions, results.ColorCounts);
        }
        
        [Test]
        public void GivenSelection_WhenSearchForOnlySizes_ThenFindCorrectColorCounts()
        {
            var searchOptions = new SearchOptions
            {
                Sizes = new List<Size> { Size.Medium, Size.Large },
            };

            var results = _searchEngine.Search(searchOptions);

            AssertResults(results.Shirts, searchOptions);
            AssertSizeCounts(_shirts, searchOptions, results.SizeCounts);
            AssertColorCounts(_shirts, searchOptions, results.ColorCounts);
        }

        [Test]
        public void GivenSelection_WhenSearchForSelection_ThenFindCorrectCounts()
        {
            var searchOptions = new SearchOptions
            {
                Colors = new List<Color> { Color.Red },
                Sizes = new List<Size> { Size.Medium, Size.Large }
            };

            var results = _searchEngine.Search(searchOptions);

            AssertResults(results.Shirts, searchOptions);
            AssertSizeCounts(_shirts, searchOptions, results.SizeCounts);
            AssertColorCounts(_shirts, searchOptions, results.ColorCounts);
        }

    }
}
