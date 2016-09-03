using System.Collections.Generic;
using Slb.Ocean.Basics;
using Slb.Ocean.Core;
using Slb.Ocean.Petrel;
using Slb.Ocean.Petrel.DomainObject.PillarGrid;

namespace DeCompactionPlugIn.Helpers
{
    /// <summary>
    ///     Helper class to provide grid related methods
    /// </summary>
    public class GridHelper
    {
        private static GridHelper _instance;

        /// <summary>
        ///     list to save cells along the faults on either side
        /// </summary>
        private List<Index3> _faultEdgeCells;

        public static GridHelper Instance
        {
            get { return _instance ?? (_instance = new GridHelper()); }
        }

        /// <summary>
        ///     Creates new grid properties corresponding to the one in original grid.
        ///     Populates the cells which are along the fault with the values from original property.
        ///     If user selects to keep the original properties, it will keep the original proeprties,
        ///     otherwise delete them.
        /// </summary>
        /// <param name="grid">Grid.</param>
        /// <param name="keepProperties">Flag indicating if the original properties should be deleted or not.</param>
        public void FaultEdgeProperty(Grid grid, bool keepProperties)
        {
            var rootFC = grid.FaultCollection;
            _faultEdgeCells = new List<Index3>();
            GetFaultsIn(rootFC);

            var numCellsI = grid.NumCellsIJK.I;
            var numCellsJ = grid.NumCellsIJK.J;
            var numCellsK = grid.NumCellsIJK.K;
            var gridPropertiesCount = grid.PropertyCount + grid.DictionaryPropertyCount;

            using (var trans = DataManager.NewTransaction())
            using (var progress = PetrelLogger.NewProgress(0, gridPropertiesCount))
            {
                // copy all continuous properties
                foreach (var property in grid.Properties)
                {
                    trans.Lock(property.PropertyCollection);
                    var resultProp = property.PropertyCollection.CreateProperty(property.Template);
                    resultProp.Name = property.Name + "[Fault Edges]";

                    var specializedAccess = resultProp.SpecializedAccess;
                    var specializedAccessOrig = property.SpecializedAccess;
                    using (
                        NoBoundaryCheckPropertyIndexer indexer = specializedAccess.OpenNoBoundaryCheckPropertyIndexer(),
                            indexerOrig = specializedAccessOrig.OpenNoBoundaryCheckPropertyIndexer())
                    {
                        _faultEdgeCells.ForEach(idx =>
                            indexer[idx] = indexerOrig[idx]
                            );
                    }
                    if (!keepProperties)
                    {
                        trans.Lock(property);
                        property.Delete();
                    }
                    progress.ProgressStatus++;
                }

                // copy all dictionary properties
                foreach (var dProperty in grid.DictionaryProperties)
                {
                    trans.Lock(dProperty.PropertyCollection);
                    var resultDictProp = dProperty.PropertyCollection.CreateDictionaryProperty(dProperty.DictionaryTemplate);
                    resultDictProp.Name = dProperty.Name + "[Fault Edges]";

                    var specializedAccessOrig = dProperty.SpecializedAccess;
                    var specializedAccess = resultDictProp.SpecializedAccess;

                    using (
                        NoBoundaryCheckDictionaryPropertyIndexer dIndexer =
                            specializedAccess.OpenNoBoundaryCheckDictionaryPropertyIndexer(),
                            dIndexerOrig = specializedAccessOrig.OpenNoBoundaryCheckDictionaryPropertyIndexer())
                    {
                        _faultEdgeCells.ForEach(idx =>
                            dIndexer[idx] = dIndexerOrig[idx]
                            );
                    }
                    if (!keepProperties)
                    {
                        trans.Lock(dProperty);
                        dProperty.Delete();
                    }
                    progress.ProgressStatus++;
                }
                trans.Commit();
            }
        }

        /// <summary>
        ///     Finds faults in given collection and sub collections recursively.
        ///     For each fault, it calls another method to find the cells along the fault.
        /// </summary>
        /// <param name="fcoll">Fault collection to find faults in.</param>
        public void GetFaultsIn(FaultCollection fcoll)
        {
            foreach (var fault in fcoll.Faults)
            {
                FindEdges(fault);
            }
            foreach (var subColl in fcoll.FaultCollections)
            {
                GetFaultsIn(subColl);
            }
        }

        /// <summary>
        ///     Finds the cells on either side of the fault and adds them to the list
        /// </summary>
        /// <param name="fault">Fault to find cells for</param>
        public void FindEdges(Fault fault)
        {
            foreach (var face in fault.Faces)
            {
                if (face.Cell1 != null)
                    _faultEdgeCells.Add(face.Cell1);
                if (face.Cell2 != null)
                    _faultEdgeCells.Add(face.Cell2);
            }
        }
    }
}