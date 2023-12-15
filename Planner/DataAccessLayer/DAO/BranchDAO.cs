using SQLite;

namespace Planner.DataAccessLayer.DAO
{
    /// <summary>
    /// Branch Data Access Object
    /// </summary>
    public class BranchDAO
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        public string Name { get; set; } = string.Empty;
    }
}
