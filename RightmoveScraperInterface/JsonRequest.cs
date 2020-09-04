using System;
using System.Collections.Generic;
using System.Text;

namespace RightmoveScraperInterface
{
    public class JsonRequest
    {
        public string search_area { get; set; }

        public Dictionary<string, int> RequestBasicParameters = new Dictionary<string, int>();
        public Dictionary<string, List<string>> CompiledAvancedParameters = new Dictionary<string, List<string>>();

        public JsonRequest(string searchArea)
        {
            search_area = searchArea;
        }
    }
}
