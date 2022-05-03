using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using SampleFunctionProject.Services;
using System.Linq;
using SampleFunctionProject.Data.Model;

namespace SampleFunctionProject
{
    public static class UserManagement
    {

        private static readonly UserManagementService _service = new ();
        private static readonly DemandManagementService _demandService = new();

        [FunctionName("UserManagementGetAll")]
        public static IActionResult Get(
            [HttpTrigger(AuthorizationLevel.Function, "get", Route = "user")] HttpRequest req,
            ILogger log)
        {
            var users = _service.GetAll();
            if (users != null && users.Any())
            {
                return new OkObjectResult(users);
            }
            else
            {
                return new NoContentResult();
            }
        }
        [FunctionName("UserManagementGetById")]
        public static IActionResult GetById(
            [HttpTrigger(AuthorizationLevel.Function, "get", Route = "user/{id}")] HttpRequest req,
            ILogger log, Guid id)
        {
            var user = _service.GetById(id);
            if (user != null)
            {
                return new OkObjectResult(user);
            }
            else
            {
                return new NotFoundResult();
            }
           
        }

        [FunctionName("UserManagementPost")]
        public static IActionResult Post(
           [HttpTrigger(AuthorizationLevel.Function, "post", Route = "user")] HttpRequest req,
           ILogger log)
        {

            string requestBody = new StreamReader(req.Body).ReadToEnd();
            try { 
            var input = JsonConvert.DeserializeObject<User>(requestBody);

            var user = _service.Add(input);
            return new OkObjectResult(user);

            }
            catch (Exception ex)
            {
                return new BadRequestResult();
            }
        }

        [FunctionName("UserManagementDelete")]
        public static IActionResult Delete(
           [HttpTrigger(AuthorizationLevel.Function, "delete", Route = "user/{id}")] HttpRequest req,
           ILogger log, Guid id)
        {
            var existingDemands = _demandService.GetByUserId(id);
            if (existingDemands != null && existingDemands.Any())
            {
                return new BadRequestResult();
            }
            
            else
            {
                var user = _service.GetById(id);
                if (user == null)
                {
                    return new BadRequestResult();
                }

                _service.Delete(id);
                return new NoContentResult();
            }

            
        }

        [FunctionName("UserManagementPut")]
        public static IActionResult Put(
           [HttpTrigger(AuthorizationLevel.Function, "put", Route = "user")] HttpRequest req,
           ILogger log)
        {

            string requestBody = new StreamReader(req.Body).ReadToEnd();
            try
            {
                var input = JsonConvert.DeserializeObject<User>(requestBody);

                var user = _service.Add(input);
                var userExisting = _service.GetById(user.Id);
                if (user == null)
                {
                    return new BadRequestResult();
                }
                else
                {
                    var updatedDemand = _service.Edit(user);
                }
                return new OkObjectResult(user);

            }
            catch (Exception ex)
            {
                return new BadRequestResult();
            }
        }
    }
}
