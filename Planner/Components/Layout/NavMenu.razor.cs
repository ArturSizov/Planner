using Microsoft.AspNetCore.Components;
using MudBlazor;
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
        [Inject] public IDialogService DialogService { get; set; } = new DialogService();

        public ObservableCollection<CompanyModel> Companies { get; set; } = new();

        public NavMenu()
        {
            Companies = new ObservableCollection<CompanyModel>()
            {
                new CompanyModel
                { 
                    Name = "Набережно-Челнинский ЗУЭС",
                    Branches = new ObservableCollection<BranchModel>
                    {
                        new BranchModel {Name = "Мензелинский РУЭС"},
                        new BranchModel {Name = "Агрызский РУЭС"}
                    }
                }, 
                new CompanyModel 
                { 
                    Name = "Альметьевский ЗУЭС",
                    Branches = new ObservableCollection<BranchModel>
                    {
                        new BranchModel {Name = "Муслюмовский РУЭС"},
                        new BranchModel {Name = "Алексеевский РУЭС"}
                    }
                }
            };
            
        }

        /// <summary>
        /// Open dialog window
        /// </summary>
        /// <param name="title"></param>
        public void OpenDialog(string title)
        {
            var options = new DialogOptions { DisableBackdropClick = true, ClassBackground = "blackout" };
            DialogService.Show<CreatingBranch>(title, options);
        }
    }
}
