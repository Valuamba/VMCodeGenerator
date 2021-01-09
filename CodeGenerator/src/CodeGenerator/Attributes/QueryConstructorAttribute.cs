using System;
using System.Collections.Generic;
using System.Text;

namespace CodeGenerator.Attributes
{
    public class QueryConstructorAttribute :  Attribute
    {
        public readonly string ReplacedName;

        public QueryConstructorAttribute(string replacedName)
        {
            ReplacedName = replacedName;
        }
    }
}
