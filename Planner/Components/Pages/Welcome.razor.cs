using Microsoft.AspNetCore.Components;
using Planner.Abstractions;
using Planner.Components.Dialogs;
using Planner.Models;

namespace Planner.Components.Pages
{
    public partial class Welcome
    {
        /// <summary>
        /// Company data manager
        /// </summary>
        [Inject] private IDataManager<CompanyModel>? _companyManager { get; set; }

        /// <summary>
        /// Dialog service
        /// </summary>
        [Inject] private ICustomDialogService? _customDialogService { get; set; }

        /// <summary>
        /// Page navigation
        /// </summary>
        [Inject] private NavigationManager? _navigation { get; set; }

        
        /// <summary>
        /// Company name
        /// </summary>
        private string? _companyName;

        /// <summary>
        /// Text button
        /// </summary>
        public string AddButtonText { get; set; } = "Добавить компанию";


        /// <summary>
        /// Create company open dialog window
        /// </summary>
        public async Task CreateCompanyAsync()
        {
            if (_customDialogService == null)
                return;

            var result = await _customDialogService.CreateItemDialog<CreateCompany>("Добавить компанию", []);

            _companyName = result.Item2 as string;

            if (result.Item1 && _companyName != null)
            {
                _companyManager?.CreateAsync(new CompanyModel
                {
                    Name = _companyName
                });

                StateHasChanged();

            }
            else
                return;


            if (_companyManager == null)
                return;

            if (_companyManager.Items.Any(x => x.Branches.Count <= 0))
            {
               var company = _companyManager?.Items.FirstOrDefault(x => x.Name == _companyName);

                var resultBranch = await _customDialogService.CreateItemDialog<CreateCompany>("Добавить филиал", []);

                if (resultBranch.Item1 && resultBranch.Item2 is string name && company != null)
                {
                    _companyManager?.Items?.FirstOrDefault(x => x.Name == company.Name)?.
                        Branches.Add(
                        new BranchModel
                        {
                            Name = name,
                            Default = !_companyManager.Items.Any(x => x.Branches.Any(b => b.Default == true))
                        });

                    if (_companyManager != null)
                        await _companyManager.UpdateAsync(company);

                    _navigation?.NavigateTo($"details/{name}", true);

                    StateHasChanged();
                }
            }
        }
    }
}
