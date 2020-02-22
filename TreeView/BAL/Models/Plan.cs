using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TreeView.BAL.Models
{
    public class Plan
    {
        public int PlanId { get; set; }
        public string PlanName { get; set; }
        public List<LOB> Lobs { get; set; }
                
    }
}
