using CodeGenerator.Document;
using CodeGenerator.Generators;
using System;
using System.Collections.Generic;

namespace CodeGenerator
{
    public class Program1
    {
        public static void Main(string[] args)
        {
            try
            {
                QueryManager manager = new QueryManager();
                manager.Execute(args);
                Console.WriteLine("Seccessfully!");
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
                Environment.Exit(0);
            }
        }
    }
}
