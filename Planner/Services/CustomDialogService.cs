using MudBlazor;
using Planner.Abstractions;
using Planner.Components.Dialogs;

namespace Planner.Services
{
    /// <summary>
    /// Custom dialog service
    /// </summary>
    public class CustomDialogService : ICustomDialogService
    {
        /// <summary>
        /// Mud Dialog service
        /// </summary>
        private readonly IDialogService _dialogService;

        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="dialogService">Mud Dialog service</param>
        public CustomDialogService(IDialogService dialogService)
        {
            _dialogService = dialogService;
        }

        /// <inheritdoc/>
        public async Task<bool> CreateItemDialog(string title)
        {
            var options = new DialogOptions { DisableBackdropClick = true, ClassBackground = "blackout" };
            var dialog = _dialogService.Show<CreateItem>(title, options);
            
            var result = await dialog.Result;

            if (!result.Canceled)
                return true;

            return false;
        }

        /// <inheritdoc/>
        public async Task<bool> DeleteItemDialog(string item)
        {
            var parameters = new DialogParameters<CustomMudDialog>();
            parameters.Add(x => x.ContentText, $"Вы действительно хотите удалить {item}?");
            parameters.Add(x => x.ButtonText, "Удалить");
            parameters.Add(x => x.Color, MudBlazor.Color.Error);

            var options = new DialogOptions() { CloseButton = true, MaxWidth = MaxWidth.False };

            var dialog = _dialogService.Show<CustomMudDialog>("Удалить", parameters, options);
            var result = await dialog.Result;

            if (!result.Canceled)
                return true;

            return false;
        }
    }
}
