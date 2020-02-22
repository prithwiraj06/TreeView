using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TreeView.BAL.Models;
using TreeView.DAL.Repo;

namespace TreeView.BAL.Services
{
    public interface IProjectTreeService
    {
        List<Plan> GetProjectTree();
    }
    public class ProjectTreeService : IProjectTreeService
    {
        private IProjectTreeRepo _projectTreeRepo;

        public ProjectTreeService(IProjectTreeRepo projectTreeRepo)
        {
            _projectTreeRepo = projectTreeRepo;
        }

        public List<Plan> GetProjectTree()
        {
            var list = _projectTreeRepo.GetProjectTree().OrderBy(x => x.PlanId).ToList();
            List<ProjectTreeMasterModel> projectTreeData = new List<ProjectTreeMasterModel>();
            list.ForEach(data =>
            {
                ProjectTreeMasterModel model = new ProjectTreeMasterModel();
                model.PlanId = data.PlanId;
                model.PlanName = data.PlanName;
                model.LobId = data.LOBId;
                model.LobName = data.LOBName;
                model.ProductId = data.ProductId;
                model.ProductName = data.ProductName;
                model.DomainId = data.DomainId;
                model.DomainName = data.DomainName;
                model.MeasureId = data.MeasureId;
                model.MeasureAbbr = data.MeasureAbbr;
                projectTreeData.Add(model);
            });
            
            List<Plan> projectTreeMasterList = new List<Plan>();
            var resultGroup = projectTreeData.GroupBy(x => x.PlanId).Select(g => g.ToList()).ToList();

            foreach (var plans in resultGroup)
            {
                Plan plan = new Plan { PlanId = plans.First().PlanId, PlanName = plans.First().PlanName };
                var lobs = from record in projectTreeData
                           where record.PlanId == plan.PlanId
                           select new LOB { LobId = record.LobId, LobName = record.LobName };
                plan.Lobs = lobs.Distinct().OrderBy(x => x.LobName).ToList();

                foreach (var lob in plan.Lobs)
                {
                    var prods = from record in projectTreeData
                                where record.PlanId == plan.PlanId && record.LobId == lob.LobId 
                                select new Product { ProductId = record.ProductId, ProductName = record.ProductName };

                    lob.Products = prods.Distinct(new ItemEqualityComparer()).OrderBy(x => x.ProductName).ToList();
                    
                    foreach (var prod in lob.Products)
                    {

                        var domains = from record in projectTreeData
                                      where record.PlanId == plan.PlanId && record.LobId == lob.LobId && record.ProductId == prod.ProductId
                                      select new Domain { DomainId = record.DomainId, DomainName = record.DomainName };

                        prod.Domains = domains.Distinct().OrderBy(x => x.DomainName).ToList();

                        foreach (var domain in prod.Domains)
                        {
                            var masures = from record in projectTreeData
                                          where record.PlanId == plan.PlanId && record.LobId == lob.LobId && record.ProductId == prod.ProductId
                                                && record.DomainId == domain.DomainId
                                          select new Measure { MeasureId = record.MeasureId,  MeasureAbbr = record.MeasureAbbr };

                            domain.Measures = masures.OrderBy(x => x.MeasureAbbr).ToList();
                        }
                    }
                }

                projectTreeMasterList.Add(plan);

            }            
            return projectTreeMasterList;

        }

        // use this class to remove the duplicate objects from the list if IEquitable is not working (Distinct() Method is not working). Duplicate products are coming in lob.Products list & IEquitable is not able to remove the duplicate records. Distinct() Method is not working properly Hence this approch is used
        class ItemEqualityComparer : IEqualityComparer<Product>
        {
            public bool Equals(Product x, Product y)
            {
                // Two items are equal if their keys are equal.
                return x.ProductId == y.ProductId;
            }

            public int GetHashCode(Product obj)
            {
                return obj.ProductId.GetHashCode();
            }
        }
    }

    
}
