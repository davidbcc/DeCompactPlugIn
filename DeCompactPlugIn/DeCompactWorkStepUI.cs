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
using System.Collections.Generic;
using System.Text.RegularExpressions;

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
        private DictionaryProperty _facies;
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
 
            
        }
        #region Drag Drop events handling
        // Grid drag-drop function.
        private void drop_grid_DragDrop(object sender, DragEventArgs e)
        {

            _grid = e.Data.GetData(typeof(object)) as Grid;
            if (_grid == null)
            {
                PetrelLogger.WarnBox("Please select a Grid");
                PetrelLogger.InfoOutputWindow("Please select a Grid");
                return;
            }
            presGrid.Text = _grid.Name;
            IImageInfoFactory fact = CoreSystem.GetService<IImageInfoFactory>(_grid);
            presGrid.Image = fact.GetImageInfo(_grid).GetDisplayImage(new ImageInfoContext());
            presGrid.Tag = _grid;
        }

    

 
        // Facies drag-drop function.
        private void drop_facies_DragDrop(object sender, DragEventArgs e)
        {
            var drop = e.Data.GetData(typeof(object));
            _facies = drop as DictionaryProperty;
            if (_facies == null)
            {
                PetrelLogger.WarnBox("Please select a Facies");
                PetrelLogger.InfoOutputWindow("Please select a Facies");
                return;
            }
                var info = CoreSystem.GetService<INameInfoFactory>(_facies);
                this.presentationBox_facies.Text = info.GetNameInfo(_facies).Name;
                var img = CoreSystem.GetService<IImageInfoFactory>(_facies);
                presentationBox_facies.Image = img.GetImageInfo(_facies).GetDisplayImage(new ImageInfoContext());
                presentationBox_facies.Tag = _facies;
     
        }
        // Silt drag-drop function.
        private void dropTarget_silt_DragDrop(object sender, DragEventArgs e)
        {

        

 
            var drop = e.Data.GetData(typeof(object));
            _silt = drop as Function;
          

            if (_silt == null || _silt.Name.ToLower() != "silt")
            {
                PetrelLogger.WarnBox("Please select a Silt function.");
                PetrelLogger.InfoOutputWindow("Please select a Silt function.");
                
                return;
            }
                var info = CoreSystem.GetService<INameInfoFactory>(_silt);
                this.presentationBox_silt.Text = info.GetNameInfo(_silt).Name;
                var img = CoreSystem.GetService<IImageInfoFactory>(_silt);
                presentationBox_silt.Image = img.GetImageInfo(_silt).GetDisplayImage(new ImageInfoContext());
                presentationBox_silt.Tag = _silt;

        }
        //Sandstone drag-drop function.
        // Facies 1 
        private void dropTarget_sandstone_DragDrop(object sender, DragEventArgs e)
        {
            var drop = e.Data.GetData(typeof(object));
            _sandstone = drop as Function;

            if (_sandstone == null || _sandstone.Name.ToLower() != "sandstone")
            {
                PetrelLogger.WarnBox("Please select a Sandstone function");
                PetrelLogger.InfoOutputWindow("Please select a Sandstone function");
                return;
            }
                var info = CoreSystem.GetService<INameInfoFactory>(_sandstone);
                this.presentationBox_sandstone.Text = info.GetNameInfo(_sandstone).Name;
                var img = CoreSystem.GetService<IImageInfoFactory>(_sandstone);
                presentationBox_sandstone.Image = img.GetImageInfo(_sandstone).GetDisplayImage(new ImageInfoContext());
                presentationBox_sandstone.Tag = _sandstone;
           
        }
        // Mudstone drag-drop function.
        private void dropTarget_mudstone_DragDrop(object sender, DragEventArgs e)
        {
            var drop = e.Data.GetData(typeof(object));
            _mudstone = drop as Function;
            if (_mudstone == null || _mudstone.Name.ToLower() != "mudstone")
            {
                PetrelLogger.WarnBox("Please select a  Mudstone function");
                PetrelLogger.InfoOutputWindow("Please select a Mudstone function");
                return;
            }
            var info = CoreSystem.GetService<INameInfoFactory>(_mudstone);
            this.presentationBox_mudstone.Text = info.GetNameInfo(_mudstone).Name;
                var img = CoreSystem.GetService<IImageInfoFactory>(_mudstone);
                presentationBox_mudstone.Image = img.GetImageInfo(_mudstone).GetDisplayImage(new ImageInfoContext());
                presentationBox_mudstone.Tag = _mudstone;
          
        }

        // Coal drag-drop function.
        private void dropTarget_coal_DragDrop(object sender, DragEventArgs e)
        {
            var drop = e.Data.GetData(typeof(object));
            _coal = drop as Function;
            if (_coal == null || _coal.Name.ToLower() != "coal")
            {
                PetrelLogger.WarnBox("Please select a Coal function");
                PetrelLogger.InfoOutputWindow("Please select a Coal function");
                return;
            }
            var info = CoreSystem.GetService<INameInfoFactory>(_coal);
            this.presentationBox_coal.Text = info.GetNameInfo(_coal).Name;
                var img = CoreSystem.GetService<IImageInfoFactory>(_coal);
                presentationBox_coal.Image = img.GetImageInfo(_coal).GetDisplayImage(new ImageInfoContext());
                presentationBox_coal.Tag = _coal;
          
        }

        // Dirty SS drag-drop function.
        private void dropTarget_dirtyss_DragDrop(object sender, DragEventArgs e)
        {
            var drop = e.Data.GetData(typeof(object));
            _dirtyss= drop as Function;

            if (_dirtyss == null || _dirtyss.Name.ToLower() != "dirtyss")
            {
                PetrelLogger.WarnBox("Please select a DirtySS function");
                PetrelLogger.InfoOutputWindow("Please select a DirtySS function");
                return;
            }

            var info = CoreSystem.GetService<INameInfoFactory>(_dirtyss);
            this.presentationBox_dirtyss.Text = info.GetNameInfo(_dirtyss).Name;
                var img = CoreSystem.GetService<IImageInfoFactory>(_dirtyss);
                presentationBox_dirtyss.Image = img.GetImageInfo(_dirtyss).GetDisplayImage(new ImageInfoContext());
                presentationBox_dirtyss.Tag = _dirtyss;
         
        }
        // Carb mud drag-drop function.
        private void dropTarget_carbmud_DragDrop(object sender, DragEventArgs e)
        {
            var drop = e.Data.GetData(typeof(object));
            _carbmud = drop as Function;
            if (_carbmud == null || _carbmud.Name.ToLower() != "carbmud")
            {
                PetrelLogger.WarnBox("Please select a Carbmud function ");
                PetrelLogger.InfoOutputWindow("Please select a Carbmud function ");
                return;
            }
            var info = CoreSystem.GetService<INameInfoFactory>(_carbmud);
            this.presentationBox_carbmud.Text = info.GetNameInfo(_carbmud).Name;
                var img = CoreSystem.GetService<IImageInfoFactory>(_carbmud);
                presentationBox_carbmud.Image = img.GetImageInfo(_carbmud).GetDisplayImage(new ImageInfoContext());
                presentationBox_carbmud.Tag = _carbmud;
          
        }

       
      

        #endregion

        
        #region buttons events handling
        /// <summary>
        /// Cancel Button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cancelButton_Click(object sender, EventArgs e)
        {
            var findForm = FindForm();
            if (findForm != null) findForm.Close();
        }
        /// <summary>
        /// Ok Button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OKButton_Click(object sender, EventArgs e)
        {
            var findForm = FindForm();
            if (findForm != null) findForm.Close();
        }
        private void applyButton_Click(object sender, EventArgs e)
        {

        }
        /// <summary>
        /// Run Button, executing the particular Worksteps.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void runButton_Click(object sender, EventArgs e)
        {
            var args = new WorkStepArgument();

            args.Facies = _facies;
            args.Grid = _grid;
          
            args.iteration = Int32.Parse(txtnolayers.Text.ToString());
            args.Coal = this._coal;
            args.Silt = this._silt;
            args.SandStone = this._sandstone;
            args.MudStone = this._mudstone;
            args.DirtySS = this._dirtyss;
            args.CarbMud = this._carbmud;
            Grid grid = presGrid.Tag as Grid;


            if (_grid == null)
            {
                PetrelLogger.WarnBox("Grid cannot be null ");
                PetrelLogger.InfoOutputWindow("grid cannot be null ");
                return;
            }
          

            if (_facies == null)
            {
                PetrelLogger.WarnBox("Facies cannot be null ");
                PetrelLogger.InfoOutputWindow("Facies cannot be null ");
                return;
            }
            if (_silt == null)
            {
                PetrelLogger.WarnBox("Silt cannot be null ");
                PetrelLogger.InfoOutputWindow("Silt cannot be null ");
                return;
            }
            if (_sandstone == null)
            {
                PetrelLogger.WarnBox("SandStone cannot be null ");
                PetrelLogger.InfoOutputWindow("SandStone cannot be null ");
                return;
            }
            if (_coal == null)
            {
                PetrelLogger.WarnBox("Coal cannot be null ");
                PetrelLogger.InfoOutputWindow("Coal cannot be null ");
                return;
            }
          
           
            if (_mudstone == null)
            {
                PetrelLogger.WarnBox("Mud Stone cannot be null ");
                PetrelLogger.InfoOutputWindow("Mud Stone cannot be null ");
                return;
            }
            if (_dirtyss == null)
            {
                PetrelLogger.WarnBox("Dirty SS cannot be null ");
                PetrelLogger.InfoOutputWindow("Dirty SS cannot be null ");
                return;
            }


            if (_carbmud == null)
            {
           
                PetrelLogger.InfoOutputWindow("Carb Mud cannot be null ");
                return;
            }
                CannedWorkflowHelper.Instance.RunWorkflow(args);
               
        
          
        }
        #endregion

    

     

   

    

      

 

     

        
    }
}
