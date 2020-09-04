using System;
using System.Collections.Generic;
using System.Text;

namespace RightmoveScraperInterface
{
    public class JsonRequestObject
    {
        public string search_area;
        public int max_bedrooms;
        public int min_bedrooms;
        public int max_price;
        public int min_price;
        public List<string> show_house_type;
        public List<string> must_have;
        public List<string> dont_show;
    }
}
