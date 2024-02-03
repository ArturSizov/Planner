using Microsoft.AspNetCore.Components;
using Planner.Abstractions;
using Planner.Models;
using Color = MudBlazor.Color;


namespace Planner.Components.Layout
{
    partial class MainLayout
    {
        /// <summary>
        /// Company data manager
        /// </summary>
        [Inject] public IDataManager<CompanyModel>? _companyManager { get; set; }

        /// <summary>
        /// Company name
        /// </summary>
        [Parameter] public string? Name { get; set; }

        /// <summary>
        /// Drawer open
        /// </summary>
        bool _drawerOpen = true;

        /// <summary>
        /// Company model
        /// </summary>
        private CompanyModel? _company = new();

        /// <summary>
        /// Branch model
        /// </summary>
        private BranchModel? _branch = new();

        /// <summary>
        /// Color branch star
        /// </summary>
        public Color ColorBranch { get; set; } = Color.Default;

        /// <summary>
        /// Open/close menu
        /// </summary>
        public void DrawerToggle()
        {
            _drawerOpen = !_drawerOpen;
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

                        if(branch.Name == Name)
                        {
                            branch.Name = branch.Name;
                            branch.Services = branch.Services;
                            branch.Default = true;
                        }

                        await _companyManager.UpdateAsync(company);
                    }
                }
            }
        }

        /// <summary>
        /// Parameter set main layout
        /// </summary>
        protected override void OnParametersSet()
        {
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
                            return;
                        }                      
                    }
                }
            }

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
                    Name = "Planner";
            }            
        }

        /// <summary>
        ///  On initialized Main Layout
        /// </summary>
        /// <returns></returns>
        protected override async Task OnInitializedAsync()
        {
            if(_companyManager != null)
               await _companyManager.ReadAllCompaniesAsync();
        }
    }
}
