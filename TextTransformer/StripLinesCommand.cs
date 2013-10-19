using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using Bendyline.Base;

namespace Bendyline.TextTransformer
{
    public class StripLinesCommand : ITextCommand
    {
        private String containing;

        public String Containing
        {
            get
            {
                return this.containing;
            }

            set
            {
                this.containing = value;
            }
        }

        public void Load(XmlNode node)
        {
            XmlNode attributeNode = node.Attributes.GetNamedItem("Containing");

            if (attributeNode != null)
            {
                this.containing = attributeNode.Value;
            }
        }

        public void Execute(ref String content)
        {
            if (this.containing == null)
            {
                Log.Error("'Containing' parameter was not specified for a replace command.");
                return;
            }


            int i = content.IndexOf(this.containing);

            while (i >= 0)
            {
                int lastNewLine = content.LastIndexOf("\n", i);
                int nextNewNewline = content.IndexOf("\n", i);

                if (lastNewLine < 0)
                {
                    lastNewLine = 0;
                }
                else
                {
                    lastNewLine++;
                }

                if (nextNewNewline < 0)
                {
                    nextNewNewline = content.Length - 1;
                }
                else
                {
                    nextNewNewline++;
                }

                content = content.Substring(0, lastNewLine) + content.Substring(nextNewNewline, content.Length - nextNewNewline);

                i = content.IndexOf(this.containing, lastNewLine + 1);
            }
        }
    }
}
