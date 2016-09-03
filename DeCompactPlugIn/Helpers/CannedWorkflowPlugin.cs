using System;
using System.IO;
using System.Linq;
using System.Reflection;
using Slb.Ocean.Core;
using Slb.Ocean.Petrel;
using Slb.Ocean.Petrel.Workflow;
using DeCompactPlugIn;

namespace DeCompactionPlugIn.Helpers
{
    /// <summary>
    ///     Helper class to handle workflow related mehtods (loading, running etc.)
    /// </summary>
    public class CannedWorkflowHelper
    {
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
            const string collName = "DB";
            // if it already exists, we will not load it
            if (FindPredefinedWorkflow(wfName, collName) != null)
            {
                return;
            }

            try
            {
                // get workflow from the project in plugin assembly
                var pluginPath = Assembly.GetAssembly(typeof (DeCompactModule)).Location;
                var pluginDir = Path.GetDirectoryName(pluginPath);
                if (pluginDir == null) return;
                var predefinedWorkflowPath = Path.Combine(pluginDir, @"C:\decomp\decompaction.pet");
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
        public object RunWorkflow(IIdentifiable objectToCopy)
        {
            ReferenceVariable input = null;
            ReferenceVariable output = null;
            IIdentifiable copiedObject = null;
            var cannedWf = FindPredefinedWorkflow("decompaction", "DB");
            var runner = new WorkflowRunner(cannedWf);
            //input = cannedWf.InputReferenceVariables.ElementAt(0);
            //runner.SetInputVariableBinding(input, objectToCopy);
            try
            {
                runner.Run();
                //output = cannedWf.OutputReferenceVariables.First(); // there is only one output variable
                //copiedObject = runner.GetValueOfOutputVariable<IIdentifiable>(output);
                //return copiedObject;
            }
           
            catch(Exception ex)
            {
                PetrelLogger.InfoOutputWindow(string.Format("Workflow Run error occured:{0}", ex.Message));
                PetrelLogger.InfoOutputWindow(string.Format("Workflow Run error occured:{0}", ex.Message));
            }
            finally
            {
                copiedObject = null;
            }
            return copiedObject;
        }
    }
}