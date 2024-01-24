using Microsoft.AspNetCore.Components;
using MudBlazor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Planner.Components.Pages
{
    partial class ServiceComponent
    {
        /// <summary>
        /// Service name
        /// </summary>
        [Parameter] public string Name { get; set; } = string.Empty;

        /// <summary>
        /// Service plan
        /// </summary>
        [Parameter] public int Plan { get; set; } = 0;

        /// <summary>
        /// Service fact
        /// </summary>
        [Parameter] public int Fact { get; set; } = 0;

        /// <summary>
        /// Row fact element reference
        /// </summary>
        public ElementReference StringFactRef = new ElementReference();

        /// <summary>
        /// Focus on fact row
        /// </summary>
        /// <returns></returns>
        public async Task FocusRowFact()
        {
            //this js snippet does `document.querySelector(myRef).focus();`
            await StringFactRef.FocusAsync();
        }


    }
}
