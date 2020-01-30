using System.Collections.Generic;
using System.Linq;

namespace ConstructionLine.CodingChallenge
{
    public class SearchEngine
    {
        private readonly List<Shirt> _shirts;

        public SearchEngine(List<Shirt> shirts)
        {
            _shirts = shirts;
        }

        /// <summary>
        /// For example, for small, medium and red the search engine should return shirts
        /// that are either small or medium in size and are red in color.
        /// 
        /// NOTE: Assume we are searching for all hits of color against size and the converse,
        /// if only one option is specified e.g a color then find all instances of that color
        /// regardless of the size.
        /// 
        /// If none are supplied then no matches etc... 
        /// </summary>
        /// <param name="options"></param>
        /// <returns></returns>
        public SearchResults Search(SearchOptions options)
        {
            var shirtResults = 
                _shirts.Where(x => (!options.Colors.Any() || options.Colors.Contains(x.Color)) &&
                                   (!options.Sizes.Any() || options.Sizes.Contains(x.Size)) )
                .ToList();

            return new SearchResults
            {
                Shirts = shirtResults,
                ColorCounts = ColorCounts(shirtResults),
                SizeCounts = SizeCounts(shirtResults)
            };
        }

        private List<SizeCount> SizeCounts(List<Shirt> shirtResults)
        {
            var counts = new List<SizeCount>();
            Size.All.ForEach(item => counts.Add( new SizeCount { Size = item, Count = shirtResults.Count(x => x.Size == item) }));
            return counts;
        }

        private List<ColorCount> ColorCounts(List<Shirt> shirtResults)
        {
            var counts = new List<ColorCount>();
            Color.All.ForEach(item => counts.Add(new ColorCount { Color = item, Count = shirtResults.Count(x => x.Color == item) }));
            return counts;
        }
    }
}