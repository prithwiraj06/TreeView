using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TreeView.BAL.Models
{
    public class Product : IEquatable<Product>
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public List<Domain> Domains { get; set; }

        public bool Equals(Product other)
        {
            if (ProductName == other.ProductName)
                return true;

            return false;
        }

        public override int GetHashCode()
        {
            int hashId = ProductId.GetHashCode();
            int hashDesc = ProductName == null ? 0 : ProductName.GetHashCode();

            return hashId ^ hashDesc;
        }

    }
}
