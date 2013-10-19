using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.IO;
using Bendyline.Base;

namespace Bendyline.TextTransformer
{
    public class TextTransformerEngine
    {       
        private List<ITextCommand> commands;

        public IList<ITextCommand> Commands
        {
            get
            {
                return this.commands;
            }
        }

        public TextTransformerEngine()
        {
            this.commands = new List<ITextCommand>();
        }

        public void LoadConfiguration(String configFilePath)
        {
            XmlDocument xd = new XmlDocument();

            try
            {
                xd.Load(configFilePath);

            }
            catch (DirectoryNotFoundException e)
            {
                Log.Error("Could not find XML file at '{0}'", configFilePath);
                throw e;    
            }

            XmlNode rootNode = xd.SelectSingleNode("Commands");
            foreach (XmlNode xn in rootNode.ChildNodes)
            {
                switch (xn.Name.ToLower())
                {
                    case "replace":
                        ReplaceCommand rc = new ReplaceCommand();

                        rc.Load(xn);

                        this.commands.Add(rc);
                        break;

                    case "striplines":
                        StripLinesCommand slc = new StripLinesCommand();

                        slc.Load(xn);

                        this.commands.Add(slc);
                        break;
                }
            }

        }

        public void Execute(IEnumerable<String> inputFilePaths, String outputFilePath)
        {
            String content = String.Empty;

            foreach (String path in inputFilePaths)
            {
                if (File.Exists(path))
                {
                    content += FileUtilities.GetTextFromFile(path);
                }
            }

            foreach (ITextCommand command in this.commands)
            {
                command.Execute(ref content);
            }

            FileUtilities.SetTextToFile(outputFilePath, content);
        }
    }
}
