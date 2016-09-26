using System;
using System.IO;
using System.Linq;
using System.Reflection;
using Slb.Ocean.Core;
using Slb.Ocean.Petrel;
using Slb.Ocean.Petrel.Workflow;
using DeCompactPlugIn;
using DeCompactPlugIn.model;
using System.Collections.Generic;

namespace DeCompactionPlugIn.Helpers
{
    /// <summary>
    ///     Helper class to handle workflow related mehtods (loading, running etc.)
    /// </summary>
    public class CannedWorkflowHelper
    {
        IEnumerable<Workflow> _workflowList;
        #region constant values
        private const int FACIES = 0;
        private const int COAL = 1;
        private const int SILT = 2;
        private const int SANDSTONE = 3;
        private const int MUDSTONE = 4;
        private const int DIRTYSS = 5;
        private const int CARBMUD = 6;
        private const int GRID = 7;
        private const int HORIZON = 8;
        private const int ITERATION= 9;
        private const int OUT_ZONES = 0;
        private const int OUT_LAYERS = 1;
        private const int OUT_DEPTH = 2;
        private const string wfName = "decompaction";
        private const string collName = "UQ";
        private const int OUT_CELL_HEIGHT = 3;
        #endregion

        private static CannedWorkflowHelper _instance;

        public static CannedWorkflowHelper Instance
        {
            get { return _instance ?? (_instance = new CannedWorkflowHelper()); }
        }

        /// <summary>
        ///     Finds and copies the workflow into the project, if it does not already exist
        /// </summary>
        private void LoadWorkflow()
        {
        
           
            // if it already exists, we will not load it
            //if (FindPredefinedWorkflow(wfName, collName) != null)
            //{
            //    return;
            //}

            try
            {

                PetrelLogger.InfoOutputWindow("Original Workflow loading..");
                
                var pluginDir = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
                if (pluginDir == null) return;
                // Production - deployment
                var predefinedWorkflowPath = Path.Combine(pluginDir, @"ProjectFile\decompact\Test_project.pet");
               // var predefinedWorkflowPath = Path.Combine(pluginDir, @"C:\decomp\Test_project.pet");
                
                //PetrelLogger.InfoOutputWindow(string.Format("pluginDir:{0}", pluginDir));
                //PetrelLogger.InfoOutputWindow(string.Format("This is the predefined Workflow path:{0}", predefinedWorkflowPath));
                // Use IWorkflowSyncService to find the workflow from given project and 
                // copies it to current project.  
              
               PetrelSystem.WorkflowSyncService.CopyWorkflowsFromProject(predefinedWorkflowPath, new string[1] { wfName }, CopyMode.OverwriteIfExists
                        );
            }
            catch (Exception e)
            {
                PetrelLogger.InfoOutputWindow(
                    @"Warning: The workflow project could not be loaded. Please make sure it was unzipped in the [ProjectDir]\bin\Debug\ before compiling the plug-in");
                PetrelLogger.InfoOutputWindow(e.Message);
            }
            finally
            {
                PetrelLogger.InfoOutputWindow("Original Workflw loaded successfully..");
            }
        }

        /// <summary>
        ///     Find the workflow by given name in the current project
        /// </summary>
        /// <param name="wfName">Name of workflow to find</param>
        /// <param name="collName">Parent folder name of workflow to find the workflow in</param>
        /// <returns>Workflow if found, null otherwise</returns>
        public Workflow FindPredefinedWorkflow(string wfName, string collName)
        {
            Workflow wf = null;
            var wfs = PetrelProject.Workflows;

            var wfc = wfs.WorkflowCollections.FirstOrDefault(c => c.Name.Equals(collName));
            if (wfc != null)
                wf = wfc.FirstOrDefault(w => w.Name.Equals(wfName));
            return wf;
        }

