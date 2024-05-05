using Amazon.Runtime.Internal.Endpoints.StandardLibrary;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using StudyfiedBackend.BaseResponse;
using StudyfiedBackend.Controllers.FlashCards;
using StudyfiedBackend.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudyfiedBackendUnitTests.Helpers.ServiceInterfaces
{
    public class BaseServiceInterface
    {
        protected readonly HttpClient _httpClient;

        public BaseServiceInterface(HttpClient client)
        {
            _httpClient = client;
        }
    }
}
