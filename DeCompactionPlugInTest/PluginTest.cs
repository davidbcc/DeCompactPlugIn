 
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PipChecker.DataProvider;
using PipChecker.Datas;
using PipCheckerLib.Arguments;
using PipCheckerLib.Rules;
using PipCheckerLib.Rules.Integration;
using DeCompactionPlugInTest.Properties;
using PlatformFamily = PipCheckerLib.Utils.PlatformFamily;

namespace UnitTest
{
    [TestClass]
    public class AcceptanceTests
    {
        private PipCheckerLib.PipChecker _checker;
        private HashSet<IData> _data;

        public AcceptanceTests()
        {
		    InstallerFileName = Resources.PipPath;
            if (!File.Exists(InstallerFileName))
            {
                Assert.Fail("Provided path: {0} is incorrect. Installer doesn't exist.", InstallerFileName);
            }

            InitializeChecker();
        }

		public string InstallerFileName { get; set; }

        #region Tests   
  
	    [TestMethod]
        public void InstallRule()
        {
             Check(_checker.Check(InstallerFileName, _data, Resources.AcceptanceTempFolder, new List<IRule> { new InstallRule() }), RuleStatus.FinishedFailed, typeof(InstallRule));
        }
			
	    [TestMethod]
        public void UninstallRule()
        {
             Check(_checker.Check(InstallerFileName, _data, Resources.AcceptanceTempFolder, new List<IRule> { new UninstallRule() }), RuleStatus.FinishedFailed, typeof(UninstallRule));
        }
			
	    [TestMethod]
        public void MsiContainsPipRule()
        {
             Check(_checker.Check(InstallerFileName, _data, Resources.AcceptanceTempFolder, new List<IRule> { new MsiContainsPipRule() }), RuleStatus.FinishedFailed, typeof(MsiContainsPipRule));
        }
			
	    [TestMethod]
        public void DoesntInstallPlatformLibraries()
        {
             Check(_checker.Check(InstallerFileName, _data, Resources.AcceptanceTempFolder, new List<IRule> { new DoesntInstallPlatformLibraries() }), RuleStatus.FinishedFailed, typeof(DoesntInstallPlatformLibraries));
        }
			
	    [TestMethod]
        public void DoesntUseSlbLicenseRule()
        {
             Check(_checker.Check(InstallerFileName, _data, Resources.AcceptanceTempFolder, new List<IRule> { new DoesntUseSlbLicenseRule() }), RuleStatus.FinishedFailed, typeof(DoesntUseSlbLicenseRule));
        }
			
	    [TestMethod]
        public void UseOnlySameLibsAsPreviousPluginsRule()
        {
             Check(_checker.Check(InstallerFileName, _data, Resources.AcceptanceTempFolder, new List<IRule> { new UseOnlySameLibsAsPreviousPluginsRule() }), RuleStatus.FinishedFailed, typeof(UseOnlySameLibsAsPreviousPluginsRule));
        }
			
	    [TestMethod]
        public void DoesntUseInternalPlatformApiRule()
        {
             Check(_checker.Check(InstallerFileName, _data, Resources.AcceptanceTempFolder, new List<IRule> { new DoesntUseInternalPlatformApiRule() }), RuleStatus.FinishedFailed, typeof(DoesntUseInternalPlatformApiRule));
        }
			
	    [TestMethod]
        public void AllAssembliesStronglyNamedRule()
        {
             Check(_checker.Check(InstallerFileName, _data, Resources.AcceptanceTempFolder, new List<IRule> { new AllAssembliesStronglyNamedRule() }), RuleStatus.FinishedFailed, typeof(AllAssembliesStronglyNamedRule));
        }
			
	    [TestMethod]
        public void AllModulesAreSignedRule()
        {
             Check(_checker.Check(InstallerFileName, _data, Resources.AcceptanceTempFolder, new List<IRule> { new AllModulesAreSignedRule() }), RuleStatus.FinishedFailed, typeof(AllModulesAreSignedRule));
        }
			
	    [TestMethod]
        public void ManifestVersionsSameRule()
        {
             Check(_checker.Check(InstallerFileName, _data, Resources.AcceptanceTempFolder, new List<IRule> { new ManifestVersionsSameRule() }), RuleStatus.FinishedFailed, typeof(ManifestVersionsSameRule));
        }
			
	    [TestMethod]
        public void VersionGreaterRule()
        {
             Check(_checker.Check(InstallerFileName, _data, Resources.AcceptanceTempFolder, new List<IRule> { new VersionGreaterRule() }), RuleStatus.FinishedFailed, typeof(VersionGreaterRule));
        }
			
	    [TestMethod]
        public void PluginManifestFilesMatchRule()
        {
             Check(_checker.Check(InstallerFileName, _data, Resources.AcceptanceTempFolder, new List<IRule> { new PluginManifestFilesMatchRule() }), RuleStatus.FinishedFailed, typeof(PluginManifestFilesMatchRule));
        }
			
	    [TestMethod]
        public void ArchitectureEqualityRule()
        {
             Check(_checker.Check(InstallerFileName, _data, Resources.AcceptanceTempFolder, new List<IRule> { new ArchitectureEqualityRule() }), RuleStatus.FinishedFailed, typeof(ArchitectureEqualityRule));
        }
			

        #endregion

		private void InitializeChecker()
		{
		    _data = GetStaticData();
            _checker = new PipCheckerLib.PipChecker(Arguments.GetDefaults());
		}

		private void Check(PipCheckerLib.PipChecker.CheckResults checkResults, RuleStatus assertStatus, Type ruleType)
        {
            var results = checkResults.CheckStatuses.Where(x => x.Value.Rule.GetType() == ruleType && (x.Value.Status == assertStatus || x.Value.Status == RuleStatus.Skipped)).ToList();
            if (results.Count > 0)
            {
                var comment = results.Aggregate(string.Empty, (current, keyValuePair) => current + keyValuePair.Value.Comment);
                Assert.Fail(comment);
            }
        }

		/// <summary>
        /// After update in Quality Assistant, you may have the latest version here: {LocalAppData}\Schlumberger\Ocean
        /// StaticData location can be changed   
        /// </summary>
		private HashSet<IData> GetStaticData()
        {
            var assemblyFolder = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            var pathToStaticData = Path.Combine(assemblyFolder, "StaticData");
            var packageDataReader = new PackageDataXmlReader(pathToStaticData);
            var packageReader = new PackageDataReader(packageDataReader);
            var dbProvider = new DataProviderBase(packageReader);
            var data = dbProvider.GetData();
            data.Add(new TargetPlatformVersion { Family = PlatformFamily.Petrel });
            return data;
        } 
    }
}