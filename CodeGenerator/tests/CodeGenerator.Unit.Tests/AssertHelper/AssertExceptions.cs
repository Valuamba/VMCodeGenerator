using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace CodeGenerator.Unit.Tests.AssertHelper
{
    public class AssertExceptions
    {
        public static void DoesNotThrow(Action a)
        {
            try
            {
                a();
            }
            catch (Exception ex)
            {
                Assert.Fail("Expected no {0} to be thrown", ex.GetType().Name);
            }
        }

    }
}
