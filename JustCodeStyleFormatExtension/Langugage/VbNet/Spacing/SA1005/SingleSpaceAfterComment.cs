namespace JustCodeStyleFormatExtension.Langugage.VbNet.Spacing.SA1005
{
    /// <summary> 
    /// 
    /// Following style cop enforced rule SA1005: SingleLineCommentsMustBeginWithSingleSpace
    /// 
    /// </summary>
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.Composition;
    using JustCodeStyleFormatExtension.Helpers;
    using Telerik.JustCode.CommonLanguageModel;

    [Export(typeof(IEngineModule))]
    [Export(typeof(ICodeMarkerGroupDefinition))]
    public class SingleSpaceAfterComment : CodeMarkerProviderModuleBase
    {
        private readonly WhiteSpaceHelper whiteSpaceHelper = new WhiteSpaceHelper();

        private const string WarningId = "SA1005-VBNET";
        private const string MarkerText = "VBNET - Comments that start with ' should begin with a single space";
        private const string Description = "VBNET - Comments that start with ' should begin with a single space.";
        private const string FixText = "VBNET - Comments that start with ' should begin with a single space";

        /// <summary>
        /// This method is responsible for analyzing a single file and producing warning code markers.
        /// It gets called whenever the file or its dependencies changes so that reanalysis of the code 
        /// markers is required
        /// </summary>
        protected override void AddCodeMarkers(FileModel fileModel)
        {
            // you can use fileModel.All<T> to iterate over the construct you are in terested in
            // you might also use LINQ queries
            foreach (IComment comment in fileModel.All<IComment>().Where(v => v.ExistsTextuallyInFile))
            {
                var lineCheck = comment.Text.Trim();              
                CheckForSingleComments(lineCheck, comment);
            }
        }

        private void CheckForSingleComments(string lineCheck, IComment comment)
        {
            var result = whiteSpaceHelper.CheckWhiteSpaceAroundComment(lineCheck, "'");
            // var result = this.whiteSpaceHelper.NeedWarningForSingleWhiteSpaceAfterKeyword(lineCheck, "//");

            if(result == true)
            {
                comment.AddCodeMarker(WarningId, this, AddSpaceAfterSingleComment, comment);
            }
        }

        /// <summary>
        /// This porperty statically defines the warning code marker: supported languages, description, 
        /// default fix text, default apperance and whether it's enabled by default.
        /// </summary>
        public override IEnumerable<CodeMarkerGroup> CodeMarkerGroups
        {
            get
            {
                foreach (var language in new[] { LanguageNames.VisualBasic })
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

        private void AddSpaceAfterSingleComment(IComment comment)
        {
            comment.Text = this.whiteSpaceHelper.RemoveWhiteSpaceAroundKeyword(comment.Text, "'");
        }
    }
}