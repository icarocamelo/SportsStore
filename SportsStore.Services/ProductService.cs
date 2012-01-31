using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;
using System.ServiceModel.Web;
using SportsStore.Domain;
using SportsStore.Domain.Abstract;
using SportsStore.Domain.Concrete;
using Microsoft.ApplicationServer.Http.Dispatcher;
using System.Net.Http;

namespace SportsStore.Services
{
    [ServiceContract]
    public class ProductService
    {
        [WebGet(ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json)]
        public Product GetProduct(int id)
        {
            EFProductRepository repo = new EFProductRepository();
            var prod = repo.Products.Where(x => x.ProductID == id).SingleOrDefault();
            
            return prod != null ? prod : null;
        }

        [WebInvoke(ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json)] //Método POST
        public void Add(Product product)
        {
            EFProductRepository repo = new EFProductRepository();

            if (product == null)
                throw new HttpResponseException(System.Net.HttpStatusCode.BadRequest);

            try
            {
                repo.Save(product);
            }
            catch
            {
                throw;
            }
            finally
            {
                repo = null;
            }
        }
    }
}
