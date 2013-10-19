using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bendyline.Base;
using System.Xml.Serialization;

namespace Bendyline.Utilities
{
    public class OperationSet : SerializableObject
    {
        private List<ReplaceOperation> replaces;
        private List<StripLinesContainingOperation> stripLinesContainings;

        [XmlArray("Replaces")]
        [XmlArrayItem("Replace", Type = typeof(StripLinesContainingOperation))]
        public IList<ReplaceOperation> Replaces
        {
            get
            {
                if (this.replaces == null)
                {
                    this.replaces = new List<ReplaceOperation>();
                }

                return this.replaces;
            }
        }

        [XmlArray("StripLinesContainings")]
        [XmlArrayItem("StripLinesContaining", Type = typeof(StripLinesContainingOperation))]
        public IList<StripLinesContainingOperation> StripLinesContainings
        {
            get
            {
                if (this.stripLinesContainings  == null)
                {
                    this.stripLinesContainings = new List<StripLinesContainingOperation>();
                }

                return this.stripLinesContainings;
            }
        }

        public OperationSet()
        {

        }

        public String Modify(String text)
        {
            foreach (ReplaceOperation roper in this.Replaces)
            {
                roper.Execute(ref text);
            }

            foreach (StripLinesContainingOperation oper in this.StripLinesContainings)
            {
                oper.Execute(ref text);
            }

            return text;
        }
    }
}
