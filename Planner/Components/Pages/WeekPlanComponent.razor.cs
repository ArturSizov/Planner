using Microsoft.AspNetCore.Components;
using MudBlazor;
using Planner.Abstractions;
using Planner.Components.Dialogs;
using Planner.Models;

namespace Planner.Components.Pages
{
    public partial class WeekPlanComponent
    {
        /// <summary>
        /// Week plan parameter
        /// </summary>
        [Parameter] public WeekServiceModel WeekPlan { get; set; } = new();

        /// <summary>
        /// Branch name
        /// </summary>
        [Parameter] public string? BranchName { get; set; }

        /// <summary>
        /// Company data manager
        /// </summary>
        [Inject] private IDataManager<CompanyModel>? _companyManager { get; set; }

        /// <summary>
        /// Dialog service
        /// </summary>
        [Inject] private ICustomDialogService? _customDialogService { get; set; }

        /// <summary>
        /// Snackbar
        /// </summary>
        [Inject] ISnackbar? _snackbar { get; set; }

        /// <summary>
        /// Weekly plan completion percentage
        /// </summary>
        public int? CompletionPercentage { get; set; }

        /// <summary>
        /// Color completion percentage
        /// </summary>
        public string? ColorCompletionPercentage { get; set; }

        /// <summary>
        /// Row week plan element reference
        /// </summary>
        public MudNumericField<ushort?> StringWeekPlanRef = new();

        /// <summary>
        /// Row fact element reference
        /// </summary>
        public MudNumericField<ushort?> StringFactRef = new();

        public bool Focused { get; set; }

        /// <summary>
        /// Focus on fact row
        /// </summary>
        /// <returns></returns>
        public async Task FocusRowFactAsync()
        {
            //this js snippet does `document.querySelector(myRef).focus();`
            await StringFactRef.FocusAsync();
        }

        /// <summary>
        /// Focus on week pLan row
        /// </summary>
        /// <returns></returns>
        public async Task FocusRowWeekPLanAsync()
        {
            //this js snippet does `document.querySelector(myRef).focus();`
            await StringWeekPlanRef.FocusAsync();
        }

        /// <summary>
        /// Update company
        /// </summary>
        /// <returns></returns>
        public async Task UpdateCompanyAsync()
        {
            if (_companyManager != null)
            {
                var company = _companyManager.Items.FirstOrDefault(x => x.Branches.Any(b => b.Name == BranchName));

                if (company != null)
                {
                    UpdateDate();
                    await _companyManager.UpdateAsync(company);
                }
            }
        }

        /// <summary>
        /// Updating the plan per share
        /// </summary>
        /// <returns></returns>
        public async Task ReloadPlanAsync()
        {
            if (_companyManager == null || _customDialogService == null)
                return;

            var branch = _companyManager.Items.SelectMany(x => x.Branches).FirstOrDefault(b => b.Name == BranchName);

            var service = branch?.Services.FirstOrDefault(x => x.Name == WeekPlan.Service.Name);

            if (branch == null || service == null || _snackbar == null)
                return;

            if (DateTime.Now.DayOfWeek != DayOfWeek.Monday)
                if (!await _customDialogService.RefreshPlanOfWeekDialog())
                    return;

            var daysLeft = DateTime.DaysInMonth(DateTime.Now.Year, DateTime.Now.Month) - DateTime.Now.Day;

            var delta = Convert.ToDouble(service.Plan) - Convert.ToDouble(service.Fact);

            if (service.Fact >= service.Plan)
                WeekPlan.Service.Plan = 0;
            else
                WeekPlan.Service.Plan = (ushort?)(Math.Round(delta / daysLeft * 7, 0, MidpointRounding.AwayFromZero));

            WeekPlan.Service.Fact = 0;

            await UpdateCompanyAsync();

            _snackbar.Configuration.PositionClass = Defaults.Classes.Position.BottomCenter;
            _snackbar.Add($"Выполнен расчет для услуги <b style='color:#00FF00'>{service.Name}</b>.<br> <b style='color:red'>ВНИМАНИЕ!</b> Расчет приблизительный</br>", Severity.Info);
        }

        /// <summary>
        /// Update data 
        /// </summary>
        private void UpdateDate()
        {
            if (WeekPlan.Service.Plan != 0)
                CompletionPercentage = Convert.ToInt32(Convert.ToDouble(WeekPlan.Service.Fact) / Convert.ToDouble(WeekPlan.Service.Plan) * 100);
            else
                CompletionPercentage = 0;

            SetColor();
        }

        /// <summary>
        /// Opens the notes window
        /// </summary>
        /// <returns></returns>
        public async Task OpenNotesAsync()
        {
            if (_customDialogService == null)
                return;

            var parameters = new DialogParameters<Notes>
            {
                { x => x.Text,  WeekPlan.Notes}
            };

            var result = await _customDialogService.CreateItemDialog<Notes>($"Заметки/{WeekPlan.Service.Name}", parameters);

            if (result.Item1)
            {
                WeekPlan.Notes = result.Item2 as string;

                await UpdateCompanyAsync();
            }
        }

        /// <summary>
        /// Set color completion percentage
        /// </summary>
        private void SetColor()
        {
            if (CompletionPercentage >= 100)
                ColorCompletionPercentage = "#32CD32";
            else if (CompletionPercentage >= 85)
                ColorCompletionPercentage = "#FFD700";
            else
                ColorCompletionPercentage = "red";
        }

        /// <summary>
        /// Initialized component
        /// </summary>
        protected override void OnInitialized()
        {
            UpdateDate();
        }
    }
}
