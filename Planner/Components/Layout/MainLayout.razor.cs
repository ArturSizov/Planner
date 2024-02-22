using Microsoft.AspNetCore.Components;
using Microsoft.Maui;
using MudBlazor;
using Planner.Abstractions;
using Planner.Auxiliary;
using Planner.Models;
using Color = MudBlazor.Color;
using SwipeDirection = MudBlazor.SwipeDirection;


namespace Planner.Components.Layout
{
    partial class MainLayout
    {
        /// <summary>
        /// Company data manager
        /// </summary>
        [Inject] public IDataManager<CompanyModel>? CompanyManager { get; set; }

        /// <summary>
        /// Snackbar
        /// </summary>
        [Inject] ISnackbar? _snackbar { get; set; }

        /// <summary>
        /// Company name
        /// </summary>
        [Parameter] public string? Name { get; set; }

        /// <summary>
        /// Swipe direction
        /// </summary>
        public SwipeDirection SwipeDirection { get; set; }

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
        public BranchModel? Branch { get; set; }
        
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
        /// Screen swipe event
        /// </summary>
        /// <param name="args"></param>
        public void HandleSwipeEnd(SwipeEventArgs args)
        {
            if(args.SwipeDirection == SwipeDirection.LeftToRight)
                DrawerOpen = true;

            if (args.SwipeDirection == SwipeDirection.RightToLeft)
                DrawerOpen = false;

        }

        /// <summary>
        /// Sets the default branch
        /// </summary>
        /// <returns></returns>
        public async Task SetDefaultBranch()
        {
            if (CompanyManager != null)
            {
                ColorBranch = Color.Warning;

                foreach (var company in CompanyManager.Items)
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

                        await CompanyManager.UpdateAsync(company);

                        if (_snackbar == null)
                            return;
                        _snackbar.Configuration.PositionClass = Defaults.Classes.Position.BottomCenter;
                        _snackbar.Add($"<b style='color:#00FF00'>{Name}</b> выбран по умолчанию", Severity.Info);
                    }
                }
            }
        }

        /// <summary>
        /// Parameter set main layout
        /// </summary>
        protected override void OnParametersSet()
        {
            if (CompanyManager != null && Name == null)
            {
                _company = CompanyManager.Items.FirstOrDefault(x => x.Branches.Any(c => c.Default == true));

                Branch = _company?.Branches.FirstOrDefault(x => x.Default == true);

                if (Branch != null)
                {
                    Name = Branch.Name;

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
            if (CompanyManager != null)
                await CompanyManager.ReadAllCompaniesAsync();
        }

        public void GetSelectBranch(BranchModel branch)
        {
            Branch = branch;
            DrawerToggle();
            StatusStar = !Branch.Default;
        }
    }
}
