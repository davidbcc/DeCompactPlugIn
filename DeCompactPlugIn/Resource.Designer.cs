﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DeCompactPlugIn {
    using System;
    
    
    /// <summary>
    ///   A strongly-typed resource class, for looking up localized strings, etc.
    /// </summary>
    // This class was auto-generated by the StronglyTypedResourceBuilder
    // class via a tool like ResGen or Visual Studio.
    // To add or remove a member, edit your .ResX file then rerun ResGen
    // with the /str option, or rebuild your VS project.
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "4.0.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    internal class Resource {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal Resource() {
        }
        
        /// <summary>
        ///   Returns the cached ResourceManager instance used by this class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("DeCompactPlugIn.Resource", typeof(Resource).Assembly);
                    resourceMan = temp;
                }
                return resourceMan;
            }
        }
        
        /// <summary>
        ///   Overrides the current thread's CurrentUICulture property for all
        ///   resource lookups using this strongly typed resource class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Globalization.CultureInfo Culture {
            get {
                return resourceCulture;
            }
            set {
                resourceCulture = value;
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to &lt;?xml version=&quot;1.0&quot; encoding=&quot;utf-8&quot; ?&gt;
        ///
        ///&lt;Configuration Type=&quot;Configuration.ConfigurationState&quot; Version=&quot;1.0.0.10&quot; Caption=&quot;DeCompactConfig&quot;&gt;
        ///  &lt;Extensions Type=&quot;Extensibility.ExtensionCollection&quot;&gt;
        ///    &lt;Workspaces.WorkspaceExtension EntityId=&quot;491b2ecc-57f0-4f0a-baf8-e7268b15e6ab&quot; /&gt;
        ///  &lt;/Extensions&gt;
        ///  &lt;Sections Type=&quot;Extesibility.ConfigurationSectionCollection&quot;&gt;
        ///    &lt;Commands.CommandSection Version=&quot;1.0.0.4&quot;&gt;
        ///      &lt;Extensions Type=&quot;Extensibility.ExtensionCollection&quot;&gt;
        ///        &lt;Workspaces.WorkspaceEx [rest of string was truncated]&quot;;.
        /// </summary>
        internal static string DeCompactConfig {
            get {
                return ResourceManager.GetString("DeCompactConfig", resourceCulture);
            }
        }
    }
}
