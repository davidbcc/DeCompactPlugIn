using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Slb.Ocean.Petrel.Commands;
using Slb.Ocean.Petrel;
using System.Windows.Forms;
using Slb.Ocean.Petrel.Contexts;

namespace DeCompactPlugIn
{
    class DeCompactPlugInCommandHandler : SimpleCommandHandler
    {
        public static string ID = "DeCompactPlugIn.DeCompactPlugInCommand";

        private Form _form;
        /// <summary>
        /// Always enabled.
        /// </summary>
        /// <param name="context">system context</param>
        /// <returns>True to keep it always enabled</returns>
        public override bool CanExecute(Context context)
        {
            return true;
        }
        /// <summary>
        /// Shows the dialog to copy the grid and create properties containing data only along the faults,
        /// derived from original proeprties. 
        /// </summary>
        /// <param name="context">System context</param>
        public override void Execute(Context context)
        {
            // create form if not already created or disposed
            if (_form == null || _form.IsDisposed)
                //_form = new DeCompactWorkStepUI();

            if (!_form.Visible)
                PetrelSystem.ShowModeless(_form);
            else
                _form.Hide();
        }
    }
}
