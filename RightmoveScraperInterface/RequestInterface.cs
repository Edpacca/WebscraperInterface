using System;
using System.Collections.Generic;
using System.Text;

namespace RightmoveScraperInterface
{
    class RequestInterface
    {
        private const string defaultSearchArea = "REGION%5E475";
        public bool ManualSearchArea { get; set; }

        public RequestInterface(bool manualSearchArea)
        {
            ManualSearchArea = manualSearchArea;
        }

        public void GenerateRequestCMD(PropertySearchOptions searchOptions)
        {
            string searchArea;

            if (ManualSearchArea)
            {
                Console.WriteLine("enter the RightMove search area code: ");
                searchArea = Console.ReadLine();
            }
            else
            {
                Console.WriteLine("default search area will be set to: " + defaultSearchArea);
                searchArea = defaultSearchArea;
            }

            Console.WriteLine("press enter to continue");
            while (Console.ReadKey().Key != ConsoleKey.Enter) {}

            var currentRequest = new RequestGenerator(searchArea, searchOptions);
            Console.WriteLine("Generating search request...");
            Console.WriteLine();

            currentRequest.GenerateJSONRequestCMD();
        }
    }
}
