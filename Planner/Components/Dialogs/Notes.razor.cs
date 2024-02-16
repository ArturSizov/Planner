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

        /// <summary>
        /// Text note parameter
        /// </summary>
        [Parameter] public string? Text { get; set; }

        /// <summary>
        /// Ok method
        /// </summary>
        public void Submit() => MudDialog?.Close(Text);

        /// <summary>
        /// Close dialog window
        /// </summary>
        public void Cancel() => MudDialog?.Cancel();

        /// <summary>
        /// Clearing text
        /// </summary>
        public void ClearText()
        {
            Text = null;
        }

        protected override void OnInitialized()
        {
            
        }
    }
}
