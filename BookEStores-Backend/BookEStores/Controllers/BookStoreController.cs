using BookEStores;
using BookStore.Models.Model;
using BookStore.Models.Models;
using BookStore.Repository;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Net;

namespace BookEStore.Controllers
{
    [Route("api/public")]
    [ApiController]
    public class BookStoreController : ControllerBase
    {
        UserRepository _repository = new UserRepository();
        DemoAES obj = new DemoAES();
  
        [HttpPost]
        [Route("Login")]
        public IActionResult Login(LoginModel model)
        {
            try
            {
                User user = new User()
                {
                    Email = model.Email,
                    Password = obj.ComputeMD5Hash(model.Password)
                };
                User Response = _repository.Login(user);
                if (Response == null)
                    return StatusCode(HttpStatusCode.NotFound.GetHashCode(), "User not found");
                //return Ok(user);
                UserModel user1 = new UserModel(Response);
                return StatusCode(HttpStatusCode.OK.GetHashCode(), user1);

            }
            catch (Exception ex)
            {

                return StatusCode(HttpStatusCode.InternalServerError.GetHashCode(), ex.Message);
            }
        }

        [HttpPost]
        [Route("Register")]
        public IActionResult Register(RegisterModel model)
        {
            try
            {
                
                if (model == null)
                    return StatusCode(HttpStatusCode.BadRequest.GetHashCode(), "Bad request");
                User userRegister = new User()
                {
                    Firstname = model.Firstname,
                    Lastname = model.Lastname,
                    Email = model.Email,
                    Password = obj.ComputeMD5Hash(model.Password),
                    Roleid = model.Roleid,
                };
                var user = _repository.Register(userRegister);
                UserModel user1 = new UserModel(user);
                return StatusCode(HttpStatusCode.OK.GetHashCode(), user1);
            }
            catch (Exception ex)
            {

                return StatusCode(HttpStatusCode.InternalServerError.GetHashCode(), ex.Message);
            }
        }

    }
}
