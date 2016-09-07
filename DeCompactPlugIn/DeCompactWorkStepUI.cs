using System;
using System.Drawing;
using System.Windows.Forms;

using Slb.Ocean.Petrel.Workflow;
using Slb.Ocean.Core;
using Slb.Ocean.Petrel.DomainObject.PillarGrid;
using Slb.Ocean.Petrel;
using Slb.Ocean.Petrel.UI;
using DeCompactionPlugIn.Helpers;
using DeCompactPlugIn.model;

namespace DeCompactPlugIn
{
    /// <summary>
    /// This class is the user interface which forms the focus for the capabilities offered by the process.  
    /// This often includes UI to set up arguments and interactively run a batch part expressed as a workstep.
    /// </summary>
    partial class DeCompactWorkStepUI : UserControl
    {
        #region Private variables
        private DeCompactWorkStep workstep;
        /// <summary>
        /// The argument package instance being edited by the UI.
        /// </summary>
        private DeCompactWorkStep.Arguments _args;
        /// <summary>
        /// Contains the actual underlaying context.
        /// </summary>
        private WorkflowContext context;
        private Grid _grid;
        private Horizon _horizon;
        private DictionaryProperty _facies;
        private int _Layers;
        private Object _silt;
        #endregion

        /// <summary>
        /// Initializes a new instance of the <see cref="DeCompactWorkStepUI"/> class.
        /// </summary>
        /// <param name="workstep">the workstep instance</param>
        /// <param name="args">the arguments</param>
        /// <param name="context">the underlying context in which this UI is being used</param>
        public DeCompactWorkStepUI(DeCompactWorkStep workstep, DeCompactWorkStep.Arguments args, WorkflowContext context)
        {
            InitializeComponent();

            this.workstep = workstep;
            this._args = args;
            this.context = context;
            UiRendering();
        }
        private void UiRendering()
        {
            cancelButton.Image = PetrelImages.Cancel;
            cancelButton.TextImageRelation = TextImageRelation.ImageBeforeText;
            applyButton.Image = PetrelImages.Apply;
            applyButton.TextImageRelation = TextImageRelation.ImageBeforeText;
            OKButton.Image = PetrelImages.OK;
            OKButton.TextImageRelation = TextImageRelation.ImageBeforeText;
            runButton.Image = PetrelImages.DownArrow;
            runButton.TextImageRelation = TextImageRelation.ImageBeforeText;
            testButton.Image = PetrelImages.OK;
            testButton.TextImageRelation = TextImageRelation.ImageBeforeText;
            
        }
        #region Drag Drop events handling
        private void drop_grid_DragDrop(object sender, DragEventArgs e)
        {

            _grid = e.Data.GetData(typeof(object)) as Grid;
            if (_grid == null)
            {
                PetrelLogger.WarnBox("Please select a proper grid");
                PetrelLogger.InfoOutputWindow("Please select a proper grid");
                return;
            }
            presGrid.Text = _grid.Name;
            IImageInfoFactory fact = CoreSystem.GetService<IImageInfoFactory>(_grid);
            presGrid.Image = fact.GetImageInfo(_grid).GetDisplayImage(new ImageInfoContext());
            presGrid.Tag = _grid;
        }


        private void drop_horizon_DragDrop(object sender, DragEventArgs e)
        {

            _horizon = e.Data.GetData(typeof(object)) as Horizon;
            if (_horizon == null)
            {
                PetrelLogger.WarnBox("Please select a proper Horizon");
                PetrelLogger.InfoOutputWindow("Please select a proper Horizon");
                return;
            }
            presHorizon.Text = _horizon.Name;
            IImageInfoFactory fact = CoreSystem.GetService<IImageInfoFactory>(_horizon);
            presHorizon.Image = fact.GetImageInfo(_horizon).GetDisplayImage(new ImageInfoContext());
            presHorizon.Tag = _horizon;
        }

        private void drop_facies_DragDrop(object sender, DragEventArgs e)
        {
            var drop = e.Data.GetData(typeof(object));
            _facies = drop as DictionaryProperty;
            if (_facies != null)
            {

                var nif = CoreSystem.GetService<INameInfoFactory>(_facies);
                this.presentationBox_facies.Text = nif.GetNameInfo(_facies).Name;
                var imgF = CoreSystem.GetService<IImageInfoFactory>(_facies);
                presentationBox_facies.Image = imgF.GetImageInfo(_facies).GetDisplayImage(new ImageInfoContext());
                presentationBox_facies.Tag = _facies;
            }
            else
                PetrelLogger.WarnBox("Please select facet");
            PetrelLogger.InfoOutputWindow("Please select facet");
        }

        private void dropTarget_silt_DragDrop(object sender, DragEventArgs e)
        {
            var drop = e.Data.GetData(typeof(object));
            _silt = drop as Object;
            if (_silt != null)
            {

                var nif = CoreSystem.GetService<INameInfoFactory>(_silt);
                this.presentationBox_facies.Text = nif.GetNameInfo(_silt).Name;
                var imgF = CoreSystem.GetService<IImageInfoFactory>(_silt);
                presentationBox_silt.Image = imgF.GetImageInfo(_silt).GetDisplayImage(new ImageInfoContext());
                presentationBox_silt.Tag = _silt;
            }
            else
                PetrelLogger.WarnBox("Please select facet");
            PetrelLogger.InfoOutputWindow("Please select facet");
        }
        #endregion

        
        #region buttons events handling
        private void cancelButton_Click(object sender, EventArgs e)
        {
            var findForm = FindForm();
            if (findForm != null) findForm.Close();
        }
        
        private void OKButton_Click(object sender, EventArgs e)
        {
            var findForm = FindForm();
            if (findForm != null) findForm.Close();
        }
        private void applyButton_Click(object sender, EventArgs e)
        {

        }

        private void runButton_Click(object sender, EventArgs e)
        {
            var args = new WorkStepArgument();

            args.Facies = _facies;
            args.Grid = _grid;
            args.Horizon = _horizon;
            args.iteration = txtnolayers.Text;
            //args.Coal = Convert.ToDouble(presentationBox_coal.Text);
            //args.Silt = Convert.ToDouble(presentationBox_silt.Text);
            //args.SandStone = Convert.ToDouble(presentationBox_sandstone.Text);
            //args.MudStone = Convert.ToDouble(presentationBox_mudstone.Text);
            //args.DirtySS = Convert.ToDouble(presentationBox_dirtyss.Text);
            //args.CarbMud = Convert.ToDouble(presentationBox_carbmud.Text);
            Grid grid = presGrid.Tag as Grid;
            if (grid != null)
            {
                CannedWorkflowHelper.Instance.RunWorkflow(args);
               
            }
            else
            {
                PetrelLogger.WarnBox("Please provide grid to copy");
                return;
            }
        }
        #endregion

    

      

 

     

        
    }
}
