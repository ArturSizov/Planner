using Microsoft.AspNetCore.Components;
using Microsoft.Maui;
using MudBlazor;
using Planner.Abstractions;
using Planner.Auxiliary;
using Planner.Components.Dialogs;
using Planner.Models;
using Color = MudBlazor.Color;
using SwipeDirection = MudBlazor.SwipeDirection;


namespace Planner.Components.Layout
{
    partial class MainLayout
    {
        /// <summary>
        /// Dialog service
        /// </summary>
        [Inject] private ICustomDialogService? _customDialogService { get; set; }

        /// <summary>
        /// Snackbar
        /// </summary>
        [Inject] ISnackbar? _snackbar { get; set; }

        /// <summary>
        /// Company data manager
        /// </summary>

        /// <summary>
        /// Page navigation
        /// </summary>
        [Inject] private NavigationManager? _navigation { get; set; }

        /// <summary>
        /// Branch name
        /// </summary>
        public string? Name { get; set; }

        /// <summary>
        /// Company data manager
        /// </summary>
        [Inject] public IDataManager<CompanyModel>? CompanyManager { get; set; }


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
        /// Color branch star
        /// </summary>
        private Color _colorStar;

        /// <summary>
        /// Branch model
        /// </summary>
        public BranchModel? Branch { get; set; }

        /// <summary>
        /// Color branch star
        /// </summary>  
        public Color ColorStar
        {
            get
            {
                if(Branch == null)
                    return _colorStar = Color.Default;

                if (Branch.Default)
                    _colorStar = Color.Warning;
                else _colorStar = Color.Default;

                return _colorStar;
            }

            set => _colorStar = value;
        }

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
                ColorStar = Color.Warning;

                foreach (var company in CompanyManager.Items)
                {
                    foreach (var branch in company.Branches)
                    {
                        branch.Name = branch.Name;
                        branch.Services = branch.Services;
                        branch.Default = false;

                        if (branch.Name == Branch?.Name)
                        {
                            branch.Name = branch.Name;
                            branch.Services = branch.Services;
                            branch.Default = true;
                        }

                        await CompanyManager.UpdateAsync(company);

                        if (_snackbar == null)
                            return;
                        _snackbar.Configuration.PositionClass = Defaults.Classes.Position.BottomCenter;
                        _snackbar.Add($"<b style='color:#00FF00'>{Branch?.Name}</b> выбран по умолчанию", Severity.Info);
                    }
                }
            }
        }

        /// <summary>
        /// Parameter set main layout
        /// </summary>
        protected override void OnParametersSet()
        {
          
        }

        /// <summary>
        ///  On initialized Main Layout
        /// </summary>
        /// <returns></returns>

        protected override async Task OnInitializedAsync()
        {
            if (CompanyManager != null)
            {
                await CompanyManager.ReadAllCompaniesAsync();

                _company = CompanyManager.Items.FirstOrDefault(x => x.Branches.Any(c => c.Default == true));

                Branch = _company?.Branches.FirstOrDefault(x => x.Default);

                if (Branch == null)
                {
                    Name = "Planner";
                    StatusStar = true;
                }
                else
                    Name = Branch.Name;
            }
        }

        /// <summary>
        /// Returns the selected branch
        /// </summary>
        /// <param name="branch"></param>
        public void GetSelectBranch(BranchModel branch)
        {
            Branch = branch;
            Name = branch.Name;

            DrawerToggle();
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

            if (result.Item1 && result.Item2 is string name && CompanyManager != null)
            {
                var branch = new BranchModel
                {
                    Name = name,
                    Default = !CompanyManager.Items.Any(x => x.Branches.Any(b => b.Default == true))
                };

                CompanyManager.Items.FirstOrDefault(x => x.Name == company.Name)?.Branches.Add(branch);

                await CompanyManager.UpdateAsync(company);

                GetSelectBranch(branch);
            }
        }

        /// <summary>
        /// Delete branch
        /// </summary>
        /// <returns></returns>
        public async Task DeleteBranchAsync(BranchModel branch)
        {
            if (_customDialogService == null)
                return;
            var result = await _customDialogService.DeleteItemDialog(branch.Name);

            if (result && Branch != null && CompanyManager != null)
            {
                var company = CompanyManager.Items.FirstOrDefault(x => x.Branches.Any(c => c.Name == branch.Name));

                if (company != null)
                {
                    company.Branches.Remove(branch);

                    if (Branch.Default)
                    {
                        foreach (var item in company.Branches)
                        {
                            item.Default = true;
                            break;
                        }
                    }

                    await CompanyManager.UpdateAsync(company);

                    Name = Branch.Name;

                    await OnInitializedAsync();
                }
            }
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
                { x => x.CompanyName, Branch?.Name}
            };

            var result = await _customDialogService.CreateItemDialog<CreateCompany>("Редактировать филиал", parameters);

            if (result.Item1 && Branch != null && CompanyManager != null && CompanyManager.Items != null)
            {
                var company = CompanyManager.Items.FirstOrDefault(x => x.Branches.Any(c => c.Name == Branch.Name));

                if (result.Item2 is string name && company != null)
                {
                    var branch = company.Branches.FirstOrDefault(x => x.Name == Branch.Name);

                    if (branch != null)
                    {
                        branch.Name = name;
                        branch.Services = Branch.Services;
                        branch.Default = Branch.Default;
                        branch.WeekPlans = Branch.WeekPlans;

                        await CompanyManager.UpdateAsync(company);

                        GetSelectBranch(branch);

                        DrawerOpen = false;
                    }
                }
            }
            else
                return;
        }
    }
}
