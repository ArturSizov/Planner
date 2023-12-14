using Microsoft.AspNetCore.Components;
using MudBlazor;
using Planner.Components.Dialogs;
using Planner.Models;

namespace Planner.Components.Layout
{
    partial class NavMenu
    {
        /// <summary>
        /// Dialog service
        /// </summary>
        [Inject] public IDialogService DialogService { get; set; } = new DialogService();

        /// <summary>
        /// Open dialog window
        /// </summary>
        /// <param name="title"></param>
        public void OpenDialog(string title)
        {
            var options = new DialogOptions { DisableBackdropClick = true, ClassBackground = "blackout" };
            DialogService.Show<CreatingBranch>(title, options);
        }
    }
}
