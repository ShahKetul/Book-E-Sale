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
    [Route("api/Publisher")]
    public class PublisherController : ControllerBase
    {
        PublisherRepository _publisherrepository = new PublisherRepository();
        [Route("list")]
        [HttpGet]
        [ProducesResponseType(typeof(ListResponse<PublisherModel>), (int)HttpStatusCode.OK)]

        public IActionResult GetPublishers(int pageIndex = 1, int pageSize = 10, string? keyword = "")
        {
            try
            {
                var pubishers = _publisherrepository.GetPublishers(pageIndex, pageSize, keyword);
                ListResponse<PublisherModel> listResponse = new ListResponse<PublisherModel>()
                {
                    records = pubishers.records.Select(p => new PublisherModel(p)),
                    totalRecords = pubishers.totalRecords,
                };
                return StatusCode(HttpStatusCode.OK.GetHashCode(), listResponse);
            }
            catch (Exception ex)
            {
                return StatusCode(HttpStatusCode.InternalServerError.GetHashCode(), ex.Message);
            }
        }


        [HttpGet("{id}")]
        [ProducesResponseType(typeof(PublisherModel), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(PublisherModel), (int)HttpStatusCode.NotFound)]

        public IActionResult GetPublisher(int id)
        {
            try
            {
                if (id > 0)
                {
                    var publishers = _publisherrepository.GetPublisher(id);
                    PublisherModel publisherModel = new PublisherModel(publishers);
                    return StatusCode(HttpStatusCode.OK.GetHashCode(), publisherModel);
                }

                return StatusCode(HttpStatusCode.NotFound.GetHashCode(), "Provide right Id..");
            }
            catch (Exception ex)
            {

                return StatusCode(HttpStatusCode.InternalServerError.GetHashCode(), ex.Message);
            }
        }

        [HttpPost("add")]
        [ProducesResponseType(typeof(PublisherModel), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(BadRequestObjectResult), (int)HttpStatusCode.BadRequest)]

        public IActionResult AddPublisher(PublisherModel model)
        {
            try
            {
                if (model == null)
                    return StatusCode(HttpStatusCode.BadRequest.GetHashCode(), "Model is null");
                Publisher pub = new Publisher()
                {
                    Id = model.Id,
                    Name = model.Name,
                    Address = model.Address,
                    Contact = model.Contact
                };
                var response = _publisherrepository.AddPublisher(pub);
                PublisherModel categoryModel = new PublisherModel(response);

                return StatusCode(HttpStatusCode.OK.GetHashCode(), categoryModel);
            }
            catch (Exception ex)
            {

                return StatusCode(HttpStatusCode.InternalServerError.GetHashCode(), ex.Message);
            }
        }

        [HttpPut("update")]
        [ProducesResponseType(typeof(bool), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(BadRequestObjectResult), (int)HttpStatusCode.BadRequest)]
        public IActionResult UpdatePublisher(PublisherModel model)
        {
            try
            {
                if (model != null)
                {
                    var isSaved = _publisherrepository.updateUser(model);
                    if (isSaved)
                    {
                        return StatusCode(HttpStatusCode.OK.GetHashCode(), "User detail updated successfully");
                    }
                    else
                    {
                        return StatusCode(HttpStatusCode.NotFound.GetHashCode(), "User Not Found");
                    }
                }
                return StatusCode(HttpStatusCode.NotFound.GetHashCode(), "Please provide correct information");
            }
            catch (Exception ex)
            {

                return StatusCode(HttpStatusCode.InternalServerError.GetHashCode(), ex.Message);
            }
        }

        [HttpDelete("delete")]
        [ProducesResponseType(typeof(bool), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(BadRequestObjectResult), (int)HttpStatusCode.BadRequest)]
        public IActionResult DeletePublisher(int id)
        {
            try
            {
                if (id == 0)
                    return StatusCode(HttpStatusCode.BadRequest.GetHashCode(), "id is null");
                if (id > 0)
                {
                    var response = _publisherrepository.deletPublisher(id);
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
