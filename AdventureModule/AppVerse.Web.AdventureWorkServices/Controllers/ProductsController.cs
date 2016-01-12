using AppVerse.AdventureWorks.DTO;
using AppVerse.AdventureWorks.Repository;
using AppVerse.AdventureWorks.Repository.ReadModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace AppVerse.Web.AdventureWorkServices.Controllers
{
    
    [RoutePrefix("api")]
    public class ProductsController : ApiController
    {

        [HttpGet]
        // GET: api/Products
        public IEnumerable<Product> Get()
        {
            IProductDetailDao repo = new ProductDetailDao();
            return repo.GetAllProducts();
        }

        [HttpGet]
        // GET: api/Products/5
        public string GetModelNumber(int id)
        {
            IProductDetailDao repo = new ProductDetailDao();
            return repo.GetAllProducts()?.First(item=>item.ProductID==id)?.ProductModelName;
        }


        [Route("api/products/{id}/ProductName")]
        [HttpGet]
        // GET: api/Products/5
        public string GetProductName(int id)
        {
            IProductDetailDao repo = new ProductDetailDao();
            return repo.GetAllProducts()?.First(item => item.ProductID == id)?.ProductName;
        }


        [HttpPost]
        // POST: api/Products
        public void Post([FromBody]Product product)
        {
            if (product==null)
            {
              BadRequest();
            }
        }

        [HttpPut]
        // PUT: api/Products/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Products/5
        public void Delete(int id)
        {
        }
    }
}
