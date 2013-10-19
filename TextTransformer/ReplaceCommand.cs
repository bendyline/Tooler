using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using Bendyline.Base;

namespace Bendyline.TextTransformer
{
    public class ReplaceCommand : ITextCommand
    {
        private String from;
        private String to;

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

        public void Load(XmlNode node)
        {
            XmlNode attributeNode = node.Attributes.GetNamedItem("From");

            if (attributeNode != null)
            {
                this.from = attributeNode.Value;
            }

            attributeNode = node.Attributes.GetNamedItem("To");

            if (attributeNode != null)
            {
                this.to = attributeNode.Value;
            }
        }

        public void Execute(ref String content)
        {
            if (this.from == null)
            {
                Log.Error("'From' parameter was not specified for a replace command.");
                return;
            }

            if (this.to == null)
            {
                Log.Error("'To' parameter was not specified for a replace command.");
                return;
            }

            content = content.Replace(from, to);
        }
    }
}
