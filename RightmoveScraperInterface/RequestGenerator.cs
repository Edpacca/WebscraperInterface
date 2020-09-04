using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

namespace RightmoveScraperInterface
{
    class RequestGenerator
    {
        JsonRequest newRequest;
        SearchOptions searchOptions;

        public readonly List<string> basicSearchParameters = new List<string>();
        public readonly List<Dictionary<string, bool>> advancedSearchParameters = new List<Dictionary<string, bool>>();

        public RequestGenerator(string searchArea)
        {
            newRequest = new JsonRequest(searchArea);
            searchOptions = new SearchOptions();

            basicSearchParameters = searchOptions.basicSearchParameters;
            advancedSearchParameters = searchOptions.advancedSearchParameters;
        }



        public void SetBasicSearchParameter(string searchParameter, int parameterValue, JsonRequest request)
        {
            if (request.JsonBasicSearchParameters.ContainsKey(searchParameter))
                request.JsonBasicSearchParameters[searchParameter] = parameterValue;
            else
                request.JsonBasicSearchParameters.Add(searchParameter, parameterValue);
        }

        public void PrintAdvancedSearchParameters()
        {
            Console.WriteLine("---------------------------------");
            Console.WriteLine("Advanced search parameters");
            Console.WriteLine("---------------------------------");
            Console.WriteLine();
            foreach (var set in advancedSearchParameters)
            {
                Console.WriteLine("{0}", set.ToString());

                foreach (var parameter in set)
                {
                    if (parameter.Value == true)
                        Console.WriteLine("searching for {0}", parameter.Key);
                }
                Console.WriteLine("-----------");
            }
        }

        public void SetAdvancedSearchParameters()
        {
            foreach (var searchParameter in advancedSearchParameters)
            {
                SetAdvancedSearchParameter(searchParameter);
                newRequest.JsonAdvancedSearchParameters.Add(searchParameter);
            }
        }

        private void SetAdvancedSearchParameter(Dictionary<string, bool> AdvSearchParameter)
        {
            string input;

            Console.WriteLine("Refining search options:");
            Console.WriteLine();
            foreach (var parameter in AdvSearchParameter)
            {
                Console.WriteLine("refine search to include {0}?", parameter.Key);
                Console.Write("'y' for yes, 'n' for no: ");

                while (true)
                {
                    input = Console.ReadLine();

                    if (string.Compare(input.ToLower(),"y") == 0)
                    {
                        AdvSearchParameter[parameter.Key] = true;
                        Console.WriteLine("search refined: {0}", parameter.Key);
                        break;
                    }
                    else if (string.Compare(input.ToLower(), "n") == 0)
                        break;
                    else
                        Console.WriteLine("Enter valid input: press 'y' for yes, 'n' for no");

                }
            }
        }

        public void GetBasicSearchParameters()
        {
            foreach (var searchProperty in basicSearchParameters)
            {
                Console.WriteLine();
                Console.WriteLine("set search property: {0}", searchProperty);
                Console.Write("Enter an integer number to set, or press enter to leave blank: ");

                EnterIntSearchProperty(searchProperty);
            }
        }

        public void PrintBasicSearchParameters()
        {
            if (newRequest == null)
                throw new ArgumentNullException("Search parameters not yet entered into JSON request.");
            
            while ((CheckMinMaxParameters(newRequest)) == false)
            {
                Console.WriteLine("---------------------------------");
                Console.WriteLine("Basic search parameters");
                Console.WriteLine("---------------------------------");
            }
            
            foreach (var kvp in newRequest.JsonBasicSearchParameters)
                Console.WriteLine(kvp.Key + ": " + kvp.Value);
        }

        private void EnterIntSearchProperty(string searchParameter)
        {
            string input;

            while (true)
            {
                input = Console.ReadLine();

                if (string.IsNullOrEmpty(input))
                {
                    Console.WriteLine("{0} not set", searchParameter);
                    break;
                }
                else if (input.All(Char.IsDigit))
                {
                    SetBasicSearchParameter(searchParameter, Int32.Parse(input), newRequest);
                    Console.WriteLine("{0} set to {1}", searchParameter, input);
                    Console.WriteLine();
                    break;
                }
                else
                {
                    Console.WriteLine("Enter a valid integer, or press Enter to leave blank");
                }
            }
        }

        private bool CheckMinMaxParameters(JsonRequest searchRequest)
        {
            int minValue, maxValue;
            string testedSearchProperty, minSearchProperty, maxSearchProperty;

            foreach (var kvp in searchRequest.JsonBasicSearchParameters)
            {
                if (kvp.Key.StartsWith("min"))
                {
                    minSearchProperty = kvp.Key;
                    minValue = kvp.Value;
                    testedSearchProperty = kvp.Key.Substring(kvp.Key.IndexOf("_") + 1);

                    if (searchRequest.JsonBasicSearchParameters.TryGetValue("max_" + testedSearchProperty, out maxValue))
                    {
                        if (minValue >= maxValue)
                        {
                            maxSearchProperty = "max_" + testedSearchProperty;
                            Console.WriteLine("{0} must be lower than {1}", minSearchProperty, maxSearchProperty);

                            Console.Write("enter new value for {0}: ", minSearchProperty);
                            EnterIntSearchProperty(minSearchProperty);
                            
                            Console.Write("enter new value for {0}: ", maxSearchProperty);
                            EnterIntSearchProperty(maxSearchProperty);

                            return false;
                        }
                    }
                    else
                    {
                        Console.WriteLine("Error: search parameter min/max pair \"{0}\" not found", testedSearchProperty);
                    }
                }
            }

            return true;
        }


    }
}
