using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TreeView.DAL.DTO;
using TreeView.DAL.Context;

namespace TreeView.DAL.Repo
{
    public interface IProjectTreeRepo
    {
        List<ProjectTreeDTO> GetProjectTree();
    }
    public class ProjectTreeRepo : IProjectTreeRepo
    {
        private AppDbContext _dbContext;
        public ProjectTreeRepo(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public List<ProjectTreeDTO> GetProjectTree()
        {
            return _dbContext.ProjectTree.ToList();
        }
    }
}
