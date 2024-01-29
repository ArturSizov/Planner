using Microsoft.AspNetCore.Components;
using Planner.Models;

namespace Planner.Components.Pages
{
    public partial class ServiceComponent
    {
        /// <summary>
        /// Service name
        /// </summary>
        [Parameter] public ServiceModel Service { get; set; } = new();


        /// <summary>
        /// Row fact element reference
        /// </summary>
        public ElementReference StringFactRef = new();

        /// <summary>
        /// Focus on fact row
        /// </summary>
        /// <returns></returns>
        public async Task FocusRowFactAsync()
        {
            //this js snippet does `document.querySelector(myRef).focus();`
            await StringFactRef.FocusAsync();
        }

    }
}
