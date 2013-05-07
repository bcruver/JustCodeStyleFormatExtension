namespace JustCodeStyleFormatExtension.Langugage.Csharp.Warning.Spacing.SA1008
{
    /// <summary> 
    /// 
    /// Following style cop enforced rule SA1005: SingleLineCommentsMustBeginWithSingleSpace
    /// 
    /// </summary>
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.Composition;
    using StyleFormatEngine.Helpers.Spacing.AddSpacing;
    using Telerik.JustCode.CommonLanguageModel;

    [Export(typeof(IEngineModule))]
    [Export(typeof(ICodeMarkerGroupDefinition))]
    public class OpeningParenthesisSpacing : CodeMarkerProviderModuleBase
    {
        private readonly AddSpaceHelper whiteSpaceHelper = new AddSpaceHelper();

        private const string WarningId = "SA1008-Csharp";
        private const string MarkerText = "CSharp - An opening parenthesis must not start with a space";
        private const string Description = "CSharp - An opening parenthesis must not start with a space";
        private const string FixText = "CSharp - An opening parenthesis must not start with a space";

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

        protected override void AddCodeMarkers(FileModel fileModel)
        {
            foreach (IForEachStatement item in fileModel.All<IForEachStatement>().Where(v => v.ExistsTextuallyInFile))
            {
                var lineCheck = item.Text.Trim();

                if (lineCheck.IndexOf("(") != -1)
                {
                    var result = whiteSpaceHelper.CheckNoWhiteSpaceAroundCharacter(lineCheck, "(");

                    if (result == true)
                    {
                        item.AddCodeMarker(WarningId, this, FixSpacingForEach, item);
                    }
                }
            }

            foreach (IForStatement item in fileModel.All<IForStatement>().Where(v => v.ExistsTextuallyInFile))
            {
                var lineCheck = item.Text.Trim();

                if (lineCheck.IndexOf("(") != -1)
                {
                    var result = whiteSpaceHelper.CheckNoWhiteSpaceAroundCharacter(lineCheck, "(");

                    if (result == true)
                    {
                        item.AddCodeMarker(WarningId, this, FixSpacingFor, item);
                    }
                }
            }
        }

        private void FixSpacingForEach(IForEachStatement item)
        {
            
        }

        private void FixSpacingFor(IForStatement item)
        {

        }



        private void AddSpaceAfterSingleComment(IComment comment)
        {
            comment.Text = this.whiteSpaceHelper.RemoveWhiteSpaceAroundKeyword(comment.Text, "//");
        }

        private void AddSpaceAfterMultiComments(IComment comment)
        {
            comment.Text = this.whiteSpaceHelper.RemoveWhiteSpaceAroundKeyword(comment.Text, "/*");
        }


    }
}
