using Microsoft.AspNetCore.Components;

namespace Planner.Components.Layout
{
    partial class MainLayout
    {
        /// <summary>
        /// Company name
        /// </summary>
        [Parameter] public string Name { get; set; } = string.Empty;

        /// <summary>
        /// Drawer open
        /// </summary>
        bool _drawerOpen = true;

        /// <summary>
        /// Open/close menu
        /// </summary>
        public void DrawerToggle()
        {
            _drawerOpen = !_drawerOpen;
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

                        if (res != null && string.IsNullOrEmpty(Name))
                            Name = res;
                        else
                            Name = "Planner";
                    }
                }
                else
                    Name = "Planner";
            }
        }
    }
}
