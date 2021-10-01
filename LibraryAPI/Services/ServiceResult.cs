using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace LibraryAPI.Services
{
    public class ServiceResult<T>
    {
        public ServiceResult(T result, HttpStatusCode code = HttpStatusCode.OK, string message = null)
        {
            Result = result;
            Code = code;
            Message = message;
        }

        public static ServiceResult<T> GetEntityNullResult()
        {
            return new ServiceResult<T>(default(T), HttpStatusCode.BadRequest, "Entity cannot be null.");
        }

        public static ServiceResult<T> GetInternalErrorResult()
        {
            return new ServiceResult<T>(default(T), HttpStatusCode.InternalServerError, "Internal server error occured.");
        }

        public static ServiceResult<T> GetResourceNotFoundResult()
        {
            return new ServiceResult<T>(default(T), HttpStatusCode.NotFound, "Requested resource has not been found.");
        }

        public T Result { get; set; }

        public HttpStatusCode Code { get; set; }

        public string Message { get; set; }

        public bool IsOk()
        {
            return ((int)Code >= 200) && ((int)Code <= 299);
        }
    }

}
