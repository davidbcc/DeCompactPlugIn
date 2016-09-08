using System;

using Slb.Ocean.Core;
using Slb.Ocean.Petrel;
using Slb.Ocean.Petrel.UI;
using Slb.Ocean.Petrel.Workflow;
using Slb.Ocean.Petrel.DomainObject.PillarGrid;

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
            private Horizon _horizon;
            private int _iteration;
            private Grid _grid;
            private string _calName;
            private double _facies;
            private double _coal;
            private double _silt;
            private double _sandstone;
            private double _mudstone;
            private double _dirtyss;
            private double _carbmud;
            private string _name;
           
            #endregion
            public Arguments()
                : this(DataManager.DataSourceManager)
            {             
   
            }

            public Arguments(IDataSourceManager dataSourceManager)
            {
            }

            #region Properties - Input Arguments
            /// <summary>
            /// Grid Property
            /// </summary>
            [Description("Grid Input", " Input Grid Property")]
            public Grid Grid
            {
                internal get
                {
                    return _grid;
                }
                set
                {
                    _grid = value;
                }
            }

            /// <summary>
            /// Horizon. Input value
            /// </summary>
            [Description("Horizon Input","Horizon")]
            public Horizon Horizon{
                internal get
                {
                    return this._horizon;
                }
                set
                {
                    _horizon = value;
                }
            }

            /// <summary>
            /// Iteration x times
            /// </summary>
            public int Interation
            {
                internal get
                {
                    return _iteration;
                }
                set
                {
                   _iteration = value;
                }
            }
            /// <summary>
            /// Calculator Name. Output Value
            /// </summary>
            public string CalName
            {
                 get
                {
                    return _calName;
                }
                internal set
                {
                    _calName= value;
                }
            }

            /// <summary>
            /// Facies
            /// </summary>
            public double Facies { 
                 internal get
                {
                    return _facies;
                }
                 set
                {
                    _facies= value;
                }
            }
            /// <summary>
            /// Coal Param
            /// </summary>
            public double Coal {   internal get
                {
                    return _coal;
                }
                 set
                {
                    _coal= value;
                }
            }
            /// <summary>
            /// Silt Param
            /// </summary>
            public double Silt
            {
                internal get
                {
                    return _silt;
                }
                set
                {
                    _silt = value;
                }
            }
            /// <summary>
            /// Sand Stone
            /// </summary>
            public double SandStone
            {
                internal get
                {
                    return _sandstone;
                }
                set
                {
                    _sandstone = value;
                }
            }
            /// <summary>
            /// Mud Stone
            /// </summary>
            public double MudStone
            {
                internal get
                {
                    return _mudstone;
                }
                set
                {
                    _mudstone = value;
                }
            }
            /// <summary>
            /// Dirty SS
            /// </summary>
            public double DirtySS
            {
                internal get
                {
                    return _dirtyss;
                }
                set
                {
                    _dirtyss = value;
                }
            }
            /// <summary>
            /// Carb Mud
            /// </summary>
            public double CarbMud
            {
                internal get
                {
                    return _carbmud;
                }
                set
                {
                    _carbmud = value;
                }
            }

            /// <summary>
            /// Output variable Name
            /// </summary>
            public string Name
            {
                 get
                {
                    return _name;
                }
                internal set
                {
                    _name = value;
                }
            }
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

        public System.Drawing.Bitmap Image
        {
            get { return PetrelImages.Modules; }
            private set 
            {
                // TODO: implement set
                this.RaiseImageChanged();
            }
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
                get { return "DeCompactionWorkSteps"; }
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