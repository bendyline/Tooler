using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;

namespace Bendyline.Utilities
{
    public class Program
    {

        static void Main(string[] args)
        {
            ExecuteCommand(args);
        }

        private static void ExecuteCommand(string[] args)
        {
            List<String> restOfArguments = new List<string>();
            ICommand command = null;
            bool isHelpMode = false;

            for (int i=0; i<args.Length - 1; i++)
            {
                String normalizedArg = GetNormalizedArgument(args[i]);

                if (normalizedArg.Length > 1)
                {
                    if (normalizedArg == "/o" || normalizedArg == "/h")
                    {
                        if (normalizedArg == "/h")
                        {
                            isHelpMode = true;
                        }

                        i++;

                        switch (GetNormalizedArgument(args[i]))
                        {
                            case "createdatabasefromxlsx":
                                command = new CreateDatabaseFromXlsx();
                                break;
                            case "modifytext":
                                command = new ModifyTextCommand();
                                break;
                        }
                    }
                }
            }

            if (command == null)
            {
                OutputUsage();

                return;
            }

            if (isHelpMode)
            {
                OutputHeader();

                Console.WriteLine("Additional help for the {0} command:", command.Id);

                command.OutputHelp();
                return;
            }


            for (int i = 0; i < args.Length - 1; i++)
            {
                String arg = args[i];
                String normalizedArg = GetNormalizedArgument(arg);

                if (normalizedArg.Length > 1)
                {
                    switch (normalizedArg)
                    {
                        case "/sourcefile":
                            i++;
                            if (command is SourceFileTargetFileCommand)
                            {
                                ((SourceFileTargetFileCommand)command).SourceFile = args[i];
                            }
                            break;

                        case "/targetfile":
                            i++;
                            if (command is SourceFileTargetFileCommand)
                            {
                                ((SourceFileTargetFileCommand)command).TargetFile = args[i];
                            }
                            break;

                        case "/operationsfile":
                            i++;
                            if (command is ModifyTextCommand)
                            {
                                ((ModifyTextCommand)command).OperationsFile = args[i];
                            }
                            break;

                        case "/startlat":
                            i++;
                            if (command is LatLongRectangleCommand)
                            {
                                ((LatLongRectangleCommand)command).StartLatitude = Convert.ToInt32(args[i]);
                            }
                            break;

                        case "/endlat":
                            i++;
                            if (command is LatLongRectangleCommand)
                            {
                                ((LatLongRectangleCommand)command).EndLatitude = Convert.ToInt32(args[i]);
                            }
                            break;

                        case "/startlong":
                            i++;
                            if (command is LatLongRectangleCommand)
                            {
                                ((LatLongRectangleCommand)command).StartLongitude = Convert.ToInt32(args[i]);
                            }
                            break;

                        case "/endlong":
                            i++;
                            if (command is LatLongRectangleCommand)
                            {
                                ((LatLongRectangleCommand)command).EndLongitude = Convert.ToInt32(args[i]);
                            }
                            break;

                    }
                }
            }

            if (!command.Validate())
            {
                return;
            }

            command.Execute();
        }

        private static void OutputHeader()
        {
            Console.WriteLine(@"Wargame Utilities. Copyright (c) 2011 Mike bendyline

");
        }

        private static void OutputUsage()
        {
            OutputHeader();

            Console.WriteLine(@"Usage:
    -c <command> -<command> <command arguments>

For help and parameters for a specific command:
    -h <command>

Commands:
    createdatabasefromxlsx:  Creates a unit database from an Excel spreadsheet.
    modifytext:  Modifies a text file.

");
        }

        private static String GetNormalizedArgument(String argument)
        {
            String normalizedArg = argument.ToLower();

            if (normalizedArg.StartsWith("-"))
            {
                normalizedArg = "/" + normalizedArg.Substring(1);
            }

            return normalizedArg;
        }
    }
}
