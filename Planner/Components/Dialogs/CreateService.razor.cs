using Microsoft.AspNetCore.Components;
using MudBlazor;
using Planner.Models;

namespace Planner.Components.Dialogs
{
    public partial class CreateService
    {
        /// <summary>
        /// Mud Dialog Instance
        /// </summary>
        [CascadingParameter] MudDialogInstance? MudDialog { get; set; }

        /// <summary>
        /// Item parameter
        /// </summary>
        [Parameter] public ServiceModel Service { get; set; } = new();

        /// <summary>
        /// Validation of the OK button
        /// </summary>
        public bool Success { get; set; } = true;

        /// <summary>
        /// Ok method
        /// </summary>
        public void Submit() => MudDialog?.Close(Service);


        /// <summary>
        /// Close dialog window
        /// </summary>
        public void Cancel() => MudDialog?.Cancel();

        /// <summary>
        /// Name validations
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public IEnumerable<string> NameStrength(string name)
        {
            if (name.Length <= 0)
                yield return "Не может быть пустым";

            if (name.Length < 2)
                yield return "Не менее 2-ух символов";

            SuccessSet();
        }

        /// <summary>
        /// Number validation
        /// </summary>
        /// <param name="number"></param>
        /// <returns></returns>
        public IEnumerable<string> NumberStrength(ushort? number)
        {
            if (number == null)
                yield return "Не может быть пустым";

            SuccessSet();
        }

        /// <summary>
        /// Disabled/enabled button
        /// </summary>
        private void SuccessSet()
        {
            if (string.IsNullOrEmpty(Service.Name) || Service.Plan == null || Service.Fact == null || Service.Name.Length < 2)
                Success = true;
            else Success = false;
        }
    }
}
