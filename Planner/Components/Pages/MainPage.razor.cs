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
        /// Branch model
        /// </summary>
        public BranchModel? Branch { get; set; } = new();
        /// <summary>
        /// Company data manager
        /// </summary>
        [Inject] public IDataManager<CompanyModel>? CompanyManager { get; set; }

        /// <summary>
        /// Initialized page
        /// </summary>
        protected override void OnInitialized()
        {
            if (CompanyManager != null && CompanyManager.Items.Count >= 1 && CompanyManager.Items != null)
            {
                Branch = CompanyManager.Items.SelectMany(x => x.Branches).FirstOrDefault(b => b.Default == true);

                if (Branch == null)
                    Branch = new();
            }
        }              
    }
}
