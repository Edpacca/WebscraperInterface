using System;
using System.Collections.Generic;
using System.Linq;

namespace RightmoveScraperInterface
{
    public static class Utilities
    {
        public static bool CmdBoolResponse(string message)
        {
            string input;
            if (!string.IsNullOrEmpty(message))
                Console.WriteLine(message);
            while (true)
            {
                input = Console.ReadLine();

                if (string.Compare(input.ToLower(), "y") == 0 || input == 1.ToString())
                    return true;
                else if (string.Compare(input.ToLower(), "n") == 0 || input == 0.ToString())
                    return false;
                else
                    Console.WriteLine("Enter valid input: 'Y' or '1' for YES, 'N' or '0' for NO");
            }
        }

        public static int CmdIntegerInput(string message)
        {
            string input;
            if (!string.IsNullOrEmpty(message))
                Console.WriteLine(message);

            while (true)
            {
                input = Console.ReadLine();

                if (string.IsNullOrEmpty(input))
                    return 0;
                else if (input.All(Char.IsDigit))
                    return Convert.ToInt32(input);
                else
                    Console.WriteLine("submit a valid integer, or leave blank");
            }
        }

        public static bool ValidateMinMaxParameters(string min, string max, Dictionary<string, int> parameters)
        {
            string testedStr, minStr;
            int minValue, maxValue;

            foreach (var kvp in parameters)
            {
                if (kvp.Key.StartsWith(min))
                {
                    minStr = kvp.Key;
                    minValue = kvp.Value;
                    testedStr = minStr.TrimStart(min.ToCharArray());

                    parameters.TryGetValue(max + testedStr, out maxValue);

                    if (ValidateMinMax(minValue, maxValue))
                        return true;
                    else
                    {
                        Console.WriteLine("Error: {0} {1} must be lower than {2}", min, testedStr, max);
                        return false;
                    }
                }
            }
            return true;
        }

        public static bool ValidateMinMax(int min, int max)
        {
            if (min > max)
                return false;
            else
                return true;
        }
    }
}
