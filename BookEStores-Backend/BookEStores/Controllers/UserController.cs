using BookEStores;
using BookStore.Models.Model;
using BookStore.Models.Models;
using BookStore.Repository;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Net;

namespace BookEStore.Controllers
{
    [Route("api/user")]
    [ApiController]
    public class UserController : ControllerBase
    {
        UserRepository _repository = new UserRepository();
        DemoAES obj = new DemoAES();
        [HttpGet]
        [Route("list")]
        public IActionResult GetUsers(int pageIndex = 1, int pageSize = 10, string keyword = "")
        {
            try
            {
                var users = _repository.GetUsers(pageIndex, pageSize, keyword);
                ListResponse<UserModel> listResponse = new ListResponse<UserModel>()
                {
                    records = users.records.Select(c => new UserModel(c)),
                    totalRecords = users.totalRecords,
                };
                if (users == null)
                    return StatusCode(HttpStatusCode.NotFound.GetHashCode(), "please provide correct information..");
                ListResponse<UserModel> userList = new ListResponse<UserModel>()
                {
                    records = users.records.Select(c => new UserModel(c)),
                    totalRecords = users.totalRecords,
                };

                return StatusCode(HttpStatusCode.OK.GetHashCode(), userList);

            }
            catch (Exception ex)
            {
                return StatusCode(HttpStatusCode.InternalServerError.GetHashCode(), ex.Message);
            }
        }

        [HttpGet]
        [Route("{id}")]
        public IActionResult GetUser(int id)
        {
            try
            {
                var user = _repository.getUser(id);

                if (user == null)
                {
                    return StatusCode(HttpStatusCode.NotFound.GetHashCode(), "Please provide correct information");

                }
                return StatusCode(HttpStatusCode.OK.GetHashCode(), user);
            }
            catch (Exception ex)
            {
                return StatusCode(HttpStatusCode.InternalServerError.GetHashCode(), ex.Message);
            }
        }
        [HttpPut("Update")]
        public IActionResult Update(UserModel model)
        {
            try
            {
                if (model == null)
                {
                    return StatusCode(HttpStatusCode.BadRequest.GetHashCode(), "Please insert details properly!");
                }
                User upuser = new User()
                {
                    Id = model.id,
                    Firstname = model.firstName,
                    Lastname = model.lastName,
                    Email = model.email,
                    Roleid = model.roleId,
                    Password = obj.ComputeMD5Hash(model.password),
                };

                var isSaved = _repository.updateUser(upuser);
                if (isSaved == null)
                {
                    return StatusCode(HttpStatusCode.NotFound.GetHashCode(), "User Not Found");
                }
                UserModel updatedUser = new UserModel(upuser);
                return StatusCode(HttpStatusCode.OK.GetHashCode(), upuser);
            }
            catch (Exception ex)
            {
                return StatusCode(HttpStatusCode.InternalServerError.GetHashCode(), ex.Message);
            }
        }
 
        [HttpDelete("delete/{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                if (id > 0)
                {
                    var user = _repository.getUser(id);
                    if (user == null)
                        return StatusCode(HttpStatusCode.NotFound.GetHashCode(), "User Not Found");
                    var isDeleted = _repository.deleteUser(user);
                    if (isDeleted)
                    {
                        return StatusCode(HttpStatusCode.OK.GetHashCode(), "User detail deleted successfully");
                    }
                }
                return StatusCode(HttpStatusCode.NotFound.GetHashCode(), "Please provide correct information");
            }
            catch (Exception ex)
            {
                return StatusCode(HttpStatusCode.InternalServerError.GetHashCode(), ex.Message);
            }
        }
    }
}
