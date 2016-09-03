using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Slb.Ocean.Petrel.Commands;
using Slb.Ocean.Petrel;

namespace DeCompactPlugIn
{
    class DeCompactPlugInCommandHandler : SimpleCommandHandler
    {
        public static string ID = "DeCompactPlugIn.DeCompactPlugInCommand";

        #region SimpleCommandHandler Members

        public override bool CanExecute(Slb.Ocean.Petrel.Contexts.Context context)
        { 
            return true;
        }

        public override void Execute(Slb.Ocean.Petrel.Contexts.Context context)
        {          
            //TODO: Add command execution logic here
            PetrelLogger.InfoOutputWindow(string.Format("{0} clicked", @"DeCompactPlugIn" ));
        }
    
        #endregion
    }
}
