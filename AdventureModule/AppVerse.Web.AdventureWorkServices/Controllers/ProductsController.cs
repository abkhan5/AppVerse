using AppVerse.AdventureWorks.DTO;
using AppVerse.AdventureWorks.Repository;
using AppVerse.AdventureWorks.Repository.ReadModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using System.Web.Http.Routing;

namespace AppVerse.Web.AdventureWorkServices.Controllers
{
    
    [RoutePrefix("api")]
    public class ProductsController : ApiController
    {

        [HttpGet]
        // GET: api/Products
        // GET: api/Products?sort=productName
        // GET: api/Products?sort=-productName,productId
        public IEnumerable<Product> Get(string sort = "ProductId")
        {
            IProductDetailDao repo = new ProductDetailDao();

            return repo.GetAllProducts(sort);
        }


        private const int maxPageSize = 20;

        [Route("api/products", Name = "ProductListing")]
        [HttpGet]
        public IEnumerable<Product> Get(string sort = "ProductId", int page = 1, int pageSize = maxPageSize)
        {
            try
            {
                IProductDetailDao repository = new ProductDetailDao();

                var products= repository.GetAllProducts(sort);
                

                // ensure the page size isn't larger than the maximum.
                if (pageSize > maxPageSize)
                {
                    pageSize = maxPageSize;
                }

                // calculate data for metadata
                var totalCount = products.Count();
                var totalPages = (int)Math.Ceiling((double)totalCount / pageSize);

                var urlHelper = new UrlHelper(Request);
                var prevLink = page > 1 ? urlHelper.Link("ProductListing",
                    new
                    {
                        page = page - 1,
                        pageSize = pageSize,
                        sort = sort
                    }) : "";
                var nextLink = page < totalPages ? urlHelper.Link("ProductListing",
                    new
                    {
                        page = page + 1,
                        pageSize = pageSize,
                        sort = sort
                    }) : "";


                var paginationHeader = new
                {
                    currentPage = page,
                    pageSize = pageSize,
                    totalCount = totalCount,
                    totalPages = totalPages,
                    previousPageLink = prevLink,
                    nextPageLink = nextLink
                };

                HttpContext.Current.Response.Headers.Add("X-Pagination",
                   Newtonsoft.Json.JsonConvert.SerializeObject(paginationHeader));


                // return result
                return (products
                    .Skip(pageSize * (page - 1))
                    .Take(pageSize)
                    .ToList());

            }
            catch (Exception)
            {
                throw;
            }
        }



        [HttpGet]
        // GET: api/Products/5
        public string GetModelNumber(int id)
        {
            IProductDetailDao repo = new ProductDetailDao();
            return repo.GetProduct(id)?.ProductNumber;
        }


      //  [Route("api/products/{productId}/ProductName")]
        // GET: api/Products/5
        // GET: api/Products?productId=5&&sort=productname

        public IHttpActionResult GetProductName(int productId,string sort = "ProductId")
        {
            IProductDetailDao repo = new ProductDetailDao();
            return Ok( repo.GetAllProducts(sort)?.Where(item => item.ProductID == productId));
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
