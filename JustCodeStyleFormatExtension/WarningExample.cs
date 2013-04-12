// using System.Collections.Generic;
// using System.ComponentModel.Composition;
// using Telerik.JustCode.CommonLanguageModel;

// namespace JustCodeStyleFormatExtension
//{
//    /// <summary>
//    /// This class implements a sample warning for fields called "foo" that JustCode will show. 
//    /// 
//    /// TODO: replace this with your own implementation
//    /// 
//    /// </summary>
//    [Export(typeof(IEngineModule))]
//    [Export(typeof(ICodeMarkerGroupDefinition))]
//    public class WarningExample : CodeMarkerProviderModuleBase
//    {
//        private const string WarningExampleID = "JustCodeStyleFormatExtensionWarningExampleMarker";
//        private const string MarkerText = "Example Marker Text";
//        private const string Description = "Example Description";
//        private const string FixText = "Example Fix Text";

//        /// <summary>
//        /// This method is responsible for analyzing a single file and producing warning code markers.
//        /// It gets called whenever the file or its dependencies changes so that reanalysis of the code 
//        /// markers is required
//        /// </summary>
//        protected override void AddCodeMarkers(FileModel fileModel)
//        {
//            // you can use fileModel.All<T> to iterate over the construct you are interested in
//            // you might also use LINQ queries
//            foreach (IFieldDeclaration field in fileModel.All<IFieldDeclaration>().Where(x => x.Identifier.Name.Contains("foo")))
//            {
//                field.AddCodeMarker(WarningExampleID, this, RemoveField, field);
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
//                foreach (var language in new[] { LanguageNames.CSharp, LanguageNames.VisualBasic })
//                {
//                    yield return CodeMarkerGroup.Define(
//                        language,
//                        WarningExampleID,
//                        CodeMarkerAppearance.Warning,
//                        Description,
//                        true,
//                        MarkerText,
//                        FixText);
//                }
//            }
//        }

//        /// <summary>
//        /// This method is executed when the user executes the quick fix for the warning
//        /// </summary>
//        /// <param name="field"></param>
//        private void RemoveField(IFieldDeclaration field)
//        {
//            field.Remove();
//        }
//    }
// }