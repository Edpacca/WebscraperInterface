using System;
using System.Collections.Generic;
using System.Text;

namespace RightmoveScraperInterface
{
    public class JsonRequest
    {
        public string SearchArea { get; set; }

        public Dictionary<string, int> RequestBasicParameters = new Dictionary<string, int>();
        public List<Dictionary<string, bool>> RequestAdvancedParameters = new List<Dictionary<string, bool>>();

        public JsonRequest(string searchArea)
        {
            SearchArea = searchArea;
        }
    }
}
