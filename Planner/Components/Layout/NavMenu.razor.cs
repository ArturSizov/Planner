using Microsoft.AspNetCore.Components;
using MudBlazor;
using Planner.Abstractions;
using Planner.Components.Dialogs;
using Planner.Models;
using System.Collections.ObjectModel;

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
        /// Create company open dialog window
        /// </summary>
        public async Task CreateCompanyAsync()
        {
            if (CustomDialogService == null)
                return;

            var result = await CustomDialogService.CreateItemDialog<CreateCompany>("Добавить ЗУЭС", []);

            var name = result.Item2 as string;

            if (result.Item1 && name != null)
                CompanyManager?.CreateAsync(new CompanyModel { Name = name} );

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

            var parameters = new DialogParameters<CreateCompany>
            {
                { x => x.ItemName, company.Name }
            };

            var result = await CustomDialogService.CreateItemDialog<CreateCompany>("Редактировать ЗУЭС", parameters);

            if (result.Item1 && company != null && CompanyManager != null)
                CompanyManager?.UpdateAsync(company);

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

            var name = result.Item2 as string;

            if (result.Item1 && name != null)
                CompanyManager?.Items?.FirstOrDefault(i => i.Id == company.Id)?.Branches.Add(new BranchModel { Name = name });

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

            if (result && company != null)
                if (CompanyManager != null)
                    await CompanyManager.DeleteAsync(company);

            StateHasChanged();
        }
    }
}
