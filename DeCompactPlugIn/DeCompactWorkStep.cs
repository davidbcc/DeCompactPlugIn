using System;

using Slb.Ocean.Core;
using Slb.Ocean.Petrel;
using Slb.Ocean.Petrel.UI;
using Slb.Ocean.Petrel.Workflow;
using Slb.Ocean.Petrel.DomainObject.PillarGrid;
using Slb.Ocean.Petrel.DomainObject.Analysis;
using System.Drawing;
using System.Reflection;

namespace DeCompactPlugIn
{
    /// <summary>
    /// This class contains all the methods and subclasses of the DeCompactWorkStep.
    /// Worksteps are displayed in the workflow editor.
    /// </summary>
    class DeCompactWorkStep : Workstep<DeCompactWorkStep.Arguments>, IExecutorSource, IAppearance, IDescriptionSource
    {
        #region Overridden Workstep methods

        /// <summary>
        /// Creates an empty Argument instance
        /// </summary>
        /// <returns>New Argument instance.</returns>

        protected override DeCompactWorkStep.Arguments CreateArgumentPackageCore(IDataSourceManager dataSourceManager)
        {
            return new Arguments(dataSourceManager);
        }
        /// <summary>
        /// Copies the Arguments instance.
        /// </summary>
        /// <param name="fromArgumentPackage">the source Arguments instance</param>
        /// <param name="toArgumentPackage">the target Arguments instance</param>
        protected override void CopyArgumentPackageCore(Arguments fromArgumentPackage, Arguments toArgumentPackage)
        {
            DescribedArgumentsHelper.Copy(fromArgumentPackage, toArgumentPackage);
        }

        /// <summary>
        /// Gets the unique identifier for this Workstep.
        /// </summary>
        protected override string UniqueIdCore
        {
            get
            {
                return "60dc69ac-411a-48bc-a0b1-488b341e0bf1";
            }
        }
        #endregion

        #region IExecutorSource Members and Executor class

        /// <summary>
        /// Creates the Executor instance for this workstep. This class will do the work of the Workstep.
        /// </summary>
        /// <param name="argumentPackage">the argumentpackage to pass to the Executor</param>
        /// <param name="workflowRuntimeContext">the context to pass to the Executor</param>
        /// <returns>The Executor instance.</returns>
        public Slb.Ocean.Petrel.Workflow.Executor GetExecutor(object argumentPackage, WorkflowRuntimeContext workflowRuntimeContext)
        {
            return new Executor(argumentPackage as Arguments, workflowRuntimeContext);
        }

        public class Executor : Slb.Ocean.Petrel.Workflow.Executor
        {
            Arguments arguments;
            WorkflowRuntimeContext context;

            public Executor(Arguments arguments, WorkflowRuntimeContext context)
            {
                this.arguments = arguments;
                this.context = context;
            }

            public override void ExecuteSimple()
            {
                // TODO: Implement the workstep logic here.
            }
        }

        #endregion

        /// <summary>
        /// ArgumentPackage class for DeCompactWorkStep.
        /// Each public property is an argument in the package.  The name, type and
        /// input/output role are taken from the property and modified by any
        /// attributes applied.
        /// </summary>
        public class Arguments : DescribedArgumentsByReflection
        {
            #region private variables
            [NonSerialized]
            private Horizon _horizon;
            [NonSerialized]
            private int _iteration;
            [NonSerialized]
            private Grid _grid;
            [NonSerialized]
            private string _calName;
            [NonSerialized]
            private Function _facies;
            [NonSerialized]
            private Function _coal;
            [NonSerialized]
            private Function _silt;
            [NonSerialized]
            private Function _sandstone;
            [NonSerialized]
            private Function _mudstone;
            [NonSerialized]
            private Function _dirtyss;
            [NonSerialized]
            private Function _carbmud;
            [NonSerialized]
            private string _name;
           
            #endregion
            public Arguments()
                : this(DataManager.DataSourceManager)
            {             
   
            }

            public Arguments(IDataSourceManager dataSourceManager)
            {
                this._iteration = 1;
            }

