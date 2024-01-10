using Microsoft.AspNetCore.Components;
using MudBlazor;
using Planner.Abstractions;
using Planner.Auxiliary;
using Planner.Components.Dialogs;
using Planner.DataAccessLayer.DAO;
using Planner.Models;

namespace Planner.Components.Layout
{
    partial class NavMenu
    {
        /// <summary>
        /// Dialog service
        /// </summary>
        [Inject] public ICustomDialogService? CustomDialogService { get; set; }

        /// <summary>
        /// Company data manager
        /// </summary>
        [Inject] public IDataManager<CompanyModel>? CompanyManager { get; set; }

        /// <summary>
        /// Page navigation
        /// </summary>
        [Inject] private NavigationManager? _navigation { get; set; }

        [Parameter] public string? BranchName { get; set; } = "01";

        /// <summary>
        /// Create company open dialog window
        /// </summary>
        public async Task CreateCompanyAsync()
        {
            if (CustomDialogService == null)
                return;

            var result = await CustomDialogService.CreateItemDialog<CreateCompany>("Добавить ЗУЭС", []);

            var company = result.Item2 as CompanyModel;

            if (result.Item1 && company != null)
                CompanyManager?.CreateAsync(company);

            StateHasChanged();
        }

        /// <summary>
        /// Edit company
        /// </summary>
        /// <param name="company"></param>
        /// <returns></returns>
        public async Task EditCompanyAsync(CompanyModel company)
        {
            if (CustomDialogService == null)
                return;

            var item = new CompanyModel
            {
                Name = company.Name,
                Id = company.Id,
                Branches = company.Branches
            };

            var parameters = new DialogParameters<CreateCompany>
            {
                { x => x.Company, item }
            };

            var result = await CustomDialogService.CreateItemDialog<CreateCompany>("Редактировать ЗУЭС", parameters);

            if (result.Item1 && company != null && CompanyManager != null)
            {
                if (item != null)
                {
                    if (result.Item2 is CompanyModel comp)
                    {
                        item.Id = comp.Id;
                        item.Name = comp.Name;
                        item.Branches = comp.Branches;
                    }

                    await CompanyManager.UpdateAsync(item);
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
            if (CustomDialogService == null || CompanyManager == null)
                return;

            var result = await CustomDialogService.CreateItemDialog<CreateCompany>("Добавить РУЭС", []);

            if (result.Item1 && result.Item2 is CompanyModel comp)
                CompanyManager?.Items?.FirstOrDefault(i => i.Id == company.Id)?.Branches.Add(new BranchModel { Name = comp.Name });

            StateHasChanged();
        }

        /// <summary>
        /// Delete company
        /// </summary>
        /// <param name="company">CompanyModel</param>
        /// <returns></returns>
        public async Task DeleteCompanyAsync(CompanyModel company)
        {
            if (CustomDialogService == null)
                return;
            var result = await CustomDialogService.DeleteItemDialog(company.Name);

            if (result && company != null && CompanyManager != null)
                await CompanyManager.DeleteAsync(company);

            StateHasChanged();
        }

        /// <summary>
        /// Opening detail branch page
        /// </summary>
        /// <param name="id"></param>
        public void OpenDetailsBranch(string name)
        {
            _navigation?.NavigateTo($"details/{name}", true);
            StateHasChanged();
        }
    }
}
