using Microsoft.AspNetCore.Components;

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
        /// <param name="item"></param>
        /// <returns></returns>
        Task<bool> CreateItemDialog<T>(string item) where T : ComponentBase;
    }
}
