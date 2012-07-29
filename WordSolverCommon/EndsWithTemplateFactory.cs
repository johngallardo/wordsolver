using System;
using System.Runtime.Serialization;

namespace WordSolver
{
    [DataContract]
    public class EndsWithTemplateFactory : TemplateFactoryBase
    {
        public override string ConstructTemplate(string input)
        {
            if (string.IsNullOrEmpty(input))
                return ",";
            else
                return "," + input;
        }

        [IgnoreDataMember]
        public string Description { get { return "Ends With"; } }

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
