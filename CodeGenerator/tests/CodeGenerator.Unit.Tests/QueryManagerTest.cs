using CodeGenerator.Commands;
using CodeGenerator.CompositeCommands;
using CodeGenerator.Unit.Tests.AssertHelper;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace CodeGenerator.Test
{
    [TestClass]
    public class QueryManagerTest
    {
        [TestMethod]
        public void ValidateCompositeCommand_AllAvailablesCommands_ValidData()
        {
            var compositeCommand = new CreateCompositeCommand();

            compositeCommand.Add(new NameCommand("FileTxt"));
            compositeCommand.Add(new PathCommand("Path"));
            compositeCommand.Add(new DataBaseCommand("FileTxt"));
            compositeCommand.Add(new ParrentClassCommand("FileTxt"));

            var Rule = new Rule(orderedList: new List<Type> { typeof(NameCommand) }, unorderedList: new List<Type> { typeof(DataBaseCommand), typeof(PathCommand), typeof(ParrentClassCommand) });
            QueryManager qq = new QueryManager();

            AssertExceptions.DoesNotThrow(() => qq.Validate(compositeCommand, Rule));
            Assert.AreEqual(4, compositeCommand.ChildCommands.Count, "Count of ChildCommands list should be equal to count of commands presented in rules.");
            Assert.AreEqual(typeof(NameCommand), compositeCommand.ChildCommands[0].GetType(), "Incorrect type.");
            Assert.AreEqual(typeof(DataBaseCommand), compositeCommand.ChildCommands[1].GetType(), "Incorrect type.");
            Assert.AreEqual(typeof(PathCommand), compositeCommand.ChildCommands[2].GetType(), "Incorrect type.");
            Assert.AreEqual(typeof(ParrentClassCommand), compositeCommand.ChildCommands[3].GetType(), "Incorrect type.");
        }

        [TestMethod]
        public void ValidateCompositeCommand_WithoutRule_ValidData()
        {
            var compositeCommand = new GlobalConfigCompositeCommand();
            AssertExceptions.DoesNotThrow(() => new QueryManager().Validate(compositeCommand, null));
            Assert.AreEqual(0, compositeCommand.ChildCommands.Count, "Count of ChildCommands list should be equal to count of commands presented in rules.");
        }

        [TestMethod]
        public void ValidateCompositeCommand_OnlyOneCommand_ValidData()
        {
            var compositeCommand = new CreateCompositeCommand();

            compositeCommand.Add(new NameCommand("FileTxt"));
            compositeCommand.Add(new ParrentClassCommand("FileTxt"));

            var Rule = new Rule(orderedList: new List<Type> { typeof(NameCommand) }, unorderedList: new List<Type> { typeof(DataBaseCommand), typeof(PathCommand), typeof(ParrentClassCommand) });
            QueryManager qq = new QueryManager();

            AssertExceptions.DoesNotThrow(() => qq.Validate(compositeCommand, Rule));
            Assert.AreEqual(4, compositeCommand.ChildCommands.Count, "Count of ChildCommands list should be equal to count of commands presented in rules.");
            Assert.AreEqual(typeof(NameCommand), compositeCommand.ChildCommands[0].GetType(), "Incorrect value.");
            Assert.AreEqual(null, compositeCommand.ChildCommands[1], "Incorrect value.");
            Assert.AreEqual(null, compositeCommand.ChildCommands[2], "Incorrect value.");
            Assert.AreEqual(typeof(ParrentClassCommand), compositeCommand.ChildCommands[3].GetType(), "Incorrect type.");
        }
    }
}
