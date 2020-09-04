using System;
using System.Collections.Generic;
using System.Text;

namespace RightmoveScraperInterface
{
    public class SearchOptions
    {
        public readonly List<Dictionary<string, bool>> advancedSearchParameters = new List<Dictionary<string, bool>>();

        public SearchOptions()
        {
            advancedSearchParameters.Add(propertyTypes);
            advancedSearchParameters.Add(mustHave);
            advancedSearchParameters.Add(dontShow);
        }

        public readonly List<string> basicSearchParameters = new List<string>() {
            "max_bedrooms",
            "min_bedrooms",
            "max_price",
            "min_price",
        };

        public readonly Dictionary<string, bool> propertyTypes = new Dictionary<string, bool>()
        {
            ["flat"] = false,
            ["detached"] = false,
            ["semi-detached"] = false,
            ["terraced"] = false,
            ["bungalow"] = false,
            ["land"] = false,
            ["park-home"] = false
        };

        public readonly Dictionary<string, bool> mustHave = new Dictionary<string, bool>()
        {
            ["garden"] = false,
            ["parking"] = false

        };

        public readonly Dictionary<string, bool> dontShow = new Dictionary<string, bool>()
        {
            ["newHome"] = false,
            ["retirement"] = false,
            ["sharedOwnership"] = false
        };
        
    }
}
