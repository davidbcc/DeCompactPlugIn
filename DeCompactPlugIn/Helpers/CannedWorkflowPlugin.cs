using System;
using System.IO;
using System.Linq;
using System.Reflection;
using Slb.Ocean.Core;
using Slb.Ocean.Petrel;
using Slb.Ocean.Petrel.Workflow;
using DeCompactPlugIn;
using DeCompactPlugIn.model;

namespace DeCompactionPlugIn.Helpers
{
    /// <summary>
    ///     Helper class to handle workflow related mehtods (loading, running etc.)
    /// </summary>
    public class CannedWorkflowHelper
    {

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
        public void LoadWorkflow()
        {
            const string wfName = "decompaction";
            const string collName = "UQ";
            // if it already exists, we will not load it
            if (FindPredefinedWorkflow(wfName, collName) != null)
            {
                return;
            }

            try
            {
                var pluginDir = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
                if (pluginDir == null) return;
                // Production - deployment
                var predefinedWorkflowPath = Path.Combine(pluginDir, @"ProjectFile\Test_project.pet");
                //var predefinedWorkflowPath = Path.Combine(pluginDir, @"C:\decomp\Test_project.pet");
                PetrelLogger.InfoOutputWindow(string.Format("pluginDir:{0}", pluginDir));
                PetrelLogger.InfoOutputWindow(string.Format("This is the predefined Workflow path:{0}", predefinedWorkflowPath));
                // Use IWorkflowSyncService to find the workflow from given project and 
                // copies it to current project.  
                var ss = PetrelSystem.WorkflowSyncService;
                var loadedWorkflows =
                    ss.CopyWorkflowsFromProject(predefinedWorkflowPath, new string[1] {wfName},
                        CopyMode.OverwriteIfExists);
            }
            catch (Exception e)
            {
                PetrelLogger.InfoOutputWindow(
                    @"Warning: The workflow project could not be loaded. Please make sure it was unzipped in the [ProjectDir]\bin\Debug\ before compiling the plug-in");
                PetrelLogger.InfoOutputWindow(e.Message);
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
            if(args == null)
            {
                PetrelLogger.InfoOutputWindow("Arguments cannot be NULL");
            }
        
            ReferenceVariable inputGrid = null;
            ReferenceVariable inputFacies= null;
            ReferenceVariable inputIteration = null;
            ReferenceVariable inputSilt = null;
            ReferenceVariable inputCoal = null;
            ReferenceVariable inputSandStone = null;
            ReferenceVariable inputMudStone = null;
            ReferenceVariable inputDirtySS = null;
            ReferenceVariable inputCarbMud = null;
            //IIdentifiable copiedObject = null;
            //ReferenceVariable outputZONES = null;
            //ReferenceVariable outputLAYERS = null;
            //ReferenceVariable outputDEPTH = null;
            //ReferenceVariable outputCELL_HEIGHT = null;

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
            runner.SetInputVariableBinding(inputIteration, args.iteration);
            runner.SetInputVariableBinding(inputSilt, args.Silt);
            runner.SetInputVariableBinding(inputCoal, args.Coal);
            runner.SetInputVariableBinding(inputSandStone, args.SandStone);
            runner.SetInputVariableBinding(inputMudStone, args.MudStone);
            runner.SetInputVariableBinding(inputDirtySS, args.DirtySS);
            runner.SetInputVariableBinding(inputCarbMud, args.CarbMud);
            runner.SetInputVariableBinding("$loops", args.iteration);

            ////Output Referenes Variables
            //outputZONES = cannedWf.OutputReferenceVariables.ElementAt(OUT_ZONES);
            //outputLAYERS = cannedWf.OutputReferenceVariables.ElementAt(OUT_LAYERS);
            //outputDEPTH = cannedWf.OutputReferenceVariables.ElementAt(OUT_DEPTH);
            //outputCELL_HEIGHT = cannedWf.OutputReferenceVariables.ElementAt(OUT_CELL_HEIGHT);
            //runner.GetValueOfOutputVariable<string>(outputZONES);
            //runner.GetValueOfOutputVariable<string>(outputLAYERS);
            //runner.GetValueOfOutputVariable<string>(outputDEPTH);
            //runner.GetValueOfOutputVariable<string>(outputCELL_HEIGHT);

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
                //copiedObject = null;
            }
            //return copiedObject;
        }
    }
}