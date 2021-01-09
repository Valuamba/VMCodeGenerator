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
            //args = new string[]
            //{
            //    "create",
            //    "-n",
            //    "FirstName",
            //    "-db",
            //    "AtonBase",
            //    "-b",
            //    "AtonBaseQuery"
            //};
            QueryManager manager = new QueryManager();
            manager.Execute(args);
        }
    }
}
