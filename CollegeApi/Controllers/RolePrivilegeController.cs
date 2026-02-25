using AutoMapper;
using CollegeApi.Data;
using CollegeApi.Data.Repository;
using CollegeApi.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace CollegeApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RolePrivilegeController : ControllerBase
    {


        private readonly IMapper _mapper;
        private readonly ICollegeRepository<RolePrivilege> _rolePrivilegeRepository;
        private APIResponse _apiResponse;

        public RolePrivilegeController(IMapper mapper, ICollegeRepository<RolePrivilege> rolePrivilegeRepository)
        {
            _mapper = mapper;
            _rolePrivilegeRepository = rolePrivilegeRepository;
            _apiResponse = new();

        }


        [HttpPost]
        [Route("Create")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status201Created)]

        public async Task<ActionResult<APIResponse>> CreateRole(RolePrivilegeDTO dto)
        {

            try
            {
                if (dto == null)
                {
                    return BadRequest();
                }

                RolePrivilege rolePrivilege = _mapper.Map<RolePrivilege>(dto);
                rolePrivilege.IsDeleted = false;
                rolePrivilege.CreatedDate = DateTime.Now;
                rolePrivilege.ModifiedDate = DateTime.Now;

                var result = await _rolePrivilegeRepository.CreateAsync(rolePrivilege);

                dto.Id = result.Id;
                _apiResponse.Data = dto;
                _apiResponse.Status = true;
                _apiResponse.StatusCode = HttpStatusCode.OK;

                return Ok(_apiResponse);
                //return CreatedAtRoute("GetRolePrivilegeById", new { id = dto.Id }, dto);
            }
            catch (Exception ex)
            {
                _apiResponse.Status = false;
                _apiResponse.StatusCode = HttpStatusCode.InternalServerError;
                _apiResponse.Error.Add(ex.Message);
                return _apiResponse;
            }

        }
    }
}
