using InfectedAPI.Models;
using InfectedAPI.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InfectedAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class InterestedUserController : ControllerBase
    {
        private readonly IServiceInterestedUser _serviceInterestedUser;
        public InterestedUserController(IServiceInterestedUser serviceInterestedUser)
        {
            _serviceInterestedUser = serviceInterestedUser;
        }
        /// <summary>
        /// Add to the database an user interested on close to expire vaccines
        /// </summary>
        /// <param name="viewModel">User informations</param>
        /// <returns>The created user</returns>
        /// <response code="201">Return the removed user removed from the database</response>
        /// <response code="400">There is an user in the database with the provided IdNumber and Name</response>
        /// <response code="500">Internal Error</response>
        [HttpPost]
        public async Task<IActionResult> AddInterestedUser([FromBody] InterestedUserViewModel viewModel)
        {
            var input = new InterestedUserViewModelInput
            {
                IdNumber = viewModel.IdNumber,
                Name = viewModel.Name
            };

            try
            {
                var user = _serviceInterestedUser.GetInterestedUser(input);
                if (user != null)
                    return BadRequest($"There is a register with this IdNumber = {input.IdNumber} and Name = {input.Name}");

            }
            catch (Exception)
            { }

            await _serviceInterestedUser.AddInterestedUser(viewModel);

            return Created("", viewModel);

        }
        /// <summary>
        /// Removes an user from the database
        /// </summary>
        /// <param name="input">Given Inputs IdNumber and Name</param>
        /// <returns>InterestedUserViewModel</returns>
        /// <response code="200">Return the removed user removed from the database</response>
        /// <response code="404">There is no register with the provided input</response>
        /// <response code="500">Internal Error</response>
        [HttpDelete]
        public async Task<ObjectResult> RemoveVaccinatedUser([FromBody] InterestedUserViewModelInput input)
        {
            try
            {
                var user = _serviceInterestedUser.GetInterestedUser(input);
                await _serviceInterestedUser.RemoveVaccinatedUser(input.IdNumber);

                return Ok(user);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }
        /// <summary>
        /// Get All the users from the database, ordered descending by Age
        /// </summary>
        /// <returns>A List of InterestedUserViewModel</returns>
        /// <response code="200">Return all the users from the database</response>
        /// <response code="404">There is no registers in the database</response>
        /// <response code="500">Internal Error</response>
        [HttpGet]
        [Route("GetAllOrderedByAge")]
        public ActionResult<List<InterestedUserViewModel>> GetAllOrderedByAge()
        {
            var users = _serviceInterestedUser.GetAllOrderedByAge();

            if (users == null)
                return NotFound("Empty Database");

            return Ok(users);
        }
        /// <summary>
        /// Get an user from the database
        /// </summary>
        /// <returns>A List of InterestedUserViewModel</returns>
        /// <response code="200">Return an user from the database</response>
        /// <response code="404">There is no register with the provided input</response>
        /// <response code="500">Internal Error</response>
        [HttpGet]
        [Route("GetInterestedUser/{idNumber}/{name}")]
        public ObjectResult GetInterestedUser([FromRoute] long idNumber, [FromRoute] string name)
        {
            var input = new InterestedUserViewModelInput
            {
                IdNumber = idNumber,
                Name = name
            };

            try
            {
                var user = _serviceInterestedUser.GetInterestedUser(input);

                return Ok(user);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }
        /// <summary>
        /// Get the next N users from the database, ordered by vaccnation priority
        /// </summary>
        /// <returns>A List of InterestedUserViewModel</returns>
        /// <response code="200">Return all the users from the database</response>
        /// <response code="404">There is no registers in the database</response>
        /// <response code="500">Internal Error</response>
        [HttpGet]
        [Route("GetNNextToBeVaccinatedUsers/{N}")]
        public ObjectResult GetNNextToBeVaccinatedUsers([FromRoute] long N)
        {
            var users = _serviceInterestedUser.GetNNextToBeVaccinatedUsers(N);

            if (users == null)
                return NotFound("Empty Database");

            return Ok(users);
        }
        /// <summary>
        /// Get all the users from the database, ordered by vaccnation priority
        /// </summary>
        /// <returns>A List of InterestedUserViewModel</returns>
        /// <response code="200">Return all the users from the database</response>
        /// <response code="404">There is no registers in the database</response>
        /// <response code="500">Internal Error</response>
        [HttpGet]
        [Route("GetAllNextToBeVaccinatedUsers")]
        public ObjectResult GetAllNextToBeVaccinatedUsers() 
        {
            var users = _serviceInterestedUser.GetAllNextToBeVaccinatedUsers();

            if (users == null)
                return NotFound("Empty Database");

            return Ok(users);
        }
    }
}
