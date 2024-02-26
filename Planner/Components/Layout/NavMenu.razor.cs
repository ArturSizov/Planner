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
        /// Returning select branch parameter
        /// </summary>
        [Parameter] public EventCallback<BranchModel> SelectBranch { get; set; }

        /// <summary>
        /// Returning select branch parameter
        /// </summary>
        [Parameter] public EventCallback<CompanyModel> CreateBranch { get; set; }

        /// <summary>
        /// Create company parameter
        /// </summary>
        [Parameter] public EventCallback CreateCompany { get; set; }

        /// <summary>
        /// Delete company parameter
        /// </summary>
        [Parameter] public EventCallback<CompanyModel> DeleteCompany { get; set; }

        /// <summary>
        /// Edit company parameter
        /// </summary>
        [Parameter] public EventCallback<CompanyModel> EditCompany { get; set; }
    }
}
