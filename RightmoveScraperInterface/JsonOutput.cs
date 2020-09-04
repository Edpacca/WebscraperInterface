using System;
using System.IO;
using Newtonsoft.Json;

namespace RightmoveScraperInterface
{
    public class JsonOutput
    {
        public static void SerializeJsonRequest(JsonRequest request)
        {
            string jsonString = JsonConvert.SerializeObject(makeJsonObject(request), Formatting.Indented);
            string path = Directory.GetCurrentDirectory();
            using (StreamWriter streamWriter = new StreamWriter(Path.Combine(path, "request.json")))
                streamWriter.Write(jsonString);

        }

        public static JsonRequestObject makeJsonObject(JsonRequest jsonRequest)
        {
            var JsonObject = new JsonRequestObject();

            JsonObject.search_area = jsonRequest.search_area;
            JsonObject.min_bedrooms = jsonRequest.RequestBasicParameters["min_bedrooms"];
            JsonObject.max_bedrooms = jsonRequest.RequestBasicParameters["max_bedrooms"];
            JsonObject.min_price = jsonRequest.RequestBasicParameters["min_price"] * 1000;
            JsonObject.max_price = jsonRequest.RequestBasicParameters["max_price"] * 1000;
            JsonObject.show_house_type = jsonRequest.CompiledAvancedParameters["property_type"];
            JsonObject.must_have = jsonRequest.CompiledAvancedParameters["must_have"];
            JsonObject.dont_show = jsonRequest.CompiledAvancedParameters["dont_show"];

            return JsonObject;
        }

    }
}
