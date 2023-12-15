using Microsoft.AspNetCore.Components;
using MudBlazor;
using Planner.Models;

namespace Planner.Components.Dialogs
{
    partial class CreatingBranch
    {
        /// <summary>
        /// MudDialogInstance
        /// </summary>
        [CascadingParameter] MudDialogInstance? MudDialog { get; set; }

        /// <summary>
        /// Validation of the OK button
        /// </summary>
        public bool Success { get; set; } = true;

        /// <summary>
        /// Company model
        /// </summary>
        public CompanyModel Company { get; set; } = new();

        /// <summary>
        /// Ok method
        /// </summary>
        public void Submit() => MudDialog?.Close(DialogResult.Ok(true));

        /// <summary>
        /// Close dialog window
        /// </summary>
        public void Cancel() => MudDialog?.Cancel();

        /// <summary>
        /// Name validations
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public IEnumerable<string> NameStrength(string name)
        {
            if (name.Length <= 0)
            {
                Success = true;
                yield return "Не может быть пустым";
            }
            if (name.Length < 2)
            {
                Success = true;
                yield return "Не менее 2-ух символов";
            }           
            else
                Success = false;
        }
    }
}
