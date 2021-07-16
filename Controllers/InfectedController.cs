using Api_Mongo.Models;
using Api_Mongo.Services;
using Microsoft.AspNetCore.Mvc;

namespace Api_Mongo.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class InfectedController : ControllerBase
    {
        private readonly IServiceInfected _serviceInfected;
       
        public InfectedController(IServiceInfected serviceInfected)
        {
            _serviceInfected = serviceInfected;
        }
        /// <summary>
        /// Register a infected person in the database
        /// </summary>
        /// <param name="viewModel"> Person info</param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult AddInfected([FromBody] InfectedViewModel viewModel)
        {
            _serviceInfected.AddInfected(viewModel);
            return Ok();
           
        }
        /// <summary>
        /// Get all registers from the database
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult GetInfectedList()
        {
            var infectados = _serviceInfected.GetInfectedList();
            
            return Ok(infectados);
        }
    }
}
