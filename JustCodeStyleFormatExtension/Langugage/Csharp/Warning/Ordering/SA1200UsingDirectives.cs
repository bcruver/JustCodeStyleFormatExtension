namespace JustCodeStyleFormatExtension.Langugage.Csharp.Warning.Ordering
{
    /// <summary> 
    /// 
    /// Following style cop enforced rule SA1200 will warn about any using statement outside the namespace
    /// 
    /// </summary>
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.Composition;
    using System.Linq;
    using Telerik.JustCode.CommonLanguageModel;

    [Export(typeof(IEngineModule))]
    [Export(typeof(ICodeMarkerGroupDefinition))]
    public class SA1200UsingDirectives : CodeMarkerProviderModuleBase
    {
        private const string WarningId = "SA1200";
        private const string MarkerText = "Using directives should be placed inside the namespace";
        private const string Description = "Using directives should be placed inside the namespace";
        private const string FixText = "Using directives should be placed inside the namespace";

        /// <summary>
        /// This porperty statically defines the warning code marker: supported languages, description, 
        /// default fix text, default apperance and whether it's enabled by default.
        /// </summary>
        public override IEnumerable<CodeMarkerGroup> CodeMarkerGroups
        {
            get
            {
                foreach (var language in new[] { LanguageNames.CSharp })
                {
                    yield return CodeMarkerGroup.Define(
                        language,
                        WarningId,
                        CodeMarkerAppearance.Warning,
                        Description,
                        true,
                        MarkerText,
                        FixText);
                }
            }
        }

        /// <summary>
        /// This method is responsible for analyzing a single file and producing warning code markers.
        /// It gets called whenever the file or its dependencies changes so that reanalysis of the code 
        /// markers is required
        /// </summary>
        protected override void AddCodeMarkers(FileModel fileModel)
        {
            // you can use fileModel.All<T> to iterate over the construct you are interested in
            // you might also use LINQ queries                     
            CheckUsing(fileModel);
        }

        public void CheckUsing(FileModel fileModel)
        {
            if (fileModel.FileName != "AssemblyInfo")
            {
                foreach (IUsingDirectiveSection section in fileModel.All<IUsingDirectiveSection>())
                {
                    if (!section.Enclosing<INamespaceDeclaration>().Exists && section.Directives.Count() > 0)
                    {
                        section.AddCodeMarker(WarningId, this, MoveUsingStatements, fileModel);
                        break;
                    }
                }
            }           
        }        

        private void MoveUsingStatements(FileModel fileModel)
        {
            List<IUsingDirectiveSection> innerSections = new List<IUsingDirectiveSection>();
            IUsingDirectiveSection mainSection = null;

            foreach (IUsingDirectiveSection section in fileModel.All<IUsingDirectiveSection>())
            {
                if (section.Enclosing<INamespaceDeclaration>().Exists)
                {
                    innerSections.Add(section);
                }
                else if (mainSection == null)
                {
                    mainSection = section;
                }
            }

            if (mainSection != null)
            {
                foreach (IUsingDirectiveSection section in innerSections)
                {
                    section.Insert(mainSection.Directives, section.FileModel);
                }

                foreach (IUsingDirective directive in mainSection.Directives)
                {
                    directive.Remove();
                }
            }
        }
    }
}