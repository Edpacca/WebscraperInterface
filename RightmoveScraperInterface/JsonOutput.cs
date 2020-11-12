using System;
using System.IO;
using Newtonsoft.Json;

namespace RightmoveScraperInterface
{
    public class JsonOutput
    {
        public static void SerializeJsonRequest(JsonRequest request)
        {
            string jsonString = JsonConvert.SerializeObject(request, Formatting.Indented);
            string path = Directory.GetCurrentDirectory();
            using (StreamWriter streamWriter = new StreamWriter(Path.Combine(path, "request.json")))
                streamWriter.Write(jsonString);

        }
    }
}
