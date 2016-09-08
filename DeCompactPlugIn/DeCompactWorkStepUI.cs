using System;
using System.Drawing;
using System.Windows.Forms;

using Slb.Ocean.Petrel.Workflow;
using Slb.Ocean.Core;
using Slb.Ocean.Petrel.DomainObject.PillarGrid;
using Slb.Ocean.Petrel.DomainObject.Analysis;
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
        private Function _silt;
        private Function _sandstone;
        private Function _mudstone;
        private Function _coal;
        private Function _dirtyss;
        private Function _carbmud;
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
        // Grid
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

        // Horizon

        private void drop_horizon_DragDrop(object sender, DragEventArgs e)
        {
            var drop = e.Data.GetData(typeof(object));
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
            _silt = drop as Function;
            if (_silt != null)
            {

                var nif = CoreSystem.GetService<INameInfoFactory>(_silt);
                this.presentationBox_silt.Text = nif.GetNameInfo(_silt).Name;
                var imgS = CoreSystem.GetService<IImageInfoFactory>(_silt);
                presentationBox_silt.Image = imgS.GetImageInfo(_silt).GetDisplayImage(new ImageInfoContext());
                presentationBox_silt.Tag = _silt;
            }
            else
                PetrelLogger.WarnBox("Please select silt function");
            PetrelLogger.InfoOutputWindow("Please select silt function");
        }

        private void dropTarget_sandstone_DragDrop(object sender, DragEventArgs e)
        {
            var drop = e.Data.GetData(typeof(object));
            _sandstone = drop as Function;
            if (_sandstone != null)
            {

                var nif = CoreSystem.GetService<INameInfoFactory>(_sandstone);
                this.presentationBox_sandstone.Text = nif.GetNameInfo(_sandstone).Name;
                var imgS = CoreSystem.GetService<IImageInfoFactory>(_sandstone);
                presentationBox_sandstone.Image = imgS.GetImageInfo(_sandstone).GetDisplayImage(new ImageInfoContext());
                presentationBox_sandstone.Tag = _sandstone;
            }
            else
                PetrelLogger.WarnBox("Please select sandstone");
            PetrelLogger.InfoOutputWindow("Please select sandstone");
        }

        private void dropTarget_mudstone_DragDrop(object sender, DragEventArgs e)
        {
            var drop = e.Data.GetData(typeof(object));
            _mudstone = drop as Function;
            if (_mudstone != null)
            {

                var nif = CoreSystem.GetService<INameInfoFactory>(_mudstone);
                this.presentationBox_mudstone.Text = nif.GetNameInfo(_mudstone).Name;
                var imgS = CoreSystem.GetService<IImageInfoFactory>(_mudstone);
                presentationBox_mudstone.Image = imgS.GetImageInfo(_mudstone).GetDisplayImage(new ImageInfoContext());
                presentationBox_mudstone.Tag = _mudstone;
            }
            else
                PetrelLogger.WarnBox("Please select sandstone");
            PetrelLogger.InfoOutputWindow("Please select sandstone");
        }
        private void dropTarget_coal_DragDrop(object sender, DragEventArgs e)
        {
            var drop = e.Data.GetData(typeof(object));
            _coal = drop as Function;
            if (_coal != null)
            {

                var nif = CoreSystem.GetService<INameInfoFactory>(_coal);
                this.presentationBox_coal.Text = nif.GetNameInfo(_coal).Name;
                var imgS = CoreSystem.GetService<IImageInfoFactory>(_coal);
                presentationBox_coal.Image = imgS.GetImageInfo(_coal).GetDisplayImage(new ImageInfoContext());
                presentationBox_coal.Tag = _coal;
            }
            else
                PetrelLogger.WarnBox("Please select coal");
            PetrelLogger.InfoOutputWindow("Please select coal");
        }


        private void dropTarget_dirtyss_DragDrop(object sender, DragEventArgs e)
        {
            var drop = e.Data.GetData(typeof(object));
            _dirtyss= drop as Function;
            if (_dirtyss != null)
            {

                var nif = CoreSystem.GetService<INameInfoFactory>(_dirtyss);
                this.presentationBox_coal.Text = nif.GetNameInfo(_dirtyss).Name;
                var imgS = CoreSystem.GetService<IImageInfoFactory>(_dirtyss);
                presentationBox_dirtyss.Image = imgS.GetImageInfo(_dirtyss).GetDisplayImage(new ImageInfoContext());
                presentationBox_dirtyss.Tag = _dirtyss;
            }
            else
                PetrelLogger.WarnBox("Please select dirtyss");
            PetrelLogger.InfoOutputWindow("Please select dirtyss");
        }

        private void dropTarget_carbmud_DragDrop(object sender, DragEventArgs e)
        {
            var drop = e.Data.GetData(typeof(object));
            _carbmud = drop as Function;
            if (_dirtyss != null)
            {

                var nif = CoreSystem.GetService<INameInfoFactory>(_carbmud);
                this.presentationBox_carbmud.Text = nif.GetNameInfo(_carbmud).Name;
                var imgS = CoreSystem.GetService<IImageInfoFactory>(_carbmud);
                presentationBox_carbmud.Image = imgS.GetImageInfo(_carbmud).GetDisplayImage(new ImageInfoContext());
                presentationBox_carbmud.Tag = _carbmud;
            }
            else
                PetrelLogger.WarnBox("Please select carbmud");
            PetrelLogger.InfoOutputWindow("Please select carbmud");
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
            args.Coal = this._coal;
            args.Silt = this._silt;
            args.SandStone = this._sandstone;
            args.MudStone = this._mudstone;
            args.DirtySS = this._dirtyss;
            args.CarbMud = this._carbmud;
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
