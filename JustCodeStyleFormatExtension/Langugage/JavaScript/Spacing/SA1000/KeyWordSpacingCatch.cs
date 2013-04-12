namespace JustCodeStyleFormatExtension.Langugage.JavaScript.Spacing.SA1000
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.Composition;
    using System.Linq;
    using JustCodeStyleFormatExtension.Extensions;
    using JustCodeStyleFormatExtension.Helpers;
    using Telerik.JustCode.CommonLanguageModel;

    /// <summary> 
    /// 
    /// Following style cop enforced rule SA1000: Spacing around keywords for, foreach, in
    /// 
    /// </summary>
    [Export(typeof(IEngineModule))]
    [Export(typeof(ICodeMarkerGroupDefinition))]
    public class KeyWordSpacingCatch : CodeMarkerProviderModuleBase
    {
        private readonly WhiteSpaceHelper whiteSpaceHelper = new WhiteSpaceHelper();

        private const string WarningId = "SA1000A-JavaScript-Catch";
        private const string MarkerText = "JavaScript - Spacing around keyword \"Catch\" should be spaced correctly";
        private const string Description = "JavaScript - Spacing around keyword \"Catch\" should be spaced correctly";
        private const string FixText = "JavaScript - Spacing around keyword \"Catch\" should be spaced correctly";

        public override IEnumerable<CodeMarkerGroup> CodeMarkerGroups
        {
            get
            {
                foreach (var language in new[] { LanguageNames.JavaScript })
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

            foreach (ITryStatement item in fileModel.All<ITryStatement>().Where(v => v.ExistsTextuallyInFile))
            {
                List<string> keywordSearch = new List<string> { "catch" };

                foreach (var key in keywordSearch)
                {
                    if (item.Text.WholeWordIndexOf(key) != -1)
                    {
                        needWarning = this.CheckSpacingAroundKeyword(key, item.Text);
                        if (needWarning == true)
                        {
                            item.AddCodeMarker(WarningId, this, FixSpacingAroundKeywordTry, item);
                            break;
                        }
                    }
                }
            }
        }

        private void FixSpacingAroundKeywordTry(ITryStatement item)
        {
            List<string> keywordSearch = new List<string> { "catch" };
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
