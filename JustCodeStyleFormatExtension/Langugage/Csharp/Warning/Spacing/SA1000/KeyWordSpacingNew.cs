namespace JustCodeStyleFormatExtension.Langugage.Csharp.Warning.Spacing.SA1000
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.Composition;
    using StyleFormatEngine.Helpers.Spacing.AddSpacing;
    using Telerik.JustCode.CommonLanguageModel;

    /// <summary> 
    /// 
    /// Following style cop enforced rule SA1000: Spacing around keywords New
    /// 
    /// </summary>
    [Export(typeof(IEngineModule))]
    [Export(typeof(ICodeMarkerGroupDefinition))]
    public class KeyWordSpacingNew : CodeMarkerProviderModuleBase
    {
        private readonly AddSpaceHelper whiteSpaceHelper = new AddSpaceHelper();

        private const string WarningId = "SA1000A-CSharp-New";
        private const string MarkerText = "CSharp - Spacing around keyword \"New\" should be spaced correctly";
        private const string Description = "CSharp - Spacing around keyword \"New\" should be spaced correctly";
        private const string FixText = "CSharp - Spacing around keyword \"New\" should be spaced correctly";

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

        protected override void AddCodeMarkers(FileModel fileModel)
        {
            var needWarning = false;
            // Grabs the first two rows of a list
            foreach (IVariableDeclaration item in fileModel.All<IVariableDeclaration>().Where(v => v.ExistsTextuallyInFile))
            {
                List<string> keywordSearch = new List<string> { "new" };
                foreach (var key in keywordSearch)
                {
                    if (item.Text.Contains(key))
                    {
                        needWarning = this.CheckSpacingAroundKeyword(key, item.Text.ToString());
                        if (needWarning == true)
                        {
                            item.AddCodeMarker(WarningId, this, FixSpacingAroundKeywordVarDec, item);
                        }
                    }
                }
            }

            // Grabs everything else
            foreach (IAssignmentExpression item in fileModel.All<IAssignmentExpression>().Where(v => v.ExistsTextuallyInFile))
            {
                List<string> keywordSearch = new List<string> { "new" };
                foreach (var key in keywordSearch)
                {
                    if (item.Text.Contains(key))
                    {
                        needWarning = this.CheckSpacingAroundKeyword(key, item.Text.ToString());
                        if (needWarning == true)
                        {
                            item.AddCodeMarker(WarningId, this, FixSpacingAroundKeywordAsExp, item);
                        }
                    }
                }
            }
        }

        private void FixSpacingAroundKeywordVarDec(IVariableDeclaration item)
        {
            List<string> keywordSearch = new List<string> { "new" };
            foreach (var key in keywordSearch)
            {
                item.Text = this.whiteSpaceHelper.RemoveWhiteSpaceAroundKeyword(item.Text, key);
            }
        }

        private void FixSpacingAroundKeywordAsExp(IAssignmentExpression item)
        {
            List<string> keywordSearch = new List<string> { "new" };
            foreach (var key in keywordSearch)
            {
                item.Text = this.whiteSpaceHelper.RemoveWhiteSpaceAroundKeyword(item.Text, key);
            }
        }

        private bool CheckSpacingAroundKeyword(string key, string item)
        {
            return whiteSpaceHelper.CheckWhiteSpaceAroundKeyword(item, key);
        }
    }
}
