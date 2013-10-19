using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Bendyline.Base;

namespace Bendyline.Utilities
{
    public abstract class SourceFileTargetFileCommand : CommandBase
    {
        private String sourceFile;
        private String targetFile;

        public String SourceFile
        {
            get { return this.sourceFile; }
            set { this.sourceFile = value; }
        }

        public String TargetFile
        {
            get { return this.targetFile; }
            set { this.targetFile = value; }
        }
        
        public override bool Validate()
        {
            if (this.sourceFile == null)
            {
                this.Output("A source file was not specified. Use the -sourcefile argument to specify an input file.");

                return false;
            }

            if (this.targetFile == null)
            {
                this.Output("A target file was not specified. Use the -targetfile argument to specify an output file.");

                return false;
            }

            if (!File.Exists(this.sourceFile))
            {
                this.Output("The source file '{0}' does not exist.", this.sourceFile);

                return false;
            }


            if (this.sourceFile == this.targetFile)
            {
                this.Output("Source file cannot be the same as the target file.");

                return false;
            }

            if (this.targetFile.Contains("\\"))
            {
                String targetDir = FileUtilities.GetDirectoryPathFromFilePath(this.targetFile);

                if (!Directory.Exists(targetDir))
                {
                    this.Output("The directory for the target file '{0}' does not exist.", this.targetFile);

                    return false;
                }
            }

            return true;
        }
    }
}
