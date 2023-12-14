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

        public ObservableCollection<Company> Companies { get; set; } = new();

        public NavMenu()
        {
            Companies = new ObservableCollection<Company>()
            {
                new Company
                { 
                    Name = "Набережно-Челнинский ЗУЭС",
                    Branches = new ObservableCollection<Branch>
                    {
                        new Branch {Name = "Мензелинский РУЭС"},
                        new Branch {Name = "Агрызский РУЭС"}
                    }
                }, 
                new Company 
                { 
                    Name = "Альметьевский ЗУЭС",
                    Branches = new ObservableCollection<Branch>
                    {
                        new Branch {Name = "Муслюмовский РУЭС"},
                        new Branch {Name = "Алексеевский РУЭС"}
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
