using System;
using System.Collections.Generic;
using System.Text;

namespace RightmoveScraperInterface
{
    public class PropertySearchOptions
    {
        // Advanced search parameters have type bool associated with a string
        public readonly List<Dictionary<string, bool>> advancedSearchParameters = new List<Dictionary<string, bool>>();
        public List<string> advancedSearchParameterTitles = new List<string>();

        // Constructor initialises the List of dictionaries of advanced parameters
        public PropertySearchOptions()
        {
            foreach (var set in SetOfAdvancedSearchParameters)
            {
                advancedSearchParameterTitles.Add(set.Key);
                advancedSearchParameters.Add(set.Value);
            }
        }

        // Basic parameters have type int
        public readonly List<string> basicSearchParameters = new List<string>() {
            "max_bedrooms",
            "min_bedrooms",
            "max_price",
            "min_price",
        };

        // ADVANCED PARAMETERS

        Dictionary<string, Dictionary<string, bool>> SetOfAdvancedSearchParameters = new Dictionary<string, Dictionary<string, bool>>()
        {
            ["property_type"] = new Dictionary<string, bool>()
            {
                ["flat"] = false,
                ["detached"] = false,
                ["semi-detached"] = false,
                ["terraced"] = false,
                ["bungalow"] = false,
                ["land"] = false,
                ["park-home"] = false
            },

            ["must_have"] = new Dictionary<string, bool>()
            {
                ["garden"] = false,
                ["parking"] = false
            },

            ["dont_show"] = new Dictionary<string, bool>()
            {
                ["newHome"] = false,
                ["retirement"] = false,
                ["sharedOwnership"] = false
            },
        };
    }
}
