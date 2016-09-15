using System;
using System.Collections.Generic;

using Slb.Ocean.Core;
using System.Reflection;
using System.Drawing;

namespace DeCompactPlugIn
{
    public class Plugin : Slb.Ocean.Core.Plugin
    {
        public override string AppVersion
        {
            get { return "2015.1"; }
        }

        public override string Author
        {
            get { return "daren"; }
        }

        public override string Contact
        {
            get { return "davidb@bit-numbers.com"; }
        }

        public override IEnumerable<PluginIdentifier> Dependencies
        {
            get { return null; }
        }

        public override string Description
        {
            get { return "Facies Decompactor"; }
        }

        public override string ImageResourceName
        {
            get { return Assembly.GetExecutingAssembly().GetName().Name + ".Resources." + "logo.bmp"; }
        }

        public override Uri PluginUri
        {
            get { return new Uri("http://www.pluginuri.info"); }
        }

        public override IEnumerable<ModuleReference> Modules
        {
            get 
            {
                // Please fill this method with your modules with lines like this:
                //yield return new ModuleReference(typeof(Module));
                yield return new ModuleReference(typeof(DeCompactModule));

            }
        }

        public override string Name
        {
            get { return "Facies Decompactor"; }
        }

        public override PluginIdentifier PluginId
        {
            get { return new PluginIdentifier(GetType().FullName, GetType().Assembly.GetName().Version); }
        }

        public override ModuleTrust Trust
        {
            get { return new ModuleTrust("Default"); }
        }
    }
}
