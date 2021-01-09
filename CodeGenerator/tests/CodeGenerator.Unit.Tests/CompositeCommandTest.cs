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
    public class CompositeCommandTest
    {
        [TestMethod]
        public void InvokeMethod_ValidData()
        {
            var compositeCommand = new CreateCompositeCommand();

            compositeCommand.Add(new NameCommand("FileTxt"));
            compositeCommand.Add(new DataBaseCommand("FileTxt"));
            compositeCommand.Add(new PathCommand("Path"));
            compositeCommand.Add(new ParrentClassCommand("FileTxt"));

            AssertExceptions.DoesNotThrow(() => compositeCommand.Invoke());
        }

        [TestMethod]
        public void InvokeMethod_CheckNullReferenceException_ValidData()
        {
            var compositeCommand = new CreateCompositeCommand();

            compositeCommand.Add(new NameCommand("FileTxt"));
            compositeCommand.Add(new DataBaseCommand("FileTxt"));
            compositeCommand.Add(null);
            compositeCommand.Add(new ParrentClassCommand("FileTxt"));

            AssertExceptions.DoesNotThrow(() => compositeCommand.Invoke());
        }
    }
}
