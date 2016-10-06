using System.Linq.Expressions;
using Exam.DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;


namespace Exam.WebApplication.Controllers
{
    public class ProductsController : ApiController
    {
        private IDataProvider<Product> myDataProvider = WebApiApplication.DataProviderFactory.CreateProductDataProvider();

        [HttpGet()]
        public IHttpActionResult GetProducts(int id)
        {
            IHttpActionResult result = null;
            IList<Product> list = myDataProvider.GetAll().Where(i=>i.Manufacturer_ID == id).ToList();
            if (list.Count > 0)
            {
                result = Ok(list);
            }
            else
            {
                result = NotFound();
            }
            return result;
        }

        [HttpGet()]
        public IHttpActionResult GetProduct(int id, int manufacturerId)
        {
            var product = myDataProvider.GetSingle(m => m.ID == id && m.Manufacturer.ID == manufacturerId);
            if (product == null)
            {
                return NotFound();
            }
            return Ok(product);
        }

        [HttpPut()]
        public IHttpActionResult Put(int id, Product product)
        {
            IHttpActionResult result = null;

            try
            {
                myDataProvider.Update(product);
                result = Ok(product);
            }
            catch (Exception)
            {
                result = NotFound();
            }
            return result;
        }


        [HttpPost()]
        public IHttpActionResult Post(Product product)
        {
            IHttpActionResult result = null;
            try
            {
                myDataProvider.Add(product);
                result = Created<Product>(Request.RequestUri + product.ID.ToString(), product);
            }
            catch (Exception ex)
            {
                result = NotFound();
            }

            return result;
        }

        [HttpDelete()]
        public IHttpActionResult Delete(int id)
        {
            IHttpActionResult result = null;
            try
            {
                myDataProvider.Remove(id);
                result = Ok(true);
            }
            catch (Exception)
            {
                result = NotFound();
            }

            return result;
        }

    }
}
