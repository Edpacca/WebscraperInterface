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
            // RequestInterface(true) for manual SearchArea entry

            var requestInterface = new RequestInterface(false);
            var searchOptions = new PropertySearchOptions();

            requestInterface.GenerateRequestCMD(searchOptions);
        }
    }
}
