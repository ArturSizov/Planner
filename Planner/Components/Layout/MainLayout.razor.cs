namespace Planner.Components.Layout
{
    partial class MainLayout
    {
        bool _drawerOpen = true;

        public void DrawerToggle()
        {
            _drawerOpen = !_drawerOpen;
        }
    }
}
