using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace Bendyline.TextTransformer
{
    public interface ITextCommand
    {
        void Load(XmlNode node);
        void Execute(ref String content);

    }
}
