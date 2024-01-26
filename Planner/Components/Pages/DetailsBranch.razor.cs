using Microsoft.AspNetCore.Components;
using MudBlazor;
using Planner.Abstractions;
using Planner.Components.Dialogs;
using Planner.Models;
using static MudBlazor.Icons.Custom;

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
        /// <returns></returns>
        public async Task DeleteBranchAsync()
        {
            if (_customDialogService == null)
                return;
            var result = await _customDialogService.DeleteItemDialog(Branch.Name);

            if (result && Branch != null && CompanyManager != null)
            {
                var company = CompanyManager.Items.FirstOrDefault(x => x.Branches.Any(c => c.Name == Branch.Name));

                if (company != null)
                {
                    company.Branches.Remove(Branch);
                    await CompanyManager.UpdateAsync(company);
                }
            }
            StateHasChanged();
        }

        /// <summary>
        /// Create service open dialog window
        /// </summary>
        /// <returns></returns>
        public async Task CreateServiceAsync()
        {
            if (_customDialogService == null)
                return;

            var result = await _customDialogService.CreateItemDialog<CreateService>("Добавить услугу", []);

            if (result.Item1 && result.Item2 is ServiceModel service && CompanyManager != null)
            {
                var company = CompanyManager.Items.FirstOrDefault(x => x.Branches.Any(c => c.Name == Branch.Name));

                if (company != null)
                {
                    Branch.Services.Add(service);
                    await CompanyManager.UpdateAsync(company);
                }
                
            }

            StateHasChanged();
        }

        /// <summary>
        /// Edit branch
        /// </summary>
        /// <returns></returns>
        public async Task EditBranchAsync()
        {
            if (_customDialogService == null)
                return;

            var parameters = new DialogParameters<CreateCompany>
            {
                { "branchName", Branch.Name }
            };

            var result = await _customDialogService.CreateItemDialog<CreateCompany>("Редактировать РУЭС", parameters);

            var d = result.Item2;

            if (result.Item1 && Branch != null && CompanyManager != null)
            {
                var company = CompanyManager.Items.FirstOrDefault(x => x.Branches.Any(c => c.Name == Branch.Name));

                if (company != null)
                    await CompanyManager.UpdateAsync(company);

            }
            else
                return;

            StateHasChanged();
        }
    }
}
