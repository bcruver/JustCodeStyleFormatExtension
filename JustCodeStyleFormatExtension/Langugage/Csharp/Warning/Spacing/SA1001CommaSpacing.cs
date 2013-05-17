// using System;
// using System.Collections.Generic;
// using System.ComponentModel.Composition;
// using Telerik.JustCode.CommonLanguageModel;

// namespace JustCodeStyleFormatExtension.Warning.Spacing
//{
//    /// <summary> 
//    /// 
//    /// Following style cop enforced rule SA1001: CommasMustBeSpacedCorrectly
//    /// 
//    /// </summary>
//    [Export(typeof(IEngineModule))]
//    [Export(typeof(ICodeMarkerGroupDefinition))]
//    public class SA1001CommaSpacing : CodeMarkerProviderModuleBase
//    {
//        private const string WarningId = "SA1001:CommasMustBeSpacedCorrectly";
//        private const string MarkerText = "SA1001: CommasMustBeSpacedCorrectly";
//        private const string Description = "The spacing around a comma is incorrect, within a C# code file";
//        private const string FixText = "SA1001: CommasMustBeSpacedCorrectly";

//        /// <summary>
//        /// This method is responsible for analyzing a single file and producing warning code markers.
//        /// It gets called whenever the file or its dependencies changes so that reanalysis of the code 
//        /// markers is required
//        /// </summary>
//        protected override void AddCodeMarkers(FileModel fileModel)
//        {
//            // you can use fileModel.All<T> to iterate over the construct you are interested in
//            // you might also use LINQ queries     
//            System.Diagnostics.Debug.WriteLine("***********************************************************************************************");    
//            foreach (IComment comment in fileModel.All<IComment>())
//            {
//                var lineCheck = comment.Text.Trim();
//                if (lineCheck.Length > 3)
//                {                    
//                    if (lineCheck.Substring(0, 3) != "// ")
//                    {
//                        System.Diagnostics.Debug.WriteLine(lineCheck);
//                        comment.AddCodeMarker(WarningId, this);
//                    }
//                }
//            }
//        }

//        /// <summary>
//        /// This porperty statically defines the warning code marker: supported languages, description, 
//        /// default fix text, default apperance and whether it's enabled by default.
//        /// </summary>
//        public override IEnumerable<CodeMarkerGroup> CodeMarkerGroups
//        {
//            get
//            {
//                foreach (var language in new[] { LanguageNames.CSharp })
//                {
//                    yield return CodeMarkerGroup.Define(
//                        language,
//                        WarningId,
//                        CodeMarkerAppearance.Warning,
//                        Description,
//                        true,
//                        MarkerText,
//                        FixText);
//                }
//            }
//        }
//    }
//}
