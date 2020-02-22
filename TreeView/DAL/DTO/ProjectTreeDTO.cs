using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TreeView.DAL.DTO
{
    public class ProjectTreeDTO
    {
        [Key]
        public int Id { get; set; }
        public int PlanId { get; set; }
        public string PlanName { get; set; }
        public int LOBId { get; set; }
        public string LOBName { get; set; }
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public int DomainId { get; set; }
        public string DomainName { get; set; }
        public int MeasureId { get; set; }
        public string MeasureAbbr { get; set; }
    }
}
