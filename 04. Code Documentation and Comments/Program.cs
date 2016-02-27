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
            Console.InputEncoding = Encoding.Unicode;
            string userInput = Console.ReadLine();
            
            Console.WriteLine(StringExtensions.ConvertCyrillicToLatinLetters(userInput));

        }
    }
}