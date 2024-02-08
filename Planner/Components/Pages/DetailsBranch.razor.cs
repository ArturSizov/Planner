using Microsoft.AspNetCore.Components;
using MudBlazor;
using Planner.Abstractions;
using Planner.Components.Dialogs;
using Planner.Models;
using SwipeDirection = MudBlazor.SwipeDirection;

namespace Planner.Components.Pages
{
    partial class DetailsBranch
    {
        /// <summary>
        /// Branch model
        /// </summary>
        public BranchModel Branch { get; set; } = new();

        /// <summary>
        /// Tab panel
        /// </summary>
        public MudTabs? Tabs { get; set; }

        /// <summary>
        /// Parameter branch name
        /// </summary>
        [Parameter] public string Name { get; set; } = string.Empty;

        /// <summary>
        /// Dialog service
        /// </summary>
        [Inject] private ICustomDialogService? _customDialogService { get; set; }

        /// <summary>
        /// Page navigation
        /// </summary>
        [Inject] private NavigationManager? _navigation { get; set; }

        /// <summary>
        /// Company data manager
        /// </summary>
        [Inject] public IDataManager<CompanyModel>? CompanyManager { get; set; }

        /// <summary>
        /// Initialized page
        /// </summary>
        protected override void OnInitialized()
        {
            if (CompanyManager != null)
                foreach (var company in CompanyManager.Items)
                {
                    var branch = company.Branches.Where(x => x.Name == Name).FirstOrDefault();

                    if (branch != null)
                        Branch = branch;
                }
        }

        /// <summary>
        /// Delete branch
        /// </summary>
        /// <returns></returns>
        public async Task DeleteBranchAsync()
        {
            if (_customDialogService == null)
                return;
            var result = await _customDialogService.DeleteItemDialog(Branch.Name);

            if (result && Branch != null && CompanyManager != null)
            {
                var company = CompanyManager.Items.FirstOrDefault(x => x.Branches.Any(c => c.Name == Branch.Name));

                if (company != null)
                {
                    company.Branches.Remove(Branch);

                    if(Branch.Default)
                    {
                        foreach (var branch in company.Branches)
                        {
                            branch.Default = true;
                            break;
                        }
                    }

                    await CompanyManager.UpdateAsync(company);
                    _navigation?.NavigateTo("/", true);
                }
            }
            StateHasChanged();
        }

        /// <summary>
        /// Create service open dialog window
        /// </summary>
        /// <returns></returns>
        public async Task CreateServiceAsync()
        {
            if (_customDialogService == null)
                return;

            var result = await _customDialogService.CreateItemDialog<CreateService>("Добавить услугу", []);

            if (result.Item1 && result.Item2 is ServiceModel service && CompanyManager != null)
            {
                var company = CompanyManager.Items.FirstOrDefault(x => x.Branches.Any(b => b.Name == Branch.Name));

                if (company != null)
                {
                    Branch.Services.Add(service);
                    await CompanyManager.UpdateAsync(company);
                }               
            }

            StateHasChanged();
        }

        /// <summary>
        /// Edit branch
        /// </summary>
        /// <returns></returns>
        public async Task EditBranchAsync()
        {
            if (_customDialogService == null)
                return;

            var parameters = new DialogParameters<CreateCompany>
            {
                { x => x.CompanyName,  Branch.Name}
            };

            var result = await _customDialogService.CreateItemDialog<CreateCompany>("Редактировать РУЭС", parameters);

            if (result.Item1 && Branch != null && CompanyManager != null)
            {
                var company = CompanyManager.Items.FirstOrDefault(x => x.Branches.Any(c => c.Name == Branch.Name));

                if (result.Item2 is string name && company != null)
                {
                    var branch = company.Branches.FirstOrDefault(x => x.Name == Branch.Name);

                    if(branch != null)
                    {
                        branch.Name = name;
                        branch.Services = Branch.Services;
                        branch.Default = Branch.Default;

                        await CompanyManager.UpdateAsync(company);
                        _navigation?.NavigateTo($"details/{name}", true);
                    }
                }
            }
            else
                return;

            StateHasChanged();
        }

        /// <summary>
        /// Edit service
        /// </summary>
        /// <param name="service"></param>
        /// <returns></returns>
        public async Task EditServiceAsync(ServiceModel service)
        {
            if (_customDialogService == null)
                return;

            var editService = new ServiceModel
            {
                Plan = service.Plan,
                Fact = service.Fact,
                Name = service.Name
            };

            if (CompanyManager != null)
            {
                var company = CompanyManager.Items.FirstOrDefault(x => x.Branches.Any(b => b.Name == Branch.Name));

                var parameters = new DialogParameters<CreateService>
                {
                    { x => x.Service,  editService}
                };

                var result = await _customDialogService.CreateItemDialog<CreateService>("Редактировать услугу", parameters);

                if (result.Item1 && service != null && CompanyManager != null)
                {
                    if (result.Item2 is ServiceModel newService && company != null)
                    {
                        if (newService != null)
                        {
                            service.Name = newService.Name;
                            service.Plan = newService.Plan;
                            service.Fact = newService.Fact;
                        }

                        await CompanyManager.UpdateAsync(company);
                    }
                }
                else
                    return;

            }

            StateHasChanged();
        }

        /// <summary>
        /// Delete service
        /// </summary>
        /// <param name="service"></param>
        /// <returns></returns>
        public async Task DeleteServiceAsync(ServiceModel service)
        {
            if (_customDialogService == null)
                return;
            var result = await _customDialogService.DeleteItemDialog(service.Name);

            if (result && CompanyManager != null)
            {
                var company = CompanyManager.Items.FirstOrDefault(x => x.Branches.Any(b => b.Name == Branch.Name));

                if (company != null)
                {
                    Branch.Services.Remove(service);
                    await CompanyManager.UpdateAsync(company);
                }

                StateHasChanged();
            }
        }

        /// <summary>
        /// Screen swipe event
        /// </summary>
        /// <param name="args"></param>
        public void HandleSwipeEnd(SwipeEventArgs args)
        {
            if (args.SwipeDirection == SwipeDirection.RightToLeft)
            {
                Tabs?.ActivatePanel("pn_two");
                return;
            }
               
            if (args.SwipeDirection == SwipeDirection.LeftToRight)
                Tabs?.ActivatePanel("pn_one");

        }
    }
}
