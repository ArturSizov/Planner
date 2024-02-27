using Microsoft.AspNetCore.Components;
using Planner.Abstractions;
using Planner.Models;

namespace Planner.Components.Layout
{
    partial class NavMenu
    {
        /// <summary>
        /// Company data manager
        /// </summary>
        [Inject] public IDataManager<CompanyModel>? CompanyManager { get; set; }

        /// <summary>
        /// Returning select branch parameter
        /// </summary>
        [Parameter] public EventCallback<BranchModel> SelectBranch { get; set; }

        /// <summary>
        /// Add branch option
        /// </summary>
        [Parameter] public EventCallback<CompanyModel> CreateBranch { get; set; }

        /// <summary>
        /// Create company option
        /// </summary>
        [Parameter] public EventCallback CreateCompany { get; set; }

        /// <summary>
        /// Delete company option
        /// </summary>
        [Parameter] public EventCallback<CompanyModel> DeleteCompany { get; set; }

        /// <summary>
        /// Edit company option
        /// </summary>
        [Parameter] public EventCallback<CompanyModel> EditCompany { get; set; }
    }
}
