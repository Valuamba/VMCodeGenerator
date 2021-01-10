using System;

namespace CodeGenerator.Utilities.ExceptionUtilities
{
    public static class HandleExceptionUtility
    {
        public static TReturn InheritanceException<TReturn, TException>(Func<TReturn> method, string message, Action handledExcpetionAction = null) where TException : Exception
        {
            try
            {
                return method();
            }
            catch (Exception ex) when(ex is TException)
            {
                handledExcpetionAction?.Invoke();
                throw (TException)Activator.CreateInstance(ex.GetType(), message, ex);
            }
        }

        public static TReturn OverrideException<TReturn, TException>(Func<TReturn> method, string message, Action handledExcpetionAction = null) where TException : Exception
        {
            try
            {
                return method();
            }
            catch (Exception ex) when (ex is TException)
            {
                handledExcpetionAction?.Invoke();
                throw (TException)Activator.CreateInstance(ex.GetType(), message);
            }
        }
    }
}
