using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace Planner.Abstractions
{
    public interface ICustomDialogService
    {
        /// <summary>
        /// Delete item dialog
        /// </summary>
        /// <param name="item">Company end Branch</param>
        /// <returns></returns>
        Task<bool> DeleteItemDialog(string item);

        /// <summary>
        /// Create item dialog window
        /// </summary>
        /// <typeparam name="T">Component</typeparam>
        /// <param name="title">Title string</param>
        /// <param name="parameters">Dialog Parameters</param>
        /// <returns></returns>
        Task<(bool, object)> CreateItemDialog<T>(string title, DialogParameters parameters) where T : ComponentBase;

        /// <summary>
        /// Weekly Plan Update Dialog Box
        /// </summary>
        /// <returns></returns>
        Task<bool> RefreshPlanOfWeekDialog();
    }
}
