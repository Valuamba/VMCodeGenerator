using System.Collections.Generic;
using CodeGenerator.Commands;
using CodeGenerator.Utilities;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CodeGenerator.Test
{
    [TestClass]
    public class ArgumentUtilityTest
    {
        [TestMethod]
        public void ParseMethod_ValidData()
        {
            List<string> args = new List<string>
            {
                "create",
                "-n",
                "FirstName",
                "-db",
                "AtonBase",
                "--cpath",
                "C:\\User",
                "-b",
                "AtonBaseQuery"
            };
            var compositeCommand = ArgumentUtility.Parse(args);

            Assert.AreEqual(4, compositeCommand.ChildCommands.Count, "Count does not equal to 4.");
            Assert.AreEqual(typeof(NameCommand), compositeCommand.ChildCommands[0].GetType(), "Incorrect type of command.");
            Assert.AreEqual(typeof(DataBaseCommand), compositeCommand.ChildCommands[1].GetType(), "Incorrect type of command.");
            Assert.AreEqual(typeof(ClassProjectPathCommand), compositeCommand.ChildCommands[2].GetType(), "Incorrect type of command.");
            Assert.AreEqual(typeof(ParrentClassCommand), compositeCommand.ChildCommands[3].GetType(), "Incorrect type of command.");
        }
    }
}
