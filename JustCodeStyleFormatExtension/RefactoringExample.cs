// namespace JustCodeStyleFormatExtension
// {
//    /// <summary>
//    /// This class implements a sample command to convert fields to auto-implemented properties, 
//    /// which be shown in JustCode's Refactor menus.
//    /// 
//    /// TODO: replace this with your own implementation
//    /// 
//    /// </summary>
//    using System.Collections.Generic;
//    using System.ComponentModel.Composition;
//    using Telerik.JustCode.CommonLanguageModel;
//    using Telerik.JustCode.CommonLanguageModel.Extensions;
//    using Telerik.JustCode.CommonLanguageModel.KeyBindings;

//    [Export(typeof(IEngineModule))]
//    [Export(typeof(ICommandDefinition))]
//    public class RefactoringExample : CommandModuleBase
//    {
//        /// <summary>
//        /// This method gets called when the selection changes inside Visual Studio (editor or solution explorer) to 
//        /// update the availability of the command in the new selection context
//        /// </summary>
//        /// <param name="solutionModel">The SolutionModel of the current solution</param>
//        /// <param name="context">The new selection context</param>
//        /// <returns>True if the command is enabled/visible in the current selection context</returns>
//        public override bool CanExecute(SolutionModel solutionModel, SelectionContext context)
//        {
//            FileModel fileModel;
//            CodeSpan selection;

//            if (!solutionModel.IsEditorSelection(context, out fileModel, out selection))
//            {
//                return false;
//            }

//            IFieldDeclaration field = fileModel.InnerMost<IFieldDeclaration>(selection);
//            return field.ExistsTextuallyInFile;
//        }

//        /// <summary>
//        /// This method gets called when the user executes the command
//        /// </summary>
//        /// <param name="solutionModel">The SolutionModel of the current solution</param>
//        /// <param name="context">The selection context the command is invoked in</param>
//        public override void Execute(SolutionModel solutionModel, SelectionContext context)
//        {
//            FileModel fileModel;
//            CodeSpan selection;

//            if (!solutionModel.IsEditorSelection(context, out fileModel, out selection))
//            {
//                return;
//            }

//            IFieldDeclaration field = fileModel.InnerMost<IFieldDeclaration>(selection);

//            IPropertyDeclaration autoImplementedProperty = field.Language.AutoImplementedProperty(
//                                                               field.Language.None<IDocComment>(),
//                                                               field.Attributes,
//                                                               field.Language.Modifiers(field.Modifiers.Modifiers.DisableFlag(Modifiers.Private).AddFlag(Modifiers.Public)),
//                                                               field.Language.TypeName(field.TypeName.Type),
//                                                               field.Language.None<IIdentifier>());

//            NamingPolicy propertyNamingPolicy = autoImplementedProperty.PrimaryNamingPolicy(fileModel.UserSettings);
//            string propertyName = propertyNamingPolicy.ChangeNameAccordingToPolicy(field, fileModel);
//            if (!field.Language.AreNamesEqual(propertyName, field.Identifier.Name))
//            {
//                propertyName = propertyNamingPolicy.MakeMemberNameUniqueInScope(field, propertyName, MakeNameUniqueOptions.NameAlreadyMatchesNamingPolicy);
//            }

//            if (field.ExistsTextuallyInFile)
//            {
//                // Iterate all accesses of the field and update them
//                foreach (IMemberAccess fieldAccess in field.AllAccesses())
//                {
//                    fieldAccess.Identifier.ReplaceWith(fieldAccess.Language.Identifier(propertyName));
//                }

//                autoImplementedProperty.Identifier = field.Language.Identifier(propertyName);

//                field.ReplaceWith(autoImplementedProperty);
//            }
//        }

//        /// <summary>
//        /// String that uniquely identifies the command
//        /// </summary>
//        public override string CommandIdentifier
//        {
//            get
//            {
//                return "JustCodeStyleFormatExtensionRefactoringExample";
//            }
//        }

//        /// <summary>
//        /// Text that appears in JustCode menus
//        /// </summary>
//        public override string Text
//        {
//            get
//            {
//                return "Extension: Convert field to auto-implemented property";
//            }
//        }

//        /// <summary>
//        /// Optional, defines a command shortcut
//        /// </summary>
//        public override IEnumerable<KeyBinding> GetKeyBindings(KeyBindingProfile profile)
//        {
//            yield return new KeyBinding(VSScopes.TextEditor, new KeyCombination(KeyboardModifierKeys.Ctrl, KeyboardKey.Key5));
//        }

//        /// <summary>
//        /// Position the command inside JustCode menus
//        /// </summary>
//        public override System.Collections.Generic.IEnumerable<CommandMenuLocation> MenuLocations
//        {
//            get
//            {
//                return PlaceInRefactorMenus(100, 1);
//            }
//        }
//    }
// }