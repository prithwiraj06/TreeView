using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TreeView.BAL.Models
{
    public class ProjectTreeMasterModel
    {
        public int PlanId { get; set; }
        public string PlanName { get; set; }
        public int LobId { get; set; }
        public string LobName { get; set; }
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public int DomainId { get; set; }
        public string DomainName { get; set; }
        public int MeasureId { get; set; }
        public string MeasureAbbr { get; set; }
    }
}
