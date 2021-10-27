using Ecommerce.BLL;
using Ecommerce.Entity.Classes;
using Ecommerce.Entity.Result;
using ECommerce.WebServis.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;

namespace ECommerce.WebServis.Controllers
{
    public class ProductController : ApiController
    {
        ProductManager productManager = new ProductManager();

        [HttpGet]
        public List<Product> GetAllProducts()
        {
            if (AuthModel.IsAuthorize(Request))
                return productManager.List(t => t.ObjectStatus == Ecommerce.Entity.Enums.ObjectStatus.NonDeleted)._ObjectList.ToList();
            else
            {
                ResultObject<Product> resultObject = new ResultObject<Product>();
                resultObject.Message = "Yetkiniz Yoktur";

                return null;
            }
        }

        [HttpGet]
        public List<Product> GetAllProduct()
        {
            return productManager.List(t => t.ObjectStatus == Ecommerce.Entity.Enums.ObjectStatus.NonDeleted)._ObjectList.ToList();
        }

        [HttpGet]
        public ResultObject<Product> GetProduct()
        {
            return productManager.Find(t => t.ID == 9999);
        }

        [HttpPost]
        public ResultObject<Product> GetProductByProductID([FromBody] Product product)
        {
            if (AuthModel.IsAuthorize(Request))
                return productManager.Find(t => t.ID == product.ID);
            else
                return new ResultObject<Product>() { Message = "Yetkiniz Yok" };
        }

        [HttpPost]
        public ResultObject<Product> GetProductByProductNameContains([FromBody] Product product)
        {
            if (AuthModel.IsAuthorize(Request))
                return productManager.List(t => t.ProductName.Contains(product.ProductName));
            else
                return new ResultObject<Product>() { Message = "Yetkiniz Yok" };
        }

        [HttpPost]
        public ResultObject<Product> GetProductByProductName([FromBody] Test model)
        {
            if (AuthModel.IsAuthorize(Request))
                return productManager.Find(t => t.ProductName == model.X);
            else
                return new ResultObject<Product>() { Message = "Yetkiniz Yok" };
        }

        [HttpGet]
        public ResultObject<Product> GetProductByID(int id)
        {
            return productManager.Find(t => t.ObjectStatus == Ecommerce.Entity.Enums.ObjectStatus.NonDeleted && t.Status == Ecommerce.Entity.Enums.Status.Active && t.ID == id);
        }

        [HttpPost]
        public ResultObject<Product> InsertNewProduct()
        {
            var urunAdi = HttpContext.Current.Request["ProductName"];
            var ProductDescription = HttpContext.Current.Request["ProductDescription"];
            var UnitPrice = HttpContext.Current.Request["UnitPrice"];
            var StockAmount = HttpContext.Current.Request["StockAmount"];
            var MinimumStockAmount = HttpContext.Current.Request["MinimumStockAmount"];
            var VatRate = HttpContext.Current.Request["VatRate"];
            var Category = HttpContext.Current.Request["Category"];
            var UserName = HttpContext.Current.Request["UserName"];

            var httpRequest = HttpContext.Current.Request;

            Product product = new Product()
            {
                CategoryID = Convert.ToInt32(Category),
                ProductName = urunAdi,
                ProductDescription = ProductDescription,
                UnitPrice = Convert.ToDecimal(UnitPrice),
                StockAmount = Convert.ToInt32(StockAmount),
                MinimumStockAmount = Convert.ToInt32(MinimumStockAmount),
                VatRate = Convert.ToInt32(VatRate),
                ProductImages = new List<ProductImage>(),
                CreatedBy = UserName,
                LastModifiedBy = UserName
            };

            for (int i = 0; i < httpRequest.Files.Count; i++)
            {
                var postedFile = httpRequest.Files[i];

                if (postedFile != null && postedFile.ContentLength > 0)
                {
                    var uniqueFileName = Guid.NewGuid();
                    var filePath = @"C:\\inetpub\\wwwroot\\ECommerceApi\\Product\\" + uniqueFileName.ToString() + ".jpg";
                    var fileName = uniqueFileName.ToString() + ".jpg";

                    postedFile.SaveAs(filePath);

                    ProductImage productImage = new ProductImage()
                    {
                        Product = product,
                        ProductImageUrl = fileName,
                        CreatedBy = UserName,
                        LastModifiedBy = UserName
                    };

                    product.ProductImages.Add(productImage);
                }
            }


            return productManager.Insert(product);
        }
    }
}
