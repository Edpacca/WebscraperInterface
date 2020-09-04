using Microsoft.VisualBasic.FileIO;
using System;
using System.IO;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Linq;
using System.Security.Cryptography.X509Certificates;

namespace RightmoveScraperInterface
{
    class Program
    {
        static void Main(string[] args)
        {
            GenerateRequest();

        }

        public static void GenerateRequest()
        {
            string defaultSearchArea = "REGION%5E475";
            string input;

            Console.WriteLine("press enter to generate new request.");
            Console.WriteLine("default search area will be set to: " + defaultSearchArea);

            while (true)
            {
                input = Console.ReadLine();
                if (input == string.Empty)
                    break;
                else
                    Console.WriteLine("press enter to continue");
            }

            var currentRequest = new RequestGenerator(defaultSearchArea);

            currentRequest.GetBasicSearchParameters();
            Console.WriteLine();
            currentRequest.PrintBasicSearchParameters();
            Console.WriteLine();
            currentRequest.SetAdvancedSearchParameters();
            Console.WriteLine();
            currentRequest.PrintAdvancedSearchParameters();
 


        }
    }
}
