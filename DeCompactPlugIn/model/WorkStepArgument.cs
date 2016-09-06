using Slb.Ocean.Core;
using Slb.Ocean.Petrel;
using Slb.Ocean.Petrel.DomainObject.PillarGrid;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeCompactPlugIn.model
{
    /// <summary>
    /// Work Step Model, parameters object. Passing in to Workflow.
    /// </summary>
     [Serializable]
    public class WorkStepArgument
    {
        #region private variables
         private Droid m_droid = Droid.Empty;
        #endregion

        /// <summary>
        /// Grid Property. Identifiable object.
        /// </summary>
        public Grid Grid { get; set; }
        /// <summary>
        /// Horizontal Property. Identifiable object.
        /// </summary>
        public Horizon Horizon { get; set; }
        /// <summary>
        /// Facies
        /// </summary>
        public DictionaryProperty Facies { get; set; }
        /// <summary>
        /// Silt
        /// </summary>
        public double Silt { get; set; }
        /// <summary>
        /// Coal
        /// </summary>
        public double Coal { get; set; }
        /// <summary>
        /// Sand Stone
        /// </summary>
        public double SandStone { get; set; }
        /// <summary>
        /// Mud Stone
        /// </summary>
        public double MudStone { get; set; }
        /// <summary>
        /// Dirty SS
        /// </summary>
        public double DirtySS { get; set; }
        /// <summary>
        /// Carb Mud
        /// </summary>
        public double CarbMud { get; set; }

        /// <summary>
        /// Iteration Looping
        /// </summary>
        public int iteration { get; set; }
        /// <summary>
        /// Name , Output value
        /// </summary>
        public string Name { get; set; }


        #region Constructor
        public WorkStepArgument()
        {
          
            PetrelLogger.InfoOutputWindow("*** WorkStepArgument Initialized ***");
        }
        #endregion
    }
}
