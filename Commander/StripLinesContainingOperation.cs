using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bendyline.Base;
using System.Xml.Serialization;

namespace Bendyline.Utilities
{
    [XmlType(TypeName = "StripLinesContaining")]
    public class StripLinesContainingOperation : SerializableObject
    {
        private String text;

        [XmlAttribute]
        public String Text
        {
            get
            {
                return this.text;
            }

            set
            {
                this.text = value;
            }
        }


        public void Execute(ref String contents)
        {
            int i = contents.IndexOf(this.Text);

            while (i >= 0)
            {
                int previousEol = contents.LastIndexOf("\n", i);

                if (previousEol < 0)
                {
                    previousEol = -1;
                }

                int nextEol = contents.IndexOf("\r", i);

                if (nextEol < 0)
                {
                    nextEol = contents.Length;
                }

                contents = contents.Substring(0, previousEol + 1) + contents.Substring(nextEol, contents.Length - nextEol);

                i = contents.IndexOf(this.Text, previousEol + 1);
            }
        }
    }
}
