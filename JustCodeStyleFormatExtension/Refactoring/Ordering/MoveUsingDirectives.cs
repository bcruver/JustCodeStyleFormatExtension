namespace JustCodeStyleFormatExtension.Refactoring.Ordering
{
    using System.Collections.Generic;
    using System.ComponentModel.Composition;
    using System.Linq;
    using Telerik.JustCode.CommonLanguageModel;

    [Export(typeof(IEngineModule))]
    [Export(typeof(ICommandDefinition))]
    public class MoveUsingDirectives : CommandModuleBase
    {
        public override bool CanExecute(SolutionModel solutionModel, SelectionContext context)
        {
            FileModel fileModel;
            CodeSpan selection;

            if (!solutionModel.IsEditorSelection(context, out fileModel, out selection))
            {
                return false;
            }

            IUsingDirectiveSection section = fileModel.InnerMost<IUsingDirectiveSection>(selection);

            return section.Exists && !section.Enclosing<INamespaceDeclaration>().Exists;
        }

        public override void Execute(SolutionModel solutionModel, SelectionContext context)
        {
            FileModel fileModel;
            CodeSpan selection;

            if (!solutionModel.IsEditorSelection(context, out fileModel, out selection))
            {
                return;
            }

            IUsingDirectiveSection mainSection = fileModel.InnerMost<IUsingDirectiveSection>(selection);

            if (mainSection.Exists)
            {
                HashSet<IUsingDirectiveSection> sections = new HashSet<IUsingDirectiveSection>();
                foreach (IUsingDirectiveSection section in fileModel.All<IUsingDirectiveSection>())
                {
                    INamespaceDeclaration nameSpaceItems = section.Enclosing<INamespaceDeclaration>();

                    //we are interested only in top level namespaces
                    if (nameSpaceItems.Exists && !nameSpaceItems.Enclosing<INamespaceDeclaration>().Exists)
                    {
                        sections.Add(section);
                    }
                }

                foreach (IUsingDirectiveSection section in sections)
                {
                    section.Insert(mainSection.Directives, fileModel);
                }
            }

            foreach (IUsingDirective directive in mainSection.Directives)
            {
                directive.Remove();
            }
        }

        /// <summary>
        /// String that uniquely identifies the command
        /// </summary>
        public override string CommandIdentifier
        {
            get
            {
                return "MoveUsingDirectives";
            }
        }

        /// <summary>
        /// Text that appears in JustCode menus
        /// </summary>
        public override string Text
        {
            get
            {
                return "SA1200: Move using directives inside namespaces";
            }
        }

        /// <summary>
        /// Position the command inside JustCode menus
        /// </summary>
        public override System.Collections.Generic.IEnumerable<CommandMenuLocation> MenuLocations
        {
            get
            {
                return PlaceInRefactorMenus(100, 1);
            }
        }
    }
}