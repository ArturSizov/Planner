using Planner.DataAccessLayer.DAO;
using Planner.Models;

namespace Planner.Auxiliary
{
    /// <summary>
    /// Data Access Object extensions
    /// </summary>
    public static class DAOExtensions
    {
        /// <summary>
        /// Transforms the company into a model
        /// </summary>
        /// <param name="dao"></param>
        /// <returns></returns>
        public static CompanyModel ToModel(this CompanyDAO dao) => new()
        {
            Id = dao.Id,
            Name = dao.Name,
            Branches = dao.Branches
        };

        /// <summary>
        /// Transforms the company into a DAO
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static CompanyDAO ToDAO(this CompanyModel model) => new()
        {
            Id = model.Id,
            Name = model.Name,
            Branches = model.Branches
        };
    }
}
