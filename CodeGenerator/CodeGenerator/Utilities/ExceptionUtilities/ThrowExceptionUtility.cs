using System;
using System.Linq;

namespace CodeGenerator.Utilities.ExceptionUtilities
{ 
    public static class ThrowExceptionUtility
    {
        public static void ThrowException<TException>(Func<bool> condition, string message) where TException : Exception
        {
            if (condition())
            {
                throw (TException)Activator.CreateInstance(typeof(TException), message);
            }
        }

        public static TReturn ThrowException<TException, TReturn>(TReturn returnValue, params object[] notEqualsValues) where TException : Exception
        {
            if (notEqualsValues.Any(x => x == null ? returnValue == null : x.Equals(returnValue)))
            {
                throw (TException)Activator.CreateInstance(typeof(TException), $"Returned value cannot be equal to '{returnValue}'");
            }
            return returnValue;
        }
    }
}
