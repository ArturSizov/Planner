using Microsoft.AspNetCore.Components;
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
        public async Task<(bool, object)> CreateItemDialog<T>(string title, DialogParameters parameters) where T : ComponentBase
        {
            var options = new DialogOptions { DisableBackdropClick = true, ClassBackground = "blackout" };

            var dialog = _dialogService.Show<T>(title, parameters, options);

            var result = await dialog.Result;

            var returnedData = await dialog.GetReturnValueAsync<object>();

            if (!result.Canceled)
                return (true, returnedData);

            return (false, returnedData);
        }

        /// <inheritdoc/>
        public async Task<bool> DeleteItemDialog(string item)
        {
            var parameters = new DialogParameters<CustomMudDialog>
            {
                { x => x.ContentText, $"Вы действительно хотите удалить {item}?" },
                { x => x.ButtonText, "Да" },
                { x => x.Color, MudBlazor.Color.Error }
            };

            var options = new DialogOptions() { CloseButton = true, MaxWidth = MaxWidth.False };

            var dialog = _dialogService.Show<CustomMudDialog>("Удаление", parameters, options);

            var result = await dialog.Result;

            if (!result.Canceled)
                return true;

            return false;
        }
    }
}
