using AppVerse.AdventureWorks.DataEntities;
using System.Collections.Generic;
using System.Linq;
using AppVerse.Common.Utilities;

namespace AppVerse.AdventureWorks.Repository.ReadModel
{
    public class ProductDetailDao : IProductDetailDao
    {
        private IQueryable<DTO.Product> GetProductsQuery(AdventureworksDataModel context)
        {
            var productDetail = from prd in context.Products

                                join prdPhotoRel in context.ProductProductPhotoes
                                on prd.ProductID equals prdPhotoRel.ProductID
                                into prdPhotoJoin 
                                from prdJoinRes in prdPhotoJoin.DefaultIfEmpty()

                                join prdPhoto in context.ProductPhotoes on
                                prdJoinRes.ProductPhotoID equals prdPhoto.ProductPhotoID

                                join prdModel in context.ProductModels
                                on prd.ProductModelID equals prdModel.ProductModelID
                                into prdModelJoin 
                                from prdModelRes in prdModelJoin.DefaultIfEmpty()


                                select new DTO.Product
                                {
                                    ProductID = prd.ProductID,
                                    ProductName = prd.Name,
                                    ProductModelName = prdModelRes.Name,
                                    ProductNumber = prd.ProductNumber,
                                    ThumbNailPhoto = prdPhoto.ThumbNailPhoto,
                                    ThumbnailPhotoFileName = prdPhoto.ThumbnailPhotoFileName
                                };
            return productDetail;

        }

        public IQueryable<DTO.Product> GetAllProducts(string sort)
        {
            var context = new AdventureworksDataModel();

            var productDetails = GetProductsQuery(context);


            if (!string.IsNullOrEmpty(sort))
            {
                productDetails = productDetails.ApplySort(sort);
            }
            return productDetails;
            
        }

        public DTO.Product GetProduct(int productId)
        {
            var context = new AdventureworksDataModel();

           var prd= GetProductsQuery(context).FirstOrDefault(item=>item.ProductID==productId);
            return prd;
        }


    }
}
