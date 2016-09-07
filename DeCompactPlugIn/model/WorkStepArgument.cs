using Slb.Ocean.Core;
using Slb.Ocean.Petrel;
using Slb.Ocean.Petrel.DomainObject.Analysis;
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
        public Function Silt { get; set; }
        /// <summary>
        /// Coal
        /// </summary>
        public Function Coal { get; set; }
        /// <summary>
        /// Sand Stone
        /// </summary>
        public Function SandStone { get; set; }
        /// <summary>
        /// Mud Stone
        /// </summary>
        public Function MudStone { get; set; }
        /// <summary>
        /// Dirty SS
        /// </summary>
        public Function DirtySS { get; set; }
        /// <summary>
        /// Carb Mud
        /// </summary>
        public Function CarbMud { get; set; }

        /// <summary>
        /// Iteration Looping
        /// </summary>
        public string iteration { get; set; }
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
