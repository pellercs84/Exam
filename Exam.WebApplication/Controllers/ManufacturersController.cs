using Exam.DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;


namespace Exam.WebApplication.Controllers
{
    public class ManufacturersController : ApiController
    {
        private IDataProvider<Manufacturer> myDataProvider = WebApiApplication.DataProviderFactory.CreateManufacturerDataProvider();

        [HttpGet()]
        public IHttpActionResult GetAllManufacturers()
        {
            IHttpActionResult result = null;
            IList<Manufacturer> list = myDataProvider.GetAll();
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
        public IHttpActionResult GetManufacturer(int id)
        {
            var manufacturer = myDataProvider.GetSingle(m => m.ID == id, m => m.Products);
            if (manufacturer == null)
            {
                return NotFound();
            }
            return Ok(manufacturer);
        }

        [HttpPut()]
        public IHttpActionResult Put(int id, Manufacturer manufacturer)
        {
            IHttpActionResult result = null;

            try
            {
                myDataProvider.Update(manufacturer);
                result = Ok(manufacturer);
            } catch(Exception)
            {
                result = NotFound();
            }
            return result;
        }


        [HttpPost()]
        public IHttpActionResult Post(Manufacturer manufacturer)
        {
            IHttpActionResult result = null;
            try
            {
                myDataProvider.Add(manufacturer);
                result = Created<Manufacturer>(Request.RequestUri + manufacturer.ID.ToString(), manufacturer);
            }
            catch (Exception)
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
