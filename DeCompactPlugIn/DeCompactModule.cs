using System;
using Slb.Ocean.Core;
using Slb.Ocean.Petrel;
using Slb.Ocean.Petrel.UI;
using Slb.Ocean.Petrel.Workflow;
using DeCompactionPlugIn.Helpers;

namespace DeCompactPlugIn
{
    /// <summary>
    /// This class will control the lifecycle of the Module.
    /// The order of the methods are the same as the calling order.
    /// </summary>
    [ModuleAppearance(typeof(DeCompactModuleAppearance))]
    public class DeCompactModule : IModule
    {
        #region Private Variables
        private Process m_decompactworkstepInstance;
        #endregion
        public DeCompactModule()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        #region IModule Members

        /// <summary>
        /// This method runs once in the Module life; when it loaded into the petrel.
        /// This method called first.
        /// </summary>
        public void Initialize()
        {
            DataManager.WorkspaceEvents.Opened += this.WorkspaceOpened;
            DataManager.WorkspaceEvents.Closing += this.WorkspaceClosing;
            DataManager.WorkspaceEvents.Closed += this.WorkspaceClosed;

            // TODO:  Add DeCompactModule.Initialize implementation
        }

        /// <summary>
        /// This method runs once in the Module life. 
        /// In this method, you can do registrations of the not UI related components.
        /// (eg: datasource, plugin)
        /// </summary>
        public void Integrate()
        {
            
            // TODO:  Add DeCompactModule.Integrate implementation
            
            // Register DeCompactWorkStep
            DeCompactWorkStep decompactworkstepInstance = new DeCompactWorkStep();
            PetrelSystem.WorkflowEditor.AddUIFactory<DeCompactWorkStep.Arguments>(new DeCompactWorkStep.UIFactory());
            PetrelSystem.WorkflowEditor.Add(decompactworkstepInstance);
            m_decompactworkstepInstance = new Slb.Ocean.Petrel.Workflow.WorkstepProcessWrapper(decompactworkstepInstance);
            PetrelSystem.ProcessDiagram.Add(m_decompactworkstepInstance, "Plug-ins");
        }

        /// <summary>
        /// This method runs once in the Module life. 
        /// In this method, you can do registrations of the UI related components.
        /// (eg: settingspages, treeextensions)
        /// </summary>
        public void IntegratePresentation()
        {

            // TODO:  Add DeCompactModule.IntegratePresentation implementation
            
            // Add Ribbon Configuration file
           // PetrelSystem.ConfigurationService.AddConfiguration(DeCompactPlugIn.Properties.Resources.OceanRibbonConfiguration1);
            // Register Menu Item for commands. Will be available in Petrel Classic mode.
           // PetrelSystem.ConfigurationService.AddConfiguration(CannedWorkflowSampleResources.CannedWorkflowSampleConfig);
        }

        /// <summary>
        /// This method is not part of the IModule interface.
        /// It is an eventhandler method, which is subscribed in the Initialize() method above,
        /// and called every time the petrel creates or loads a project.
        /// </summary>
        private void WorkspaceOpened(object sender, WorkspaceEventArgs args)
        {

            // TODO:  Add Workspace Opened eventhandler implementation
            CannedWorkflowHelper.Instance.LoadWorkflow();
        }

        /// <summary>
        /// This method is not part of the IModule interface.
        /// It is an eventhandler method, which is subscribed in the Initialize() method above,
        /// and called every time before the petrel closes a project.
        /// </summary>
        private void WorkspaceClosing(object sender, WorkspaceCancelEventArgs args)
        {
            // TODO:  Add Workspace Closing eventhandler implementation
        }

        /// <summary>
        /// This method is not part of the IModule interface.
        /// It is an eventhandler method, which is subscribed in the Initialize() method above,
        /// and called every time after the petrel closed a project.
        /// </summary>
        private void WorkspaceClosed(object sender, WorkspaceEventArgs args)
        {
            // TODO:  Add Workspace Closed eventhandler implementation
        }

        /// <summary>
        /// This method called once in the life of the module; 
        /// right before the module is unloaded. 
        /// It is usually when the application is closing.
        /// </summary>
        public void Disintegrate()
        {
            DataManager.WorkspaceEvents.Opened -= this.WorkspaceOpened;
            DataManager.WorkspaceEvents.Closing -= this.WorkspaceClosing;
            DataManager.WorkspaceEvents.Closed -= this.WorkspaceClosed;

            // TODO:  Add DeCompactModule.Disintegrate implementation
            // Unregister DeCompactWorkStep
            PetrelSystem.WorkflowEditor.RemoveUIFactory<DeCompactWorkStep.Arguments>();
            PetrelSystem.ProcessDiagram.Remove(m_decompactworkstepInstance);
        }

        #endregion

        #region IDisposable Members

        public void Dispose()
        {
            // TODO:  Add DeCompactModule.Dispose implementation
        }

        #endregion

    }

    #region ModuleAppearance Class

    /// <summary>
    /// Appearance (or branding) for a Slb.Ocean.Core.IModule.
    /// This is associated with a module using Slb.Ocean.Core.ModuleAppearanceAttribute.
    /// </summary>
    internal class DeCompactModuleAppearance : IModuleAppearance
    {
        /// <summary>
        /// Description of the module.
        /// </summary>
        public string Description
        {
            get { return "DeCompactModule"; }
        }

        /// <summary>
        /// Display name for the module.
        /// </summary>
        public string DisplayName
        {
            get { return "DeCompactModule"; }
        }

        /// <summary>
        /// Returns the name of a image resource.
        /// </summary>
        public string ImageResourceName
        {
            get { return null; }
        }

        /// <summary>
        /// A link to the publisher or null.
        /// </summary>
        public Uri ModuleUri
        {
            get { return null; }
        }
    }

    #endregion
}