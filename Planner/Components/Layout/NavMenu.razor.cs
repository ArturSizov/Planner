using Microsoft.AspNetCore.Components;
using MudBlazor;
using Planner.Abstractions;
using Planner.Components.Dialogs;
using Planner.Components.Pages;
using Planner.Models;
using System.Xml.Linq;

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
        /// Initialized NavMenu
        /// </summary>
        /// <returns></returns>
        protected override async Task OnInitializedAsync()
        {
           if(CompanyManager != null)
              await CompanyManager.ReadAllCompaniesAsync();
        }

        /// <summary>
        /// Create company open dialog window
        /// </summary>
        public async Task CreateCompanyAsync()
        {
            if (_customDialogService == null)
                return;

            var result = await _customDialogService.CreateItemDialog<CreateCompany>("Добавить ЗУЭС", []);

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
            if (_customDialogService == null)
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

            var result = await _customDialogService.CreateItemDialog<CreateCompany>("Редактировать ЗУЭС", parameters);

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
            if (_customDialogService == null)
                return;

            var result = await _customDialogService.CreateItemDialog<CreateCompany>("Добавить РУЭС", []);

            if (result.Item1 && result.Item2 is CompanyModel comp)
                CompanyManager?.Items?.FirstOrDefault(i => i.Name == company.Name)?.Branches.Add(new BranchModel { Name = comp.Name });

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
