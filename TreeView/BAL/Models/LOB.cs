using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TreeView.BAL.Models
{
    public class LOB  : IEquatable<LOB>
    {
        public int LobId { get; set; }
        public string LobName { get; set; }
        public List<Product> Products { get; set; }

        public bool Equals(LOB other)
        {
            if (LobId == other.LobId)
                return true;

            return false;
        }

        public override int GetHashCode()
        {
            int hashId = LobId.GetHashCode();
            int hashDesc = LobName == null ? 0 : LobName.GetHashCode();

            return hashId ^ hashDesc;
        }
        
    }
}
