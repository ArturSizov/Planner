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
        [Inject] public ICustomDialogService? CustomDialogService { get; set; }

        /// <summary>
        /// Company data manager
        /// </summary>
        [Inject] public IDataManager<CompanyModel>? CompanyManager { get; set; }

        /// <summary>
        /// Company data manager
        /// </summary>
        //[Inject] public IDataManager<BranchModel>? BranchManager { get; set; }

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

        public async Task EditCompanyAsync(CompanyModel company)
        {
            if (CustomDialogService == null)
                return;
            var parameters = new DialogParameters<CreateCompany>
            {
                {x => x.Company, company }
            };

            var result = await CustomDialogService.CreateItemDialog<CreateCompany>("Редактировать ЗУЭС", parameters);

            if (result.Item1 && company != null)
                CompanyManager?.UpdateAsync(company);

            StateHasChanged();
        }

        /// <summary>
        /// Create branch open dialog window
        /// </summary>
        //public async Task CreateBranch()
        //{
        //    if (CustomDialogService == null)
        //        return;

        //    var result = await CustomDialogService.CreateItemDialog("Добавить РУЭС");

        //    if (result)
        //        if (BranchManager != null)
        //            await BranchManager.CreateAsync(new BranchModel
        //            {
        //                Name = "wse"
        //            });

        //    StateHasChanged();
        //}

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
