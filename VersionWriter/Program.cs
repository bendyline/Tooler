using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Bendyline.Base;

namespace Bendyline.Tools.VersionWriter
{
    public class Program
    {
        static void Main(string[] args)
        {
            if (args.Length != 2)
            {
                Console.WriteLine("Usage: versionwriter <input file> <output file>\r\n");
                return;
            }

            if (!File.Exists(args[0]))
            {
                Console.WriteLine(String.Format("Could not find file '{0}'", args[0]));
                return;
            }

            String targetDirectoryPath = FileUtilities.GetDirectoryPathFromFilePath(args[1]);

            if (!String.IsNullOrEmpty(targetDirectoryPath) && !Directory.Exists(targetDirectoryPath))
            {
                Directory.CreateDirectory(targetDirectoryPath);
            }

            int majorVersion = 1;
            int minorVersion = 0;
            int buildNumber = 0;
            int revisionNumber = 0;

            DateTime dtNow = DateTime.Now;

            buildNumber = buildNumber + dtNow.Month;
            buildNumber = buildNumber + ((dtNow.Year - 2020) * 12);
            buildNumber *= 100;

            buildNumber += dtNow.Day;

            String text = FileUtilities.GetTextFromFile(args[0]);

            revisionNumber = (dtNow.Hour * 100) + dtNow.Minute;

            text = text.Replace("%FULLVERSION%", String.Format("{0}.{1}.{2}.{3}", majorVersion, minorVersion, buildNumber, revisionNumber));
            text = text.Replace("%REVISIONNUMBER%", Convert.ToString(revisionNumber));
            text = text.Replace("%BUILDNUMBER%", Convert.ToString(buildNumber));

            FileUtilities.SetTextToFile(args[1], text);
        }
    }
}
