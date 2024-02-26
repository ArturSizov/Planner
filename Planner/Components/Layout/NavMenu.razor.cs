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
        /// Returning select branch parameter
        /// </summary>
        [Parameter] public EventCallback<BranchModel> SelectBranch { get; set; }

        /// <summary>
        /// Returning select branch parameter
        /// </summary>
        [Parameter] public EventCallback<CompanyModel> CreateBranch { get; set; }

        [Parameter] public EventCallback CreateCompany { get; set; }

        [Parameter] public EventCallback<CompanyModel> DeleteCompany { get; set; }

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

            var result = await _customDialogService.CreateItemDialog<CreateCompany>("Редактировать компанию", parameters);

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
    }
}
