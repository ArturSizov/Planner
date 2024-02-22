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
        [Inject] private IDataManager<CompanyModel>? _companyManager { get; set; }

        /// <summary>
        /// Page navigation
        /// </summary>
        [Inject] private NavigationManager? _navigation { get; set; }

        /// <summary>
        /// Returning select branch parameter
        /// </summary>
        [Parameter] public EventCallback<BranchModel> GetBranch { get; set; }

        /// <summary>
        /// Companies parameter
        /// </summary>
        [Parameter] public ObservableCollection<CompanyModel>? Companies { get; set; }


        /// <summary>
        /// Create company open dialog window
        /// </summary>
        public async Task CreateCompanyAsync()
        {
            if (_customDialogService == null)
                return;

            var result = await _customDialogService.CreateItemDialog<CreateCompany>("Добавить компанию", []);

            var companyName = result.Item2 as string;

            if (result.Item1 && companyName != null)
            {
                _companyManager?.CreateAsync(new CompanyModel
                {
                    Name = companyName
                });

                StateHasChanged();
            }
            else
                return;


            if (_companyManager == null)
                return;

            if (_companyManager.Items.Any(x => x.Branches.Count <= 0))
            {
                var company = _companyManager?.Items.FirstOrDefault(x => x.Name == companyName);

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

            if (result.Item1 && company != null && _companyManager != null)
            {
                if (result.Item2 is string name)
                {
                    var newCompany = new CompanyModel
                    {
                        Id = company.Id,
                        Name = name,
                        Branches = company.Branches
                    };

                    await _companyManager.UpdateAsync(newCompany);
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

            var result = await _customDialogService.CreateItemDialog<CreateCompany>("Добавить филиал", []);

            if (result.Item1 && result.Item2 is string name)
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
            else
                if (_companyManager != null)
                await _companyManager.UpdateAsync(company);
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

            if (result && company != null && _companyManager != null)
            {
                await _companyManager.DeleteAsync(company);

                if(!_companyManager.Items.Any(x => x.Branches.Count != 0) || _companyManager.Items.Count <= 0)
                    _navigation?.NavigateTo("/welcome", true);
                else
                {
                    var branch = _companyManager.Items.SelectMany(x => x.Branches).FirstOrDefault(b => b.Name != string.Empty);

                    if (branch != null)
                        _navigation?.NavigateTo($"/details/{branch.Name}", true);
                }
            }

            StateHasChanged();
        }
    }
}
