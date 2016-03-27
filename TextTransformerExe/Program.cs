using System;
using System.Collections.Generic;
using System.Linq;
using Bendyline.Base;
using Bendyline.TextTransformer;

namespace Bendyline.TextTransformer.exe
{
    public static class Program
    {
        static void Main(string[] args)
        {
            CommandLineLogger cll = new CommandLineLogger();
            cll.Initialize();

            TextTransformerEngine tte = new TextTransformerEngine();

            List<String> inputFiles = new List<string>();
            String outputFile = null;
            String configFile = null;

            for (int i = 0; i < args.Length; i++)
            {
                String arg = args[i];
                String argCanon = arg.ToLower();

                if (argCanon.StartsWith("/"))
                {
                    argCanon = "-" + argCanon.Substring(1);
                }

                switch (argCanon)
                {
                    case "-?":
                    case "-h":
                    case "-help":
                        OutputUsage();
                        break;

                    case "-if":
                    case "-inputfile":
                    case "-inputfiles":
                        i++;
                        while (i < args.Length && args[i][0] != '-' && args[i][0] != '/')
                        {
                            inputFiles.Add(args[i]);
                            i++;
                        }
                        i--;
                        break;

                    case "-of":
                    case "-outputfile":
                        if (i < args.Length - 1)
                        {
                            i++;
                            outputFile = args[i];
                        }
                        break;

                    case "-cf":
                    case "-configfile":
                        if (i < args.Length - 1)
                        {
                            i++;
                            configFile = args[i];
                        }
                        break;

                }
            }

            if (inputFiles.Count == 0)
            {
                Console.WriteLine("No input file was specified.\r\n");

                OutputUsage();

                return;
            }

            if (outputFile == null)
            {
                Console.WriteLine("No output file was specified.");

                OutputUsage();

                return;
            }

            if (configFile == null)
            {
                Console.WriteLine("No config file was specified.");

                OutputUsage();

                return;
            }
           
            tte.LoadConfiguration(configFile);

            tte.Execute(inputFiles, outputFile);
        }

        private static void OutputUsage()
        {
            Console.WriteLine(@"Usage: bltemplate -inputfolder <folder for input> -outputfolder <folder for output>

    inputfile: Path to a text file to folder in.  Can be used mutltiple times to specify multiple files to concatenate.
    configfile: Path to configuration file.
    outputfile: Path to export text file.");
        }
    }
}
