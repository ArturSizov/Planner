using Microsoft.AspNetCore.Components;

namespace Planner.Components.Pages
{
    public partial class WelcomeComponent
    {
        /// <summary>
        /// Create company option
        /// </summary>
        [Parameter] public EventCallback CreateCompany { get; set; }
    }
}
