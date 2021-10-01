using LibraryAPI.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LibraryAPI.Controllers
{
    public class ControllerResult<T>
    {
        private readonly ServiceResult<T> serviceResult;
        public ControllerResult(ServiceResult<T> serviceResult)
        {
            this.serviceResult = serviceResult;
        }

        public ActionResult GetResponse()
        {
            ObjectResult result = serviceResult.IsOk() ? new ObjectResult(serviceResult.Result) : new ObjectResult(new { message = serviceResult.Message });
            result.StatusCode = (int)serviceResult.Code;
            return result;
        }

    }
}
