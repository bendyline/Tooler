using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Bendyline.Base;

namespace Bendyline.Utilities
{
    public abstract class LatLongRectangleCommand : CommandBase
    {
        private int startLat =  -300;
        private int endLat = -300;
        private int startLong = -300;
        private int endLong = -300;

        public int StartLatitude
        {
            get { return this.startLat; }
            set { this.startLat = value; }
        }

        public int EndLatitude
        {
            get { return this.endLat; }
            set { this.endLat = value; }
        }

        public int StartLongitude
        {
            get { return this.startLong; }
            set { this.startLong = value; }
        }

        public int EndLongitude
        {
            get { return this.endLat; }
            set { this.endLat = value; }
        }

        public override bool Validate()
        {
            if (this.startLat < -180 || startLat > 180)
            {
                this.Output("Starting Latitude was not specified or specified incorrectly. Use the -startLat argument to specify a value between -180 and 180.");

                return false;
            }

            if (this.endLat < -180 || endLat > 180)
            {
                this.Output("Ending Latitude was not specified or specified incorrectly. Use the -endLat argument to specify a value between -180 and 180.");

                return false;
            }

            if (this.startLong < -180 || startLong > 180)
            {
                this.Output("Starting Longitude was not specified or specified incorrectly. Use the -startLong argument to specify a value between -180 and 180.");

                return false;
            }

            if (this.endLong < -180 || endLong > 180)
            {
                this.Output("Ending Longitude was not specified or specified incorrectly. Use the -endong argument to specify a value between -180 and 180.");

                return false;
            }

            return true;
        }
    }
}