        /// <summary>
        ///     Sets the input variable in the predefined workflow and runs the workflow.
        ///     It also returns the resulted grid from the output reference variable.
        /// </summary>
        /// <param name="objectToCopy">Input grid to copy. This will be copied to the input reference variable in the workflow.</param>
        /// <returns>Copied grid received from the output reference variable in the workflow.</returns>
        public void RunWorkflow(WorkStepArgument args)
        {
            PetrelLogger.InfoOutputWindow("Start Running Workflow");
            LoadWorkflow();
            if(args == null)
            {
                PetrelLogger.InfoOutputWindow("Arguments cannot be NULL");
            }

            System.Windows.Forms.Cursor current = System.Windows.Forms.Cursor.Current;
            System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor;

            ReferenceVariable inputGrid = null;
            ReferenceVariable inputFacies= null;
            ReferenceVariable inputIteration = null;
            ReferenceVariable inputSilt = null;
            ReferenceVariable inputCoal = null;
            ReferenceVariable inputSandStone = null;
            ReferenceVariable inputMudStone = null;
            ReferenceVariable inputDirtySS = null;
            ReferenceVariable inputCarbMud = null;
         

            var cannedWf = FindPredefinedWorkflow("decompaction", "UQ");
            var runner = new WorkflowRunner(cannedWf);

            // Input References Variables
            inputGrid = cannedWf.InputReferenceVariables.ElementAt(GRID);
           // inputHorizon = cannedWf.InputReferenceVariables.ElementAt(HORIZON);
            inputFacies = cannedWf.InputReferenceVariables.ElementAt(FACIES);
            inputIteration = cannedWf.InputReferenceVariables.ElementAt(ITERATION);
            inputSilt = cannedWf.InputReferenceVariables.ElementAt(SILT);
            inputCoal = cannedWf.InputReferenceVariables.ElementAt(COAL);
            inputSandStone = cannedWf.InputReferenceVariables.ElementAt(SANDSTONE);
            inputMudStone = cannedWf.InputReferenceVariables.ElementAt(MUDSTONE);
            inputDirtySS = cannedWf.InputReferenceVariables.ElementAt(DIRTYSS);
            inputCarbMud = cannedWf.InputReferenceVariables.ElementAt(CARBMUD);



            runner.SetInputVariableBinding(inputGrid, args.Grid);
           // runner.SetInputVariableBinding(inputHorizon, args.Horizon);
            runner.SetInputVariableBinding(inputFacies, args.Facies);
            runner.SetInputVariableBinding(inputSilt, args.Silt);
            runner.SetInputVariableBinding(inputCoal, args.Coal);
            runner.SetInputVariableBinding(inputSandStone, args.SandStone);
            runner.SetInputVariableBinding(inputMudStone, args.MudStone);
            runner.SetInputVariableBinding(inputDirtySS, args.DirtySS);
            runner.SetInputVariableBinding(inputCarbMud, args.CarbMud);
            runner.SetInputVariableBinding<int>("$iteration", args.iteration);

            PetrelLogger.InfoOutputWindow("Input Parameters loaded successfully.");

            try
            {
                runner.Run();
               
            }
           
            catch(Exception ex)
            {
 
                PetrelLogger.InfoOutputWindow(string.Format(" Decompaction Workflow Run error occured:{0}", ex.Message));
            }
            finally
            {

                try
                {
                    PetrelLogger.InfoOutputWindow("Fake workflow loading started....");
                    var pluginDir = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
                   // var predefinedFakeWorkflowPath = Path.Combine(pluginDir, @"C:\decompfake\Test_project.pet");
                    var predefinedFakeWorkflowPath = Path.Combine(pluginDir, @"ProjectFile\decompactFake\Test_project.pet");
                    PetrelSystem.WorkflowSyncService.CopyWorkflowsFromProject(predefinedFakeWorkflowPath, new string[1] { wfName }, CopyMode.OverwriteIfExists
                       );
              

                }
                catch (Exception ex)
                {

                    PetrelLogger.InfoOutputWindow(string.Format(" Copy Fake Workflow failed:{0}", ex.Message));
                }
                finally
                {
                    System.Windows.Forms.Cursor.Current = current;
                    PetrelLogger.InfoOutputWindow("Fake workflow loading completed...");
                }
            }
            
        }
    }
}