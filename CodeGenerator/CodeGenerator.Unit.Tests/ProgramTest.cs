using System.Collections.Generic;
using CodeGenerator.Commands;
using CodeGenerator.Utilities;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CodeGenerator.Unit.Tests
{
    [TestClass]
    public class ProgramTest
    {
        [TestMethod]
        public void Should_CreateTwoFiles()
        {
            var args = new string[]
            {
                "create",
                "-n",
                "FirstName",
                "-db",
                "AtonBase",
                "-b",
                "AtonBaseQuery"
            };
            Program1.Main(args);
        }

    }
}
