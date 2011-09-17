using System;
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
    [DataContract]
    public class ContainsTemplateFactory : TemplateFactoryBase
    {
        public override string ConstructTemplate(string input)
        {
            if (string.IsNullOrEmpty(input))
                return ",";
            else
                return "," + input + ",";
        }

        [IgnoreDataMember]
        public string Description { get { return "Contains"; } }

        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;
            return this.GetType() == obj.GetType();
        }

        public override int GetHashCode()
        {
            return this.GetType().GetHashCode();
        }
    }
}
