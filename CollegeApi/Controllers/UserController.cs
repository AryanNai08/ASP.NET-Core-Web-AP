using AutoMapper;
using CollegeApi.Data.Repository;
using CollegeApi.Models;
using CollegeApi.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace CollegeApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {

        private readonly ILogger<UserController> _Logger;

        private readonly IMapper _mapper;

        private APIResponse _apiResponse;

        private readonly IUserService _userService;

       public UserController(ILogger<UserController> logger, IMapper mapper,IUserService userService)
        {

            _Logger = logger;
            _mapper = mapper;
            _apiResponse = new();
            _userService = userService;

        }

        [HttpPost]
        [Route("Create")]
        //api/user/create
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<ActionResult<APIResponse>> CreateUserAsync(UserDTO dto)
        {
            try
            {
                var userCreated = await _userService.CreateUserAsync(dto);

                _apiResponse.Data = userCreated;
                _apiResponse.Status = true;
                _apiResponse.StatusCode = HttpStatusCode.OK;
                //OK - 200 - Success
                return Ok(_apiResponse);
            }
            catch (Exception ex)
            {
                _apiResponse.Error = new List<string> { ex.Message };
                _apiResponse.StatusCode = HttpStatusCode.InternalServerError;
                _apiResponse.Status = false;
                return StatusCode(500, _apiResponse);
            }
        }


    }
}
