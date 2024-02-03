using Microsoft.AspNetCore.Components;
using Planner.Abstractions;
using Planner.Models;

namespace Planner.Components.Pages
{
    /// <summary>
    /// Main page 
    /// </summary>
    partial class MainPage
    {
        /// <summary>
        /// Page navigation
        /// </summary>
        [Inject] private NavigationManager? _navigation { get; set; }

        /// <summary>
        /// Branch model
        /// </summary>
        public BranchModel Branch { get; set; } = new();

        /// <summary>
        /// Company data manager
        /// </summary>
        [Inject] private IDataManager<CompanyModel>? _companyManager { get; set; }

        /// <summary>
        /// Initialized page
        /// </summary>
        protected override void OnInitialized()
        {
            if (_companyManager != null && _companyManager.Items.Count >= 1 && _companyManager.Items != null)
            {
                Branch = _companyManager.Items.SelectMany(x => x.Branches).FirstOrDefault(b => b.Default == true);

                if (Branch == null)
                    Branch = new();
            }
        }              
    }
}
