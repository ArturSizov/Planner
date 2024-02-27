using Microsoft.AspNetCore.Components;
using MudBlazor;
using Planner.Abstractions;
using Planner.Components.Dialogs;
using Planner.Models;
using System.Collections.ObjectModel;
using SwipeDirection = MudBlazor.SwipeDirection;

namespace Planner.Components.Pages
{
    partial class DetailsBranch
    {
        /// <summary>
        /// Branch parameter
        /// </summary>
        [Parameter] public BranchModel? Branch { get; set; }

        /// <summary>
        /// Companies parameter
        /// </summary>
        [Parameter] public ObservableCollection<CompanyModel>? Companies { get; set; }

        /// <summary>
        /// Delete branch parameter
        /// </summary>
        [Parameter] public EventCallback DeleteBranch { get; set; }

        /// <summary>
        /// Edit branch parameter
        /// </summary>
        [Parameter] public EventCallback EditBranch { get; set; }

        /// <summary>
        /// Tab panel
        /// </summary>
        public MudTabs? Tabs { get; set; }

        /// <summary>
        /// Dialog service
        /// </summary>
        [Inject] private ICustomDialogService? _customDialogService { get; set; }

        /// <summary>
        /// Company data manager
        /// </summary>
        [Inject] public IDataManager<CompanyModel>? _companyManager { get; set; }

        /// <summary>
        /// Create service open dialog window
        /// </summary>
        /// <returns></returns>
        public async Task CreateServiceAsync()
        {
            if (_customDialogService == null)
                return;

            var result = await _customDialogService.CreateItemDialog<CreateService>("Добавить услугу", []);

            if (result.Item1 && result.Item2 is ServiceModel service && _companyManager != null)
            {
                var company = _companyManager.Items.FirstOrDefault(x => x.Branches.Any(b => b.Name == Branch?.Name));

                if (company != null)
                {
                    Branch?.Services.Add(service);

                    Branch?.WeekPlans.Add(new WeekServiceModel { Service = new ServiceModel { Name = service.Name, Plan = 0, Fact = 0} });

                    await _companyManager.UpdateAsync(company);
                }               
            }

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

            if (_companyManager != null)
            {
                var company = _companyManager.Items.FirstOrDefault(x => x.Branches.Any(b => b.Name == Branch?.Name));

                var parameters = new DialogParameters<CreateService>
                {
                    { x => x.Service,  editService}
                };

                var result = await _customDialogService.CreateItemDialog<CreateService>("Редактировать услугу", parameters);

                if (result.Item1 && service != null && _companyManager != null)
                {
                    if (result.Item2 is ServiceModel newService && company != null)
                    {
                        if (newService != null)
                        {
                            service.Name = newService.Name;
                            service.Plan = newService.Plan;
                            service.Fact = newService.Fact;
                        }

                        await _companyManager.UpdateAsync(company);
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

            if (result && _companyManager != null)
            {
                var company = _companyManager.Items.FirstOrDefault(x => x.Branches.Any(b => b.Name == Branch?.Name));

                var weekService = Branch?.WeekPlans.FirstOrDefault(x => x.Service.Name == service.Name);

                if (company != null && weekService != null)
                {
                    Branch?.Services.Remove(service);
                    Branch?.WeekPlans.Remove(weekService);
                    await _companyManager.UpdateAsync(company);
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
