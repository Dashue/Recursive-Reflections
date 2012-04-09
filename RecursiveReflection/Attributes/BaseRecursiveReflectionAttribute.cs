using System;

namespace RecursiveReflection.Attributes
{
    public abstract class BaseRecursiveReflectionAttribute : Attribute
    {
        public abstract void Action(object property);
    }
}