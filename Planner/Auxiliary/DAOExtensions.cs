using Planner.DataAccessLayer.DAO;
using Planner.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        /// <summary>
        /// Transforms the branch into a model
        /// </summary>
        /// <param name="dao"></param>
        /// <returns></returns>
        public static BranchModel ToModel(this BranchDAO dao) => new()
        {
            Id = dao.Id, 
            Name = dao.Name
        };

        /// <summary>
        /// Transforms the branch into a DAO
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static BranchDAO ToDAO(this BranchModel model) => new()
        {
            Id = model.Id,
            Name = model.Name
        };
         
    }
}
