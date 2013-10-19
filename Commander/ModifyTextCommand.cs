using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bendyline.Base;
using System.IO;

namespace Bendyline.Utilities
{
    public class ModifyTextCommand : SourceFileTargetFileCommand
    {
        private String operationsFile;

        public String OperationsFile
        {
            get { return this.operationsFile; }
            set { this.operationsFile = value; }
        }

        public override string Id
        {
            get { return "modifytext"; }
        }

        public override bool Validate()
        {
            return base.Validate();

            if (this.operationsFile == null)
            {
                this.Output("An operations was not specified. Use the -operationsfile argument to specify an operations file.");

                return false;
            }

            if (!File.Exists(this.operationsFile))
            {
                this.Output("The operations file '{0}' does not exist.", this.operationsFile);

                return false;
            }
        }

        public override void Execute()
        {
            String operationContents = FileUtilities.GetTextFromFile(this.OperationsFile);

            OperationSet os = new OperationSet();
            os.Xml = operationContents;


            String fileContents = FileUtilities.GetTextFromFile(this.SourceFile);

            fileContents = os.Modify(fileContents);


            FileUtilities.SetTextToFile(this.TargetFile, fileContents);
        }
    }
}
