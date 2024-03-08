using Microsoft.AspNetCore.Components;
using MudBlazor;
using Planner.Converters;

namespace Planner.Components.Dialogs
{
    public partial class NumericDialog
    {
        /// <summary>
        /// Mud Dialog Instance
        /// </summary>
        [CascadingParameter] MudDialogInstance? MudDialog { get; set; }

        /// <summary>
        /// Item parameter
        /// </summary>
        [Parameter] public double? Fact { get; set; }

        /// <summary>
        /// Overridden default converter
        /// </summary>
        private CustomConverter<double?> _converter = new();

        /// <summary>
        /// Validation of the OK button
        /// </summary>
        public bool Success { get; set; } = true;

        /// <summary>
        /// Ok method
        /// </summary>
        public void Submit() => MudDialog?.Close(Fact);

        /// <summary>
        /// Close dialog window
        /// </summary>
        public void Cancel() => MudDialog?.Cancel();

        /// <summary>
        /// Number validation
        /// </summary>
        /// <param name="number"></param>
        /// <returns></returns>
        public IEnumerable<string> NumberStrength(double? number)
        {
            if (number < 0 || number!.ToString()!.Contains("-0"))
                yield return "Не может быть отрицательным";

            if (number == null)
                yield return "Не может быть пустым";

            SuccessSet();
        }

        /// <summary>
        /// Disabled/enabled button
        /// </summary>
        private void SuccessSet()
        {
            if (Fact == null || Fact < 0 || Fact!.ToString()!.Contains("-0"))
            {
                Success = true;
                return;
            }
            Success = false;
        }
    }
}
