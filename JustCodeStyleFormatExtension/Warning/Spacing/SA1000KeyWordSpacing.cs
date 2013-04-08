namespace JustCodeStyleFormatExtension.Warning.Spacing
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.Composition;
    using JustCodeStyleFormatExtension.Helpers;
    using Telerik.JustCode.CommonLanguageModel;

    /// <summary> 
    /// 
    /// Following style cop enforced rule SA1000: Spacing around keywords
    /// 
    /// </summary>
    [Export(typeof(IEngineModule))]
    [Export(typeof(ICodeMarkerGroupDefinition))]
    public class SA1000KeyWordSpacing : CodeMarkerProviderModuleBase
    {
        private readonly WhiteSpaceHelper whiteSpaceHelper = new WhiteSpaceHelper();
        private readonly VarableListHelper varableListHelper = new VarableListHelper();

        private const string WarningId = "SA1000";
        private const string MarkerText = "SA1000: Must be a single space after Keyword";
        private const string Description = "SA1000: Must be a single space after Keyword";
        private const string FixText = "SA1000: Must be a single space after Keyword";

    
        protected override void AddCodeMarkers(FileModel fileModel)
        {
           // List<string> keywordSearch = varableListHelper.GetKeyWordList();
            var needWarning = false;

            Dictionary<IMemberDeclaration, List<IVariableDeclaration>> variableDeclarations = new Dictionary<IMemberDeclaration, List<IVariableDeclaration>>();

            // Grabs the first two rows of a list
            foreach (IVariableDeclaration item in fileModel.All<IVariableDeclaration>().Where(v => v.ExistsTextuallyInFile))
            {
                List<string> keywordSearch = new List<string> { "new" };
                foreach (var key in keywordSearch)
                {
                    if (item.Text.Contains(key))
                    {
                        needWarning = whiteSpaceHelper.NeedWarningForSingleWhiteSpaceAfterKeyword(item.Text, key);

                        if (needWarning == true)
                        {
                            // item.AddCodeMarker(WarningId, this, AddSpaceAfterSingleComment, comment);
                            item.AddCodeMarker(WarningId, this);
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
                        needWarning = whiteSpaceHelper.NeedWarningForSingleWhiteSpaceAfterKeyword(item.Text, key);

                        if (needWarning == true)
                        {
                            // item.AddCodeMarker(WarningId, this, AddSpaceAfterSingleComment, comment);
                            item.AddCodeMarker(WarningId, this);
                        }
                    }
                }
            }


            //foreach (IDeclaration item in fileModel.All<IDeclaration>())
            //{
            //    var test = item;
            //}

            //foreach (IExpression item in fileModel.All<IExpression>())
            //{
            //    var test = item;
            //}

            //foreach (ILabeledStatement item in fileModel.All<ILabeledStatement>())
            //{
            //    var test = item;
            //}

            //foreach (IStringLiteral item in fileModel.All<IStringLiteral>())
            //{
            //    var test = item;
            //}

            //foreach (IMemberDeclaration item in fileModel.All<IMemberDeclaration>())
            //{
            //    var test = item;
            //}

            //foreach (IMemberUsage item in fileModel.All<IMemberUsage>())
            //{
            //    var test = item;
            //}

            //foreach (IModifiers item in fileModel.All<IModifiers>())
            //{
            //    var test = item;
            //}

            //foreach (IParameter item in fileModel.All<IParameter>())
            //{
            //    var test = item;
            //}

            //foreach (IParameters item in fileModel.All<IParameters>())
            //{
            //    var test = item;
            //}

            //foreach (IPointerTypeName item in fileModel.All<IPointerTypeName>())
            //{
            //    var test = item;
            //}

            //foreach (IQualifiedTypeName item in fileModel.All<IQualifiedTypeName>())
            //{
            //    var test = item;
            //}

            //foreach (IReturnStatement item in fileModel.All<IReturnStatement>())
            //{
            //    var test = item;
            //}

            //foreach (ISimpleTypeName item in fileModel.All<ISimpleTypeName>())
            //{
            //    var test = item;
            //}

            //foreach (IStringLiteral item in fileModel.All<IStringLiteral>())
            //{
            //    var test = item;
            //}

            //foreach (ITypeName item in fileModel.All<ITypeName>())
            //{
            //    var test = item;
            //}

            //foreach (IVariableDeclaration item in fileModel.All<IVariableDeclaration>())
            //{
            //    var test = item;
            //}


            //foreach (IStatement item in fileModel.All<IStatement>())
            //{
            //    foreach (var key in keywordSearch)
            //    {
            //        if (item.Text.Contains(key))
            //        {
            //            needWarning = whiteSpaceHelper.NeedWarningWhiteSpaceBeforeKeyword(item.Text, key);

            //            if (needWarning == true)
            //            {
            //                // item.AddCodeMarker(WarningId, this, AddSpaceAfterSingleComment, comment);
            //                item.AddCodeMarker(WarningId, this);
            //            }
            //            else
            //            {
            //                needWarning = whiteSpaceHelper.NeedWarningForSingleWhiteSpaceAfterKeyword(item.Text, key);

            //                if (needWarning == true)
            //                {
            //                    // item.AddCodeMarker(WarningId, this, AddSpaceAfterSingleComment, comment);
            //                    item.AddCodeMarker(WarningId, this);
            //                }
            //            }
            //        }
            //    }
            //}
        }

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
    }
}
