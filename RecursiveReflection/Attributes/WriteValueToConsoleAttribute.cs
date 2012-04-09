
using System;

namespace RecursiveReflection.Attributes
{
    public class WriteValueToConsoleAttribute : BaseRecursiveReflectionAttribute
    {
        public override void Action(object property)
        {
            Console.WriteLine(property);
        }
    }
}