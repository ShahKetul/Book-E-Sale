using BookStore.Models.Model;
using BookStore.Models.Models;
using BookStore.Repository;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Net;

namespace BookEStores.Controllers
{
    [ApiController]
    [Route("api/role")]
    public class RoleController : ControllerBase
    {
        RoleRepository _rolerepository = new RoleRepository();
        [Route("list")]
        [HttpGet]
        [ProducesResponseType(typeof(ListResponse<RoleModel>), (int)HttpStatusCode.OK)]

        public IActionResult GetCategorie(int pageIndex = 1, int pageSize = 10, string? keyword = "")
        {
            try
            {
                var roles = _rolerepository.GetRoles(pageIndex, pageSize, keyword);
                ListResponse<RoleModel> listResponse = new ListResponse<RoleModel>()
                {
                    records = roles.records.Select(c => new RoleModel(c)),
                    totalRecords = roles.totalRecords,
                };
                return StatusCode(HttpStatusCode.OK.GetHashCode(), listResponse);
            }
            catch (Exception ex)
            {
                return StatusCode(HttpStatusCode.InternalServerError.GetHashCode(), ex.Message);
            }
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(RoleModel), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(RoleModel), (int)HttpStatusCode.NotFound)]

        public IActionResult GetRole(int id)
        {
            try
            {
                if (id > 0)
                {
                    var roles = _rolerepository.GetRole(id);
                    RoleModel roleModel = new RoleModel(roles);
                    return StatusCode(HttpStatusCode.OK.GetHashCode(), roleModel);
                }

                return StatusCode(HttpStatusCode.NotFound.GetHashCode(), "Provide right Id..");
            }
            catch (Exception ex)
            {

                return StatusCode(HttpStatusCode.InternalServerError.GetHashCode(), ex.Message);
            }
        }

        [HttpPost("add")]
        [ProducesResponseType(typeof(RoleModel), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(BadRequestObjectResult), (int)HttpStatusCode.BadRequest)]

        public IActionResult AddRole(RoleModel model)
        {
            try
            {
                if (model == null)
                    return StatusCode(HttpStatusCode.BadRequest.GetHashCode(), "Model is null");
                Role role = new Role()
                {
                    Id = model.Id,
                    Name = model.Name,
                };
                var response = _rolerepository.AddRole(role);
                RoleModel roleModel = new RoleModel(response);

                return StatusCode(HttpStatusCode.OK.GetHashCode(), roleModel);
            }
            catch (Exception ex)
            {

                return StatusCode(HttpStatusCode.InternalServerError.GetHashCode(), ex.Message);
            }
        }

        [HttpPut("update")]
        [ProducesResponseType(typeof(RoleModel), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(BadRequestObjectResult), (int)HttpStatusCode.BadRequest)]
        public IActionResult UpdateRole(RoleModel model)
        {
            try
            {
                if (model == null)
                    return StatusCode(HttpStatusCode.BadRequest.GetHashCode(), "Model is null");
                Role role = new Role()
                {
                    Id = model.Id,
                    Name = model.Name
                };
                var response = _rolerepository.UpdateRole(role);
                RoleModel roleModel = new RoleModel(response);

                return StatusCode(HttpStatusCode.OK.GetHashCode(), roleModel);
            }
            catch (Exception ex)
            {

                return StatusCode(HttpStatusCode.InternalServerError.GetHashCode(), ex.Message);
            }
        }

        [HttpDelete("delete")]
        [ProducesResponseType(typeof(bool), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(BadRequestObjectResult), (int)HttpStatusCode.BadRequest)]
        public IActionResult DeleteRole(int id)
        {
            try
            {
                if (id == 0)
                    return StatusCode(HttpStatusCode.BadRequest.GetHashCode(), "id is null");
                if (id > 0)
                {
                    var response = _rolerepository.DeleteRole(id);
                    return StatusCode(HttpStatusCode.OK.GetHashCode(), response);
                }

                return StatusCode(HttpStatusCode.NotFound.GetHashCode(), "Provide right id...");

            }
            catch (Exception ex)
            {
                return StatusCode(HttpStatusCode.InternalServerError.GetHashCode(), ex.Message);
            }
        }
    }
}
