using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace WordSolver
{
    [
    DataContract,
    KnownType(typeof(ContainsTemplateFactory)),
    KnownType(typeof(EndsWithTemplateFactory)),
    KnownType(typeof(StartsWithTemplateFactory))
    ]
    public abstract class TemplateFactoryBase
    {
        public abstract string ConstructTemplate(string input);
    }

    public class TemplateFactoryCollection : List<TemplateFactoryBase>
    {
        public TemplateFactoryCollection()
        {
            Capacity = 3;
            Add(new StartsWithTemplateFactory());
            Add(new ContainsTemplateFactory());
            Add(new EndsWithTemplateFactory());            
        }
    }
}
