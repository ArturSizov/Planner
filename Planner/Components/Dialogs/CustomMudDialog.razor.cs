using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace Planner.Components.Dialogs
{
    /// <summary>
    /// Dialog window
    /// </summary>
    partial class CustomMudDialog
    {
        /// <summary>
        /// Mud Dialog Instance
        /// </summary>
        [CascadingParameter] MudDialogInstance MudDialog { get; set; } = new();

        /// <summary>
        /// Message content
        /// </summary>
        [Parameter] public string ContentText { get; set; } = string.Empty;

        /// <summary>
        /// Button content
        /// </summary>
        [Parameter] public string ButtonText { get; set; } = string.Empty;

        /// <summary>
        /// Background color
        /// </summary>
        [Parameter] public MudBlazor.Color Color { get; set; }

        /// <summary>
        /// Ok button
        /// </summary>
        public void Submit() => MudDialog.Close(DialogResult.Ok(true));

        /// <summary>
        /// Cancel button
        /// </summary>
        public void Cancel() => MudDialog.Cancel();
    }
}
