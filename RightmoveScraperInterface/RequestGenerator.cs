using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

namespace RightmoveScraperInterface
{
    class RequestGenerator
    {
        public JsonRequest Request { get; set; }
        public PropertySearchOptions SearchOptions { get; set; }

        public readonly List<string> basicSearchParameters = new List<string>();
        public readonly List<Dictionary<string, bool>> advancedSearchParameters = new List<Dictionary<string, bool>>();
        public readonly List<string> advancedSearchParameterTitles = new List<string>();

        // Makes a new JsonRequest and declares the search options
        public RequestGenerator(string searchArea, PropertySearchOptions searchOptions)
        {
            Request = new JsonRequest(searchArea);
            SearchOptions = searchOptions;

            basicSearchParameters = SearchOptions.basicSearchParameters;
            advancedSearchParameters = SearchOptions.advancedSearchParameters;
            advancedSearchParameterTitles = SearchOptions.advancedSearchParameterTitles;
        }

        public void GenerateJSONRequestCMD()
        {
            SetBasicSearchParametersCMD();
            ValidateBasicParameters("min", "max", Request.RequestBasicParameters);
            SetAdvancedSearchParametersCMD();
            PrintBasicSearchParameters();
            PrintAdvancedSearchParameters();
            JsonOutput.SerializeJsonRequest(Request);
        }

        // Either makes a new entry for a Basic Parameter, or changes the value of the Basic Parameter
        public void SetBasicSearchParameter(string searchParameter, int parameterValue)
        {
            if (Request.RequestBasicParameters.ContainsKey(searchParameter))
                Request.RequestBasicParameters[searchParameter] = parameterValue;
            else
                Request.RequestBasicParameters.Add(searchParameter, parameterValue);
        }

        // Sets an advanced search parameter to boolean value
        public void SetAdvancedSearchParameter(Dictionary<string, bool> advSearchParameter, string parameter, bool setting)
        {
            advSearchParameter[parameter] = setting;
        }

        // Sets Basic Search Parameters from the command line
        public void SetBasicSearchParametersCMD()
        {
            Console.WriteLine("--- Basic Search Parameters ---\n submit integer number, or press enter to leave blank. \n");
            foreach (var searchProperty in basicSearchParameters)
            {
                Console.Write("Set value for {0}: ", searchProperty);
                var input = Utilities.CmdIntegerInput("");

                if (input != 0)
                    SetBasicSearchParameter(searchProperty, input);

                Console.WriteLine();
            }
        }

        // Sets all Advanced Search Parameters
        public void SetAdvancedSearchParametersCMD()
        {
            Console.WriteLine("--- Advanced Search Parameters --- \n");

            var i = 0;
            foreach (var searchParameter in advancedSearchParameters)
            {
                if (i > advancedSearchParameterTitles.Count)
                    throw new ArgumentOutOfRangeException("Advanced-Search-Parameter titles not equal to number of parameters");

                Request.CompiledAvancedParameters.Add(advancedSearchParameterTitles[i],
                    SetAdvancedSearchParameterCMD(searchParameter, advancedSearchParameterTitles[i]));
                i++;
            }
        }

        // Sets Advanced Search Parameter from command line
        private List<string> SetAdvancedSearchParameterCMD(Dictionary<string, bool> advSearchParameter, string advSearchParameterTitle)
        {
            List<string> keys = new List<string>(advSearchParameter.Keys);
            List<string> compiledParameters = new List<string>();
            Console.WriteLine("'Y' or '1' == YES. 'N' or '0' == NO.\n");
            Console.WriteLine("Refine search options for: {0}?", advSearchParameterTitle);
            if (!Utilities.CmdBoolResponse(""))
                return compiledParameters;

            foreach (var parameter in keys)
            {
                Console.WriteLine("Search for: {0}?", parameter);

                if (Utilities.CmdBoolResponse(""))
                {
                    SetAdvancedSearchParameter(advSearchParameter, parameter, true);
                    compiledParameters.Add(parameter);
                }
            }

            return compiledParameters;
        }

        // Validates "min" and "max" values entered for basic search parameters
        // Prompts user to re-enter values if an invalid pair is found
        public void ValidateBasicParameters(string min, string max, Dictionary<string, int> basicParameters)
        {
            var keys = new List<string>(basicParameters.Keys);
            string testedStr, minStr, maxStr;
            int minValue, maxValue;

            foreach (var key in keys)
            {
                if (key.StartsWith(min))
                {
                    minStr = key;
                    minValue = basicParameters[key];
                    testedStr = minStr.TrimStart(min.ToCharArray());

                    if (basicParameters.TryGetValue(max + testedStr, out maxValue))
                    {
                        maxStr = max + testedStr;

                        if (!Utilities.ValidateMinMax(minValue, maxValue))
                        {
                            Console.WriteLine("Error: {0}{2} must be lower than {1}{2}", min, max, testedStr);
                            SetBasicSearchParameter(minStr, Utilities.CmdIntegerInput("enter new value for " +  minStr));
                            SetBasicSearchParameter(maxStr, Utilities.CmdIntegerInput("enter new value for " + maxStr));
                            ValidateBasicParameters(min, max, basicParameters);
                        }
                    }
                    else
                        Console.WriteLine("Error: {0}/{1} pair for {2} not found. Ammend \"Search Options\"", min, max, testedStr);
                }
            }
        }

        // Prints Basic Search Parameters to command line
        public void PrintBasicSearchParameters()
        {
            if (Request.RequestBasicParameters == null)
                throw new ArgumentNullException("Search parameters not yet entered into JSON request.");

            Console.WriteLine("---------------------------------\nBasic search parameters\n---------------------------------");

            foreach (var kvp in Request.RequestBasicParameters)
                Console.WriteLine(kvp.Key + ": " + kvp.Value);
        }

        // Prints Advanced Search Parameters to command line
        public void PrintAdvancedSearchParameters()
        {
            Console.WriteLine("---------------------------------\nAdvanced search parameters\n---------------------------------");

            if (Request.CompiledAvancedParameters == null || Request.CompiledAvancedParameters.Count == 0)
            {
                Console.WriteLine("No advanced search parameters found");
                return;
            }

            foreach (var set in Request.CompiledAvancedParameters)
            {
                Console.Write("{0} includes: ", set.Key);

                foreach (var parameter in set.Value)
                    Console.Write(parameter + ", ");
                Console.WriteLine();
            }
        }
    }
}
