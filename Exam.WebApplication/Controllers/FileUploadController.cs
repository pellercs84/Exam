/* -------------------------------------------------------------------------------------------------
   Restricted - Copyright (C) Siemens Healthcare GmbH/Siemens Medical Solutions USA, Inc., 2016. All rights reserved
   ------------------------------------------------------------------------------------------------- */

using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Web;
using System.Web.Http;

namespace Exam.WebApplication.Controllers
{
    public class FileUploadController:ApiController
    {
        [HttpPost]
        public IHttpActionResult UploadFile()
        {
            IHttpActionResult result = null;
            if (HttpContext.Current.Request.Files.AllKeys.Any())
            {
                var httpPostedFile = HttpContext.Current.Request.Files["UploadedImage"];

                if (httpPostedFile != null)
                {
                    string id = DateTime.Now.ToString("MMddyyyyHHmmssfff")+Path.GetExtension(httpPostedFile.FileName);
                    string targetFolder = HttpContext.Current.Server.MapPath("~/UploadedFiles");
                    if (!Directory.Exists(targetFolder))
                    {
                        Directory.CreateDirectory(targetFolder);
                    }
                    var fileSavePath = Path.Combine(targetFolder, id);
                    httpPostedFile.SaveAs(fileSavePath);
                    
                    HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK, "value");
                    response.Content = new StringContent(id, Encoding.Unicode);
                    result = Ok(id);
                    
                }

                //todo handle negativ cases
            }
            return result;
        }
    }
}