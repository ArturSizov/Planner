using Microsoft.AspNetCore.Components;
using MudBlazor;
using Planner.Abstractions;
using Planner.Components.Dialogs;
using Planner.Models;

namespace Planner.Components.Layout
{
    partial class NavMenu
    {
        /// <summary>
        /// Dialog service
        /// </summary>
        [Inject] private ICustomDialogService? _customDialogService { get; set; }

        /// <summary>
        /// Company data manager
        /// </summary>
        [Inject] public IDataManager<CompanyModel>? CompanyManager { get; set; }

        /// <summary>
        /// Page navigation
        /// </summary>
        [Inject] private NavigationManager? _navigation { get; set; }

 
        /// <summary>
        /// Create company open dialog window
        /// </summary>
        public async Task CreateCompanyAsync()
        {
            if (_customDialogService == null)
                return;

            var result = await _customDialogService.CreateItemDialog<CreateCompany>("Добавить ЗУЭС", []);

            var companyName = result.Item2 as string;

            if (result.Item1 && companyName != null)
                CompanyManager?.CreateAsync(new CompanyModel
                {
                    Name = companyName
                });

            StateHasChanged();
        }

        /// <summary>
        /// Edit company
        /// </summary>
        /// <param name="company"></param>
        /// <returns></returns>
        public async Task EditCompanyAsync(CompanyModel company)
        {
            if (_customDialogService == null)
                return;

            var parameters = new DialogParameters<CreateCompany>
            {
                { x => x.CompanyName,  company.Name}
            };

            var result = await _customDialogService.CreateItemDialog<CreateCompany>("Редактировать ЗУЭС", parameters);

            if (result.Item1 && company != null && CompanyManager != null)
            {
                if (result.Item2 is string name)
                {
                    var newCompany = new CompanyModel
                    {
                        Id = company.Id,
                        Name = name,
                        Branches = company.Branches
                    };

                    await CompanyManager.UpdateAsync(newCompany);
                }
            }
            else 
                return;

                StateHasChanged();
        }

        /// <summary>
        /// Create branch open dialog window
        /// </summary>
        /// <param name="company"></param>
        /// <returns></returns>
        public async Task CreateBranchAsync(CompanyModel company)
        {
            if (_customDialogService == null)
                return;

            var result = await _customDialogService.CreateItemDialog<CreateCompany>("Добавить РУЭС", []);

            if (result.Item1 && result.Item2 is string name)
                CompanyManager?.Items?.FirstOrDefault(x => x.Name == company.Name)?.Branches.Add(new BranchModel { Name = name });

            if(CompanyManager != null)
                await CompanyManager.UpdateAsync(company);

            StateHasChanged();
        }

        /// <summary>
        /// Delete company
        /// </summary>
        /// <param name="company">CompanyModel</param>
        /// <returns></returns>
        public async Task DeleteCompanyAsync(CompanyModel company)
        {
            if (_customDialogService == null)
                return;
            var result = await _customDialogService.DeleteItemDialog(company.Name);

            if (result && company != null && CompanyManager != null)
                await CompanyManager.DeleteAsync(company);

            StateHasChanged();
        }

        /// <summary>
        /// Opening detail branch page
        /// </summary>
        /// <param name="name"></param>
        public void OpenDetailsBranch(string name)
        {
            _navigation?.NavigateTo($"details/{name}", true);

           StateHasChanged();
        }
    }
}
