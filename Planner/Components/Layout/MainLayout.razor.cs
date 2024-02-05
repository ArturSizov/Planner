using Microsoft.AspNetCore.Components;
using MudBlazor;
using Planner.Abstractions;
using Planner.Auxiliary;
using Planner.Models;
using Color = MudBlazor.Color;


namespace Planner.Components.Layout
{
    partial class MainLayout
    {
        /// <summary>
        /// Company data manager
        /// </summary>
        [Inject] private IDataManager<CompanyModel>? _companyManager { get; set; }

        /// <summary>
        /// Snackbar
        /// </summary>
        [Inject] ISnackbar? _snackbar { get; set; }

        /// <summary>
        /// Company name
        /// </summary>
        [Parameter] public string? Name { get; set; }

        /// <summary>
        /// Drawer open
        /// </summary>
        public bool DrawerOpen = true;

        /// <summary>
        /// Company model
        /// </summary>
        private CompanyModel? _company;

        /// <summary>
        /// Branch model
        /// </summary>
        private BranchModel? _branch;

        
        /// <summary>
        /// Color branch star
        /// </summary>
        public Color ColorBranch { get; set; } = Color.Default;

        /// <summary>
        /// Enabled/disabled status star
        /// </summary>
        public bool StatusStar { get; set; } = false;

        /// <summary>
        /// Open/close menu
        /// </summary>
        public void DrawerToggle()
        {
            DrawerOpen = !DrawerOpen;
        }

        /// <summary>
        /// Sets the default branch
        /// </summary>
        /// <returns></returns>
        public async Task SetDefaultBranch()
        {
            if (_companyManager != null)
            {
                ColorBranch = Color.Warning;

                foreach (var company in _companyManager.Items)
                {
                    foreach (var branch in company.Branches)
                    {
                        branch.Name = branch.Name;
                        branch.Services = branch.Services;
                        branch.Default = false;

                        if (branch.Name == Name)
                        {
                            branch.Name = branch.Name;
                            branch.Services = branch.Services;
                            branch.Default = true;
                        }

                        await _companyManager.UpdateAsync(company);

                        if (_snackbar == null)
                            return;
                        _snackbar.Configuration.PositionClass = Defaults.Classes.Position.BottomCenter;
                        _snackbar.Add($"{Name} выбран по умолчанию", Severity.Info);
                    }
                }
            }
        }

        /// <summary>
        /// Parameter set main layout
        /// </summary>
        protected override void OnParametersSet()
        {
            if (_companyManager != null && Name == null)
            {
                _company = _companyManager.Items.FirstOrDefault(x => x.Branches.Any(c => c.Default == true));

                _branch = _company?.Branches.FirstOrDefault(x => x.Default == true);

                if (_branch != null)
                {
                    Name = _branch.Name;

                    ColorBranch = Color.Warning;
                }
                else
                {
                    Name = "Planner";
                    StatusStar = true;
                }
            }
        }

        /// <summary>
        ///  On initialized Main Layout
        /// </summary>
        /// <returns></returns>
        protected override async Task OnInitializedAsync()
        {
            if (_companyManager != null)
            {
                await _companyManager.ReadAllCompaniesAsync();

                //Intercepts the branch name
                if (Body != null)
                {
                    if ((Body.Target as RouteView)?.RouteData.RouteValues?.TryGetValue("name", out object? obj) == true)
                    {
                        if (obj != null)
                        {
                            var res = obj as string;

                            if (res != null)
                            {
                                Name = res;

                                _company = _companyManager.Items.FirstOrDefault(x => x.Branches.Any(c => c.Name == Name));

                                _branch = _company?.Branches.FirstOrDefault(x => x.Name == Name);

                               if(_branch != null)
                                    if (_branch.Default)
                                        ColorBranch = Color.Warning;
                            }
                        }
                    }
                }

            }               
        }
    }
}
