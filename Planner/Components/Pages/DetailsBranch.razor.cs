using Microsoft.AspNetCore.Components;
using Planner.Abstractions;
using Planner.Models;

namespace Planner.Components.Pages
{
    partial class DetailsBranch
    {
        public BranchModel Branch { get; set; } = new();

        [Parameter] public string Name { get; set; } = string.Empty;

        /// <summary>
        /// Dialog service
        /// </summary>
        [Inject] public ICustomDialogService? CustomDialogService { get; set; }

        /// <summary>
        /// Company data manager
        /// </summary>
        [Inject] public IDataManager<CompanyModel>? CompanyManager { get; set; }

        protected override void OnInitialized()
        {
            if(CompanyManager != null)
               foreach (var company in CompanyManager.Items)
               {
                    var branch = company.Branches.Where(x => x.Name == Name).FirstOrDefault();

                    if (branch != null)
                        Branch = branch;
                }
        }
    }
}
