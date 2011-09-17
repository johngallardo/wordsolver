using System;
using System.Collections.Generic;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
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
