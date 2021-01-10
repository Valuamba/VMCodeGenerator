using System;
using System.Collections.Generic;

namespace CodeGenerator.Utilities
{
    public class ConsoleRetrier
    {
        public bool DoWithRetry(Func<bool> function, string helpMessage)
        {
            var result = false;

            while (!result)
            {
                result = function();
                if(!result)
                {
                    Console.WriteLine(helpMessage);
                }
            }
            return result;
        }
    }
}
