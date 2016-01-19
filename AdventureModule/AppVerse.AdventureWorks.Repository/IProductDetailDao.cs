using System.Collections.Generic;
using System.Linq;

namespace AppVerse.AdventureWorks.Repository
{
    public interface IProductDetailDao
    {
        IQueryable<DTO.Product> GetAllProducts(string sort = "ProductID");

        DTO.Product GetProduct(int productId);

    }
}