            #region Properties - Input Arguments
            /// <summary>
            /// Grid Property. Identifiable object.
            /// </summary>
            [Description("The Grid", "The Grid Object")]
            public Grid Grid { get; set; }
            /// <summary>
            /// Horizontal Property. Identifiable object.
            /// </summary>
             [Description("The Horizon Object", "The Horizon Object")]
            public Horizon Horizon { get; set; }
            /// <summary>
            /// Facies
            /// </summary>
            [Description("The Facies Dictionary Property", "The Facies Dictionary Property")]
            public DictionaryProperty Facies { get; set; }
            /// <summary>
            /// Silt
            /// </summary>
            [Description("The Silt Function", "The Silt Function")]
            public Function Silt { get; set; }
            /// <summary>
            /// Coal
            /// </summary>
             [Description("The Coal Function", "The Coal Function")]
            public Function Coal { get; set; }
            /// <summary>
            /// Sand Stone
            /// </summary>
             [Description("The Sandstone Function", "The Sandstone Function")]
            public Function SandStone { get; set; }
            /// <summary>
            /// Mud Stone
            /// </summary>
             [Description("The Mudstone Function", "The Mudstone Function")]
            public Function MudStone { get; set; }
            /// <summary>
            /// Dirty SS
            /// </summary>
             [Description("The Silt Function", "The Silt Function")]
            public Function DirtySS { get; set; }
            /// <summary>
            /// Carb Mud
            /// </summary>
             [Description("The Carbmud Function", "The Carbmud Function")]
            public Function CarbMud { get; set; }

            /// <summary>
            /// Iteration Looping
            /// </summary>
            [Description("This is the number of Iteration", "Default set to 1")]
            public int iteration { get; set; }
            /// <summary>
            /// Name , Output value
            /// </summary>
            public string Name { get; set; }
            #endregion
        }
    
        #region IAppearance Members
        public event EventHandler<TextChangedEventArgs> TextChanged;
        protected void RaiseTextChanged()
        {
            if (this.TextChanged != null)
                this.TextChanged(this, new TextChangedEventArgs(this));
        }

        public string Text
        {
            get { return Description.Name; }
            private set 
            {
                // TODO: implement set
                this.RaiseTextChanged();
            }
        }

        public event EventHandler<ImageChangedEventArgs> ImageChanged;
        protected void RaiseImageChanged()
        {
            if (this.ImageChanged != null)
                this.ImageChanged(this, new ImageChangedEventArgs(this));
        }

        //public System.Drawing.Bitmap Image
        //{
        //    get { return PetrelImages.Modules; }
        //    private set 
        //    {
        //        // TODO: implement set
        //        this.RaiseImageChanged();
        //    }
        //}
        public Bitmap Image
        {
            get { return new Bitmap(Assembly.GetExecutingAssembly().GetManifestResourceStream(Assembly.GetExecutingAssembly().GetName().Name + ".Resources." + "logo.bmp")); ; }
        }
        #endregion

        #region IDescriptionSource Members

        /// <summary>
        /// Gets the description of the DeCompactWorkStep
        /// </summary>
        public IDescription Description
        {
            get { return DeCompactWorkStepDescription.Instance; }
        }

        /// <summary>
        /// This singleton class contains the description of the DeCompactWorkStep.
        /// Contains Name, Shorter description and detailed description.
        /// </summary>
        public class DeCompactWorkStepDescription : IDescription
        {
            /// <summary>
            /// Contains the singleton instance.
            /// </summary>
            private  static DeCompactWorkStepDescription instance = new DeCompactWorkStepDescription();
            /// <summary>
            /// Gets the singleton instance of this Description class
            /// </summary>
            public static DeCompactWorkStepDescription Instance
            {
                get { return instance; }
            }

            #region IDescription Members

            /// <summary>
            /// Gets the name of DeCompactWorkStep
            /// </summary>
            public string Name
            {
                get { return "Facies Decompactor"; }
            }
            /// <summary>
            /// Gets the short description of DeCompactWorkStep
            /// </summary>
            public string ShortDescription
            {
                get { return "This is the Canned Workflow to perform decompaction."; }
            }
            /// <summary>
            /// Gets the detailed description of DeCompactWorkStep
            /// </summary>
            public string Description
            {
                get { return "This is the Canned Workflow to perform decompaction"; }
            }

            #endregion
        }
        #endregion

        public class UIFactory : WorkflowEditorUIFactory
        {
            /// <summary>
            /// This method creates the dialog UI for the given workstep, arguments
            /// and context.
            /// </summary>
            /// <param name="workstep">the workstep instance</param>
            /// <param name="argumentPackage">the arguments to pass to the UI</param>
            /// <param name="context">the underlying context in which the UI is being used</param>
            /// <returns>a Windows.Forms.Control to edit the argument package with</returns>
            protected override System.Windows.Forms.Control CreateDialogUICore(Workstep workstep, object argumentPackage, WorkflowContext context)
            {
                return new DeCompactWorkStepUI((DeCompactWorkStep)workstep, (Arguments)argumentPackage, context);
            }
        }
    }
}