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
    public static class DemandManagement
    {
        private static readonly DemandManagementService _service = new();

        [FunctionName("DemandManagementGetAll")]
        public static IActionResult Get(
            [HttpTrigger(AuthorizationLevel.Function, "get", Route = "demand")] HttpRequest req,
            ILogger log)
        {
            var demands = _service.GetAll();
            if (demands != null && demands.Any())
            {
                return new OkObjectResult(demands);
            }
            else
            {
                return new NoContentResult();
            }
        }
        [FunctionName("DemandManagementGetById")]
        public static IActionResult GetById(
            [HttpTrigger(AuthorizationLevel.Function, "get", Route = "demand/{id}")] HttpRequest req,
            ILogger log, Guid id)
        {
            var demand = _service.GetById(id);
            if (demand != null)
            {
                return new OkObjectResult(demand);
            }
            else
            {
                return new NotFoundResult();
            }

        }

        [FunctionName("DemandManagementPost")]
        public static IActionResult Post(
           [HttpTrigger(AuthorizationLevel.Function, "post", Route = "demand")] HttpRequest req,
           ILogger log)
        {

            string requestBody = new StreamReader(req.Body).ReadToEnd();
            try
            {
                var input = JsonConvert.DeserializeObject<Demand>(requestBody);

                var demand = _service.Add(input);
                return new OkObjectResult(demand);

            }
            catch (Exception ex)
            {
                return new BadRequestResult();
            }
        }

        [FunctionName("DemandManagementDelete")]
        public static IActionResult Delete(
           [HttpTrigger(AuthorizationLevel.Function, "delete", Route = "demand/{id}")] HttpRequest req,
           ILogger log, Guid id)
        {

            var demand = _service.GetById(id);
            if (demand == null)
            {
                return new BadRequestResult();
            }

            _service.Delete(id);
            return new NoContentResult();
        }


        [FunctionName("DemandManagementPut")]
        public static IActionResult Put(
           [HttpTrigger(AuthorizationLevel.Function, "put", Route = "demand")] HttpRequest req,
           ILogger log)
        {

            string requestBody = new StreamReader(req.Body).ReadToEnd();
            try
            {
                var input = JsonConvert.DeserializeObject<Demand>(requestBody);

                var demand = _service.Add(input);
                var demandExisting = _service.GetById(demand.Id);
                if (demand == null)
                {
                    return new BadRequestResult();
                }
                else
                {
                    var updatedDemand = _service.Edit(demand);
                }
                return new OkObjectResult(demand);

            }
            catch (Exception ex)
            {
                return new BadRequestResult();
            }
        }
    }
}
