using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using JustCodeStyleFormatExtension.Helpers;
using Telerik.JustCode.CommonLanguageModel;

namespace JustCodeStyleFormatExtension.Warning.Spacing
{
    /// <summary> 
    /// 
    /// Following style cop enforced rule SA1005: SingleLineCommentsMustBeginWithSingleSpace
    /// 
    /// </summary>
    [Export(typeof(IEngineModule))]
    [Export(typeof(ICodeMarkerGroupDefinition))]
    public class SA1005SingleLineCommentsMustBeginWithSingleSpace : CodeMarkerProviderModuleBase
    {
        private readonly WhiteSpaceHelper whiteSpaceHelper = new WhiteSpaceHelper();

        private const string WarningId = "SA1005";
        private const string MarkerText = "SA1005: Comments Must Begin With Single Space";
        private const string Description = "A single-line comment within a C# code file does not begin with a single space.";
        private const string FixText = "SA1005: Comments Must Begin With Single Space";

        /// <summary>
        /// This method is responsible for analyzing a single file and producing warning code markers.
        /// It gets called whenever the file or its dependencies changes so that reanalysis of the code 
        /// markers is required
        /// </summary>
        protected override void AddCodeMarkers(FileModel fileModel)
        {
            // you can use fileModel.All<T> to iterate over the construct you are interested in
            // you might also use LINQ queries
            foreach (IComment comment in fileModel.All<IComment>())
            {
                var lineCheck = comment.Text.Trim();

                if(lineCheck.IndexOf("/*") != -1)
                {
                    CheckForMultiLineComments(lineCheck, comment);
                }else
                {
                    CheckForSingleComments(lineCheck, comment);
                }
            }
        }

        private void CheckForMultiLineComments(string lineCheck, IComment comment)
        {
            var result = this.whiteSpaceHelper.NeedWarningForSingleWhiteSpaceAfterKeyword(lineCheck, "/*");

            if (result == true)
            {
                comment.AddCodeMarker(WarningId, this, AddSpaceAfterMultiComments, comment);
            }
        }

        private void CheckForSingleComments(string lineCheck, IComment comment)
        {
            var result = this.whiteSpaceHelper.NeedWarningForSingleWhiteSpaceAfterKeyword(lineCheck, "//");

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

        private void AddSpaceAfterSingleComment(IComment comment)
        {
            comment.Text = this.whiteSpaceHelper.RemoveAddSingleWhiteSpaceAfterKeyword(comment.Text, "//");
        }

        private void AddSpaceAfterMultiComments(IComment comment)
        {
            comment.Text = this.whiteSpaceHelper.RemoveAddSingleWhiteSpaceAfterKeyword(comment.Text, "/*");
        }
    }
}