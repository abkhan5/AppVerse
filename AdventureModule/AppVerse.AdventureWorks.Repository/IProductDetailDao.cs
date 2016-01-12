using System.Collections.Generic;

namespace AppVerse.AdventureWorks.Repository
{
    public interface IProductDetailDao
    {
        IEnumerable<DTO.Product> GetAllProducts();
    }
}
