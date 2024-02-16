using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace Planner.Components.Dialogs
{
    public partial class Notes
    {
        /// <summary>
        /// Mud Dialog Instance
        /// </summary>
        [CascadingParameter] MudDialogInstance? MudDialog { get; set; }

        public string? Text { get; set; }


        /// <summary>
        /// Ok method
        /// </summary>
        public void Submit() => MudDialog?.Close();

        /// <summary>
        /// Close dialog window
        /// </summary>
        public void Cancel() => MudDialog?.Cancel();
    }
}
