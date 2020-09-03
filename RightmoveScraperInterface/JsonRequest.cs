using System;
using System.Collections.Generic;
using System.Text;

namespace RightmoveScraperInterface
{
    public class JsonRequest
    {
        private string SearchArea { get => _searchArea; set => _searchArea = value; }
        private string _searchArea;

        public Dictionary<string, int> JsonBasicSearchParameters = new Dictionary<string, int>();
        public List<Dictionary<string, bool>> JsonAdvancedSearchParameters = new List<Dictionary<string, bool>>();

        public JsonRequest(string searchArea)
        {
            SearchArea = searchArea;
        }
    }
}
