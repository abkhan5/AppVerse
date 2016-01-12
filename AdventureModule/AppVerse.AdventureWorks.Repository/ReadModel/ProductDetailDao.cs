using AppVerse.AdventureWorks.DataEntities;
using System.Collections.Generic;
using System.Linq;

namespace AppVerse.AdventureWorks.Repository.ReadModel
{
    public class ProductDetailDao : IProductDetailDao
    {
        private IQueryable<DTO.Product> GetProductsQuery(AdventureworksDataModel context)
        {
            var productDetail = from prd in context.Products

                                join prdPhotoRel in context.ProductProductPhotoes
                                on prd.ProductID equals prdPhotoRel.ProductID

                                join prdPhoto in context.ProductPhotoes on
                                prdPhotoRel.ProductPhotoID equals prdPhoto.ProductPhotoID

                                join prdModel in context.ProductModels
                                on prd.ProductModelID equals prdModel.ProductModelID
                                orderby prd.ProductID
                                select new DTO.Product
                                {
                                    ProductID = prd.ProductID,
                                    ProductName = prd.Name,
                                    ProductModelName = prdModel.Name,
                                    ProductNumber = prd.ProductNumber,
                                    ThumbNailPhoto = prdPhoto.ThumbNailPhoto,
                                    ThumbnailPhotoFileName = prdPhoto.ThumbnailPhotoFileName
                                };
            return productDetail;

        }

        public IEnumerable<DTO.Product> GetAllProducts()
        {
            var context = new AdventureworksDataModel();

            var productDetails = GetProductsQuery(context);
            foreach (var item in productDetails)
            {
                yield return item;
            }
        }

    }
}
