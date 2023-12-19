using Microsoft.AspNetCore.Components;
using Planner.Abstractions;
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
        public async Task CreateCompany()
        {
            if (CustomDialogService == null)
                return;

            var result = await CustomDialogService.CreateItemDialog("Добавить ЗУЭС");

            if (result)
                if (CompanyManager != null)
                    await CompanyManager.CreateAsync(new CompanyModel
                    {
                        Name = "ws"
                    });

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
        public async Task DeleteCompany(CompanyModel company)
        {
            if (CustomDialogService == null)
                return;
            var result = await CustomDialogService.DeleteItemDialog(company.Name);

            if (result)
                if (CompanyManager != null)
                    await CompanyManager.DeleteAsync(company);

            StateHasChanged();
        }
    }
}
