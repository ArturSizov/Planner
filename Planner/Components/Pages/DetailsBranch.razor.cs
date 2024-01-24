using Microsoft.AspNetCore.Components;
using MudBlazor;
using Planner.Abstractions;
using Planner.Models;
using static MudBlazor.CategoryTypes;

namespace Planner.Components.Pages
{
    partial class DetailsBranch
    {
        /// <summary>
        /// Branch model
        /// </summary>
        public BranchModel Branch { get; set; } = new();

        /// <summary>
        /// Parameter branch name
        /// </summary>
        [Parameter] public string Name { get; set; } = string.Empty;

        /// <summary>
        /// Dialog service
        /// </summary>
        [Inject] private ICustomDialogService? _customDialogService { get; set; }

        /// <summary>
        /// Company data manager
        /// </summary>
        [Inject] public IDataManager<CompanyModel>? CompanyManager { get; set; }

        /// <summary>
        /// Initialized page
        /// </summary>
        protected override void OnInitialized()
        {
            if (CompanyManager != null)
                foreach (var company in CompanyManager.Items)
                {
                    var branch = company.Branches.Where(x => x.Name == Name).FirstOrDefault();

                    if (branch != null)
                        Branch = branch;
                }
        }

        /// <summary>
        /// Delete branch
        /// </summary>
        /// <param name="branch">CompanyModel</param>
        /// <returns></returns>
        public async Task DeleteBranchAsync(BranchModel branch)
        {
            if (_customDialogService == null)
                return;
            var result = await _customDialogService.DeleteItemDialog(branch.Name);

            if (result && branch != null && CompanyManager != null)
            {
                var company = CompanyManager.Items.FirstOrDefault(x => x.Branches.Any(c => c.Name == branch.Name));

                if (company != null)
                {
                    company.Branches.Remove(branch);
                    await CompanyManager.UpdateAsync(company);
                }
            }
            StateHasChanged();
        }
    }
}
