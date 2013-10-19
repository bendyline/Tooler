using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bendyline.Base;
using System.Xml.Serialization;

namespace Bendyline.Utilities
{
    [XmlType(TypeName = "Replace")]
    public class ReplaceOperation : SerializableObject
    {
        private String from;
        private String to;

        [XmlAttribute]
        public String From
        {
            get
            {
                return this.from;
            }

            set
            {
                this.from = value;
            }
        }

        [XmlAttribute]
        public String To
        {
            get
            {
                return this.to;
            }

            set
            {
                this.to = value;
            }
        }

        public void Execute(ref String file)
        {
            file = file.Replace(from, to);
        }
    }
}
