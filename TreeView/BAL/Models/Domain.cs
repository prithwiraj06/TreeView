using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TreeView.BAL.Models
{
    public class Domain : IEquatable<Domain>
    {
        public int DomainId { get; set; }
        public string DomainName { get; set; }
        public List<Measure> Measures { get; set; }

        public bool Equals(Domain other)
        {
            if (DomainId == other.DomainId)
                return true;

            return false;
        }

        public override int GetHashCode()
        {
            int hashId = DomainId.GetHashCode();
            int hashDesc = DomainName == null ? 0 : DomainName.GetHashCode();

            return hashId ^ hashDesc;
        }
    }
}
