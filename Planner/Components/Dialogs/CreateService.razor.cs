using Microsoft.AspNetCore.Components;
using MudBlazor;
using Planner.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Planner.Components.Dialogs
{
    public partial class CreateService
    {
        /// <summary>
        /// Mud Dialog Instance
        /// </summary>
        [CascadingParameter] MudDialogInstance? MudDialog { get; set; }

        /// <summary>
        /// Item name parameter
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
            {
                Success = true;
                yield return "Не может быть пустым";
            }
            if (name.Length < 2)
            {
                Success = true;
                yield return "Не менее 2-ух символов";
            }
            else
                Success = false;
        }

    }
}
