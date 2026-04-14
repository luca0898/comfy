using System;
using System.Net;

namespace Comfy.SystemObjects.Exceptions
{
    public class ComfyApplicationException : Exception
    {
        public HttpStatusCode StatusCode { get; set; }

        public ComfyApplicationException(
            string message,
            HttpStatusCode statusCode = HttpStatusCode.BadRequest
            ) : base(message)
        {
            StatusCode = statusCode;
        }

        public ComfyApplicationException(
            string message,
            Exception exception,
            HttpStatusCode statusCode = HttpStatusCode.BadRequest
            ) : base(message, exception)
        {
            StatusCode = statusCode;
        }
    }
}
