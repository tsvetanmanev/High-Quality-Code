using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telerik.ILS.Common;

namespace _04.Code_Documentation_and_Comments
{
    class Program
    {
        static void Main(string[] args)
        {
            string userInput = Console.ReadLine();

            Console.WriteLine(StringExtensions.ToMd5Hash(userInput));
            Console.WriteLine(StringExtensions.ToDateTime(userInput));
        }
    }
}