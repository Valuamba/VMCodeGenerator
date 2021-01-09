using System;
using System.Collections.Generic;

namespace CodeGenerator
{
    public class Rule
    {
        public List<Type> OrderedList { get; }
        public List<Type> UnorderedList { get; }

        public Rule(List<Type> orderedList = null, List<Type> unorderedList = null)
        {
            OrderedList = orderedList;
            UnorderedList = unorderedList;
        }
    }
}
