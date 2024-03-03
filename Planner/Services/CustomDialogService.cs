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

        /// <inheritdoc/>
        public IDialogReference? DialogReference { get; set; }

        /// <inheritdoc/>
        public bool IsOpened { get; set; }

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

            DialogReference = _dialogService.Show<T>(title, parameters, options);

            var result = await DialogReference.Result;

            var returnedData = await DialogReference.GetReturnValueAsync<object>();

            if (IsOpened)
            {
                IsOpened = false;

                return (false, returnedData);
            }

            if (!result.Canceled)
                return (true, returnedData);

            return (false, returnedData);
        }

        /// <inheritdoc/>
        public async Task<bool> DeleteItemDialog(string item)
        {
            var parameters = new DialogParameters<CustomDialog>
            {
                { x => x.ContentText, (MarkupString)$"Вы действительно хотите удалить <b style='color:red'>{item}</b>?" },
                { x => x.OkButtonText, "Да" },
                { x => x.Color, MudBlazor.Color.Error }
            };

            var options = new DialogOptions() { CloseButton = true, MaxWidth = MaxWidth.False, ClassBackground = "blackout" };

            DialogReference = _dialogService.Show<CustomDialog>("Удаление", parameters, options);

            var result = await DialogReference.Result;

            if (IsOpened)
            {
                IsOpened = false;

                return false;
            }

            if (!result.Canceled)
                return true;

            return false;
        }

        /// <inheritdoc/>
        public async Task<bool> RefreshPlanOfWeekDialog()
        {
            var parameters = new DialogParameters<CustomDialog>
            {
                { x => x.ContentText,  (MarkupString)"Сегодня не <b style='color:red'>понедельник</b>.<br>Вы действительно хотите рассчитать план на неделю?</br>" },
                { x => x.OkButtonText, "Да" },
                { x => x.NoButtonText, "Нет" },
                { x => x.Color, MudBlazor.Color.Warning }
            };

            var options = new DialogOptions() { CloseButton = true, MaxWidth = MaxWidth.False, ClassBackground = "blackout" };

            DialogReference = _dialogService.Show<CustomDialog>("Внимание", parameters, options);

            var result = await DialogReference.Result;

            if (!result.Canceled)
                return true;

            return false;
        }
    }
}
