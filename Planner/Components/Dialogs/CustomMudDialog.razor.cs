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
        [Parameter] public MarkupString ContentText { get; set; }

        /// <summary>
        /// Ok button content
        /// </summary>
        [Parameter] public string OkButtonText { get; set; } = string.Empty;

        /// <summary>
        /// No button content
        /// </summary>
        [Parameter] public string NoButtonText { get; set; } = "Отмена";

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
