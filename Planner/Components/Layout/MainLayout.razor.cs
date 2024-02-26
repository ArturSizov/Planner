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
        /// Company model
        /// </summary>
        private CompanyModel? _company;

        /// <summary>
        /// Color branch star
        /// </summary>
        private Color _colorStar;

        /// <summary>
        /// Branch name
        /// </summary>
        private string? _name;

        /// <summary>
        /// Dialog service
        /// </summary>
        [Inject] private ICustomDialogService? _customDialogService { get; set; }

        /// <summary>
        /// Snackbar
        /// </summary>
        [Inject] private ISnackbar? _snackbar { get; set; }

        /// <summary>
        /// Page navigation
        /// </summary>
        [Inject] private NavigationManager? _navigation { get; set; }

        /// <summary>
        /// Branch name
        /// </summary>
        public string? Name
        {
            get
            {
                if (Branch == null)
                {
                    _name = "Planner";
                    StatusStar = true;
                }
                else
                {
                    _name = Branch.Name;
                    StatusStar = false;
                }
                return _name;
            }

            set => _name = value;
        }

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
        public void DrawerToggle() => DrawerOpen = !DrawerOpen;

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
            }
        }

        /// <summary>
        /// Create company open dialog window
        /// </summary>
        public async Task CreateCompanyAsync()
        {
            if (_customDialogService == null)
                return;

            var result = await _customDialogService.CreateItemDialog<CreateCompany>("Добавить компанию", []);

            var companyName = result.Item2 as string;

            var company = new CompanyModel { Name = companyName };

            if (result.Item1 && companyName != null)
            {
                CompanyManager?.CreateAsync(company);

                StateHasChanged();
            }
            else
                return;

           await CreateBranchAsync(company);
        }

        /// <summary>
        /// Delete company
        /// </summary>
        /// <returns></returns>
        public async Task DeleteCompanyAsync(CompanyModel company)
        {
            if (_customDialogService == null || company?.Name == null)
                return;

            var result = await _customDialogService.DeleteItemDialog(company.Name);

            if (result && _company != null && CompanyManager != null)
            {
                await CompanyManager.DeleteAsync(company);

                Branch = await SetDefaultBranch(company);

                DrawerOpen = false;
            }
        }

        /// <summary>
        /// Returns the selected branch
        /// </summary>
        /// <param name="branch"></param>
        public void GetSelectBranch(BranchModel branch)
        {
            _company = CompanyManager?.Items.FirstOrDefault(x => x.Branches.Any(c => c.Name == branch.Name));

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

            _company = company;

            var result = await _customDialogService.CreateItemDialog<CreateCompany>("Добавить филиал", []);

            if (result.Item1 && result.Item2 is string name && CompanyManager != null)
            {
                var branch = new BranchModel
                {
                    Name = name,
                    Default = !CompanyManager.Items.Any(x => x.Branches.Any(b => b.Default == true))
                };

                _company?.Branches.Add(branch);

                if(_company != null)
                    await CompanyManager.UpdateAsync(_company);

                GetSelectBranch(branch);
            }
        }

        /// <summary>
        /// Delete branch
        /// </summary>
        /// <returns></returns>
        public async Task DeleteBranchAsync()
        {
            if (_customDialogService == null || Branch == null || CompanyManager == null)
                return;

            var result = await _customDialogService.DeleteItemDialog(Branch.Name);

            _company = CompanyManager.Items.FirstOrDefault(x => x.Branches.Any(c => c.Name == Branch.Name));
  
            if (result && _company != null)
            {
                _company.Branches.Remove(Branch);

                Branch = await SetDefaultBranch(_company);
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

                if (result.Item2 is string name && _company != null)
                {
                    if (Branch != null)
                    {
                        Branch.Name = name;
 
                        await CompanyManager.UpdateAsync(_company);

                        GetSelectBranch(Branch);

                        DrawerOpen = false;
                    }
                }
            }
            else
                return;
        }

        /// <summary>
        /// Sets the default branch
        /// </summary>
        /// <param name="company"></param>
        /// <returns></returns>
        private async Task <BranchModel> SetDefaultBranch(CompanyModel company)
        {
            if (Branch == null || CompanyManager == null)
                return new BranchModel();

            BranchModel? newBranch = null;

            if (company?.Branches.Count == 0 && company != null)
            {
                await CompanyManager.UpdateAsync(company);

                foreach (var item in CompanyManager.Items)
                {
                    if (item.Branches.Count != 0)
                    {
                        company = item;
                        break;
                    }
                }
            }

           if(company != null)
            {
                foreach (var branch in company.Branches)
                {
                    branch.Default = true;
                    newBranch = branch;
                    break;
                }

                await CompanyManager.UpdateAsync(company);
            }

            return newBranch!;
        }
    }
}
