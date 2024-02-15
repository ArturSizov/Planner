using Microsoft.AspNetCore.Components;
using MudBlazor;
using Planner.Abstractions;
using Planner.Models;

namespace Planner.Components.Pages
{
    public partial class ServiceComponent
    {
        /// <summary>
        /// Service parameter
        /// </summary>
        [Parameter] public ServiceModel Service { get; set; } = new();

        /// <summary>
        /// Company data manager
        /// </summary>
        [Inject] private IDataManager<CompanyModel>? _companyManager { get; set; }

        /// <summary>
        /// 85 %
        /// </summary>
        public int? EightyFive { get; set; }

        /// <summary>
        /// 100 %
        /// </summary>
        public int? OneHundred { get; set; }

        /// <summary>
        /// Fact as of today
        /// </summary>
        public int CurrentDatePercentage { get; set; }

        /// <summary>
        /// Target percentage for the current date
        /// </summary>
        public int TargetPercentage { get; set; }

        /// <summary>
        /// Fact difference for current date
        /// </summary>
        public int? DeltaFact { get; set; }

        /// <summary>
        /// Current progress as a percentage
        /// </summary>
        public int CurrentExecutionPercentage { get; set; }

        /// <summary>
        /// Color eighty five
        /// </summary>
        public string? ColorEightyFive { get; set; }

        /// <summary>
        /// Color one hundred
        /// </summary>
        public string? ColorOneHundred { get; set; }

        /// <summary>
        /// Color fact of today
        /// </summary>
        public string? ColorCurrentDatePercentage { get; set; }

        /// <summary>
        /// Color delta fact
        /// </summary>
        public string? ColorDeltaFact { get; set; }

        /// <summary>
        /// Color current execution percentage
        /// </summary>
        public string? ColorCurrentExecutionPercentage { get; set; }

        /// <summary>
        /// Row fact element reference
        /// </summary>
        public MudNumericField<ushort?> StringFactRef = new();

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
        /// Update company
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public async Task UpdateCompanyAsync(string name)
        {
            if(_companyManager != null)
            {
                var company = _companyManager.Items.FirstOrDefault(x => x.Branches.Any(s => s.Services.Any(s => s.Name == name)));

                if(company != null && Service.Fact >= 0)
                {
                    UpdateDate();
                    await _companyManager.UpdateAsync(company);
                }
            }
        }

        /// <summary>
        /// Number validation
        /// </summary>
        /// <param name="number"></param>
        /// <returns></returns>
        public IEnumerable<string> NumberStrength(ushort? number)
        {
            if (number == null)
                yield return "Не может быть пустым";
        }

        /// <summary>
        /// On parameters set
        /// </summary>
        protected override void OnParametersSet()
        {
            UpdateDate();            
        }

        /// <summary>
        /// Updates data when the fact field changes/when the page is loaded
        /// </summary>
        private void UpdateDate()
        {
            if (Service.Plan != null && Service.Fact != null)
            {
                EightyFive = (Service.Plan * 85 / 100) - Service.Fact;

                OneHundred = Service.Plan - Service.Fact;

                if (Service.Plan != 0)
                    CurrentDatePercentage = Convert.ToInt32(Convert.ToDouble(Service.Fact) / Convert.ToDouble(Service.Plan) * 100);
                else
                    CurrentDatePercentage = 0;

                var days = DateTime.Today.Day;

                var lastDayOfMonth = DateTime.DaysInMonth(DateTime.Today.Year, DateTime.Today.Month);

                if (days == lastDayOfMonth)
                    TargetPercentage = 100;
                else
                {
                    if (days > 2)
                        days--;

                    TargetPercentage = Convert.ToInt32(days / Convert.ToDouble(lastDayOfMonth) * 100);
                }

                DeltaFact = Service.Fact - (Service.Plan * TargetPercentage / 100);

                CurrentExecutionPercentage = Convert.ToInt32(Convert.ToDouble(CurrentDatePercentage) / Convert.ToDouble(TargetPercentage) * 100);

                if (EightyFive < 0)
                    EightyFive = 0;

                if (OneHundred < 0)
                    OneHundred = 0;

                SetColor();
            }
        }

        /// <summary>
        /// Set color eighty five/one hundred
        /// </summary>
        private void SetColor()
        {
            if (CurrentDatePercentage >= 85)
                ColorEightyFive = "#32CD32";
            else if(CurrentDatePercentage >= 65)
                ColorEightyFive = "#FFD700";
            else
                 ColorEightyFive = "red";

            if (CurrentDatePercentage >= 100)
                ColorOneHundred = "#32CD32";
            else if (CurrentDatePercentage >= 85)
                ColorOneHundred = "#FFD700";
            else
                ColorOneHundred = "red";


            if (CurrentDatePercentage >= TargetPercentage)
                ColorCurrentDatePercentage = "#32CD32";
            else if (CurrentDatePercentage >= TargetPercentage * 85 / 100)
                ColorCurrentDatePercentage = "#FFD700";
            else
                ColorCurrentDatePercentage = "red";

            if (DeltaFact < 0)
                ColorDeltaFact = "red";
            else
                ColorDeltaFact = "#32CD32";

            if (CurrentExecutionPercentage >= 100)
                ColorCurrentExecutionPercentage = "#32CD32";
            else if (CurrentExecutionPercentage >= 85)
                ColorCurrentExecutionPercentage = "#FFD700";
            else
                ColorCurrentExecutionPercentage = "red";
        }
    }
}
