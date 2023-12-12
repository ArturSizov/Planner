using Microsoft.AspNetCore.Components;
using MudBlazor;
using Planner.Components.Dialogs;

namespace Planner.Components.Layout
{
    partial class NavMenu
    {
        [Inject]
        public IDialogService DialogService { get; set; } = new DialogService();

        public void OpenDialog()
        {
            var options = new DialogOptions { CloseOnEscapeKey = true };
            DialogService.Show<CreatingBranch>("Last element focused", options);
        }

    }
}
