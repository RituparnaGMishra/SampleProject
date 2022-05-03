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
    public static class CarManagement
    {
        private static readonly CarManagementService _service = new();

        [FunctionName("CarManagementGetAll")]
        public static IActionResult Get(
            [HttpTrigger(AuthorizationLevel.Function, "get", Route = "car")] HttpRequest req,
            ILogger log)
        {
            var cars = _service.GetAll();
            if (cars != null && cars.Any())
            {
                return new OkObjectResult(cars);
            }
            else
            {
                return new NoContentResult();
            }
        }
        [FunctionName("CarManagementGetById")]
        public static IActionResult GetById(
            [HttpTrigger(AuthorizationLevel.Function, "get", Route = "car/{id}")] HttpRequest req,
            ILogger log, Guid id)
        {
            var Car = _service.GetById(id);
            if (Car != null)
            {
                return new OkObjectResult(Car);
            }
            else
            {
                return new NotFoundResult();
            }

        }

        [FunctionName("CarManagementPost")]
        public static IActionResult Post(
           [HttpTrigger(AuthorizationLevel.Function, "post", Route = "car")] HttpRequest req,
           ILogger log)
        {

            string requestBody = new StreamReader(req.Body).ReadToEnd();
            try
            {
                var input = JsonConvert.DeserializeObject<Car>(requestBody);

                var car = _service.Add(input);
                return new OkObjectResult(car);

            }
            catch (Exception ex)
            {
                return new BadRequestResult();
            }
        }

        [FunctionName("CarManagementDelete")]
        public static IActionResult Delete(
           [HttpTrigger(AuthorizationLevel.Function, "delete", Route = "car/{id}")] HttpRequest req,
           ILogger log, Guid id)
        {

            var car = _service.GetById(id);
            if (car == null)
            {
                return new BadRequestResult();
            }

            _service.Delete(id);
            return new NoContentResult();
        }


        [FunctionName("CarManagementPut")]
        public static IActionResult Put(
           [HttpTrigger(AuthorizationLevel.Function, "put", Route = "car")] HttpRequest req,
           ILogger log)
        {

            string requestBody = new StreamReader(req.Body).ReadToEnd();
            try
            {
                var input = JsonConvert.DeserializeObject<Car>(requestBody);

                var car = _service.Add(input);
                var carExisting = _service.GetById(car.Id);
                if (car == null)
                {
                    return new BadRequestResult();
                }
                else
                {
                    var updatedDemand = _service.Edit(car);
                }
                return new OkObjectResult(car);

            }
            catch (Exception ex)
            {
                return new BadRequestResult();
            }
        }
    }
}
