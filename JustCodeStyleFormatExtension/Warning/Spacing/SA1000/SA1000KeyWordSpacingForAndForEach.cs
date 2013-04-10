namespace JustCodeStyleFormatExtension.Warning.Spacing.SA1000
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
    public class SA1000KeyWordSpacingForAndForEach : CodeMarkerProviderModuleBase
    {
        private readonly WhiteSpaceHelper whiteSpaceHelper = new WhiteSpaceHelper();

        private const string WarningId = "SA1000A";
        private const string MarkerText = "SA1000: Keywords must be spaced correctly";
        private const string Description = "SA1000: Keywords must be spaced correctly";
        private const string FixText = "SA1000: Keywords must be spaced correctly";

        public override IEnumerable<CodeMarkerGroup> CodeMarkerGroups
        {
            get
            {
                foreach (var language in new[] { LanguageNames.CSharp, LanguageNames.VisualBasic, LanguageNames.JavaScript })
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

            foreach (IForEachStatement item in fileModel.All<IForEachStatement>().Where(v => v.ExistsTextuallyInFile))
            {
                List<string> keywordSearch = new List<string> { "for", "foreach", "in" };
                foreach (var key in keywordSearch)
                {
                    if (item.Text.WholeWordIndexOf(key) != -1)
                    {
                        needWarning = this.CheckSpacingAroundKeyword(key, item.Text);
                        if (needWarning == true)
                        {
                            item.AddCodeMarker(WarningId, this, FixSpacingAroundKeywordForeach, item);
                            break;
                        }
                    }
                }
            }

            foreach (IForStatement item in fileModel.All<IForStatement>().Where(v => v.ExistsTextuallyInFile))
            {
                List<string> keywordSearch = new List<string> { "for", "foreach", "in" };

                foreach (var key in keywordSearch)
                {
                    if (item.Text.WholeWordIndexOf(key) != -1)
                    {
                        needWarning = this.CheckSpacingAroundKeyword(key, item.Text);
                        if (needWarning == true)
                        {
                            item.AddCodeMarker(WarningId, this, FixSpacingAroundKeywordFor, item);
                            break;
                        }
                    }
                }
            }

            foreach (ISwitchStatement item in fileModel.All<ISwitchStatement>().Where(v => v.ExistsTextuallyInFile))
            {
                List<string> keywordSearch = new List<string> { "switch" };

                foreach (var key in keywordSearch)
                {
                    if (item.Text.WholeWordIndexOf(key) != -1)
                    {
                        needWarning = this.CheckSpacingAroundKeyword(key, item.Text);
                        if (needWarning == true)
                        {
                            item.AddCodeMarker(WarningId, this, FixSpacingAroundKeywordSwitch, item);
                            break;
                        }
                    }
                }
            }

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

        private void FixSpacingAroundKeywordForeach(IForEachStatement item)
        {
            List<string> keywordSearch = new List<string> { "foreach", "in" };
            foreach (var key in keywordSearch)
            {
                item.Text = this.whiteSpaceHelper.RemoveWhiteSpaceAroundKeyword(item.Text, key);
            }
        }

        private void FixSpacingAroundKeywordFor(IForStatement item)
        {
            List<string> keywordSearch = new List<string> { "for" };
            foreach (var key in keywordSearch)
            {
                item.Text = this.whiteSpaceHelper.RemoveWhiteSpaceAroundKeyword(item.Text, key);
            }
        }

        private void FixSpacingAroundKeywordSwitch(ISwitchStatement item)
        {
            List<string> keywordSearch = new List<string> { "switch" };
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
