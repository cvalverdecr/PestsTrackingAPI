using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace PestTracking.Models
{
    public class RespuestaAPI
    {
      public RespuestaAPI()
      {
        ErrorMessages = new List<string>();
      }  

      public HttpStatusCode StatusCode { get; set; }
      public bool isSuccess { get; set; } = true;
      public List<string> ErrorMessages { get; set; }
      public object Result { get; set; }
    }
}