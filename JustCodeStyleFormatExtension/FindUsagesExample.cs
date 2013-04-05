using System.Collections.Generic;
using System.ComponentModel.Composition;
using Telerik.JustCode.CommonLanguageModel;
using Telerik.JustCode.CommonLanguageModel.KeyBindings;

namespace JustCodeStyleFormatExtension
{
    /// <summary>
    /// This class implements a sample command that searches for invocations of a selected method and
    /// shows them in JustCode's find results window,  which be shown in JustCode's Navigate menus.
    /// 
    /// TODO: replace this with your own implementation
    /// 
    /// </summary>
    [Export(typeof(IEngineModule))]
    [Export(typeof(ICommandDefinition))]
    public class FindUsagesExample : CommandModuleBase
    {
        /// <summary>
        /// This method gets called when the selection changes inside Visual Studio (editor or solution explorer) to 
        /// update the availability of the command in the new selection context
        /// </summary>
        /// <param name="solutionModel">The SolutionModel of the current solution</param>
        /// <param name="context">The new selection context</param>
        /// <returns>True if the command is enabled/visible in the current selection context</returns>
        public override bool CanExecute(SolutionModel solutionModel, SelectionContext context)
        {
            FileModel fileModel;
            CodeSpan selection;

            if (!solutionModel.IsEditorSelection(context, out fileModel, out selection))
            {
                return false;
            }

            IMethodDeclaration method = fileModel.MethodIdentifierAt(selection);
            return method.ExistsTextuallyInFile;
        }

        /// <summary>
        /// This method gets called when the user executes the command
        /// </summary>
        /// <param name="solutionModel">The SolutionModel of the current solution</param>
        /// <param name="context">The selection context the command is invoked in</param>
        public override void Execute(SolutionModel solutionModel, SelectionContext context)
        {
            FileModel fileModel;
            CodeSpan selection;

            if (!solutionModel.IsEditorSelection(context, out fileModel, out selection))
            {
                return;
            }

            FindMethodInvocations(fileModel, selection, false);
        }

        /// <summary>
        /// Gets called on invocation of the command or when the user selects "Refresh" in the find results window 
        /// </summary>
        private void FindMethodInvocations(FileModel fileModel, CodeSpan selection, bool isRefresh)
        {
            IMethodDeclaration method = fileModel.MethodIdentifierAt(selection);
            if (method.ExistsTextuallyInFile)
            {
                method.SolutionModel.UIProcess.Get<FindResultsWindow>().ShowProgressPopup();
                method.SolutionModel.UIProcess.Get<FindResultsWindow>().StartSearch("Method invocations of '{0}'",
                    method,
                    FindMethodInvocations,
                    isRefresh,
                    false);

                // Iterate all invocations of the method and add them to the find results window
                foreach (IMethodInvocation invocation in method.AllInvocations())
                {
                    invocation.SolutionModel.UIProcess.Get<FindResultsWindow>().AddResult(invocation, true, false);
                }

                method.SolutionModel.UIProcess.Get<FindResultsWindow>().FinishSearch();
                method.SolutionModel.UIProcess.Get<FindResultsWindow>().HideProgressPopup();
            }
        }

        /// <summary>
        /// String that uniquely identifies the command
        /// </summary>
        public override string CommandIdentifier
        {
            get
            {
                return "JustCodeStyleFormatExtensionFindUsagesExample";
            }
        }

        /// <summary>
        /// Text that appears in JustCode menus
        /// </summary>
        public override string Text
        {
            get
            {
                return "Extension: Find All Method Invocations";
            }
        }

        /// <summary>
        /// Optional, defines a command shortcut
        /// </summary>
        public override IEnumerable<KeyBinding> GetKeyBindings(KeyBindingProfile profile)
        {
            yield return new KeyBinding(VSScopes.TextEditor, new KeyCombination(KeyboardModifierKeys.Ctrl, KeyboardKey.Key6));
        }

        /// <summary>
        /// Position the command inside JustCode menus
        /// </summary>
        public override System.Collections.Generic.IEnumerable<CommandMenuLocation> MenuLocations
        {
            get
            {
                return PlaceInNavigateMenus(100, 1);
            }
        }
    }
}