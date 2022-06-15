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
    [Route("api/Book")]
    public class BookController : Controller
    {
        BookRepository _bookrepository = new BookRepository();
        [Route("list")]
        [HttpGet]
        [ProducesResponseType(typeof(ListResponse<BookModel>), (int)HttpStatusCode.OK)]

        public IActionResult GetBooks(int pageIndex = 1, int pageSize = 10, string? keyword = "")
        {
            try
            {
                var books = _bookrepository.GetBooks(pageIndex, pageSize, keyword);
                ListResponse<BookModel> listResponse = new ListResponse<BookModel>()
                {
                    records = books.records.Select(c => new BookModel(c)),
                    totalRecords = books.totalRecords,
                };
                return StatusCode(HttpStatusCode.OK.GetHashCode(), listResponse);
            }
            catch (Exception ex)
            {
                return StatusCode(HttpStatusCode.InternalServerError.GetHashCode(), ex.Message);
            }
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(BookModel), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(BookModel), (int)HttpStatusCode.NotFound)]

        public IActionResult GetBook(int id)
        {
            try
            {
                if (id > 0)
                {
                    var books = _bookrepository.GetBook(id);
                    BookModel bookModel = new BookModel(books);
                    return StatusCode(HttpStatusCode.OK.GetHashCode(), bookModel);
                }

                return StatusCode(HttpStatusCode.NotFound.GetHashCode(), "Provide right Id..");
            }
            catch (Exception ex)
            {

                return StatusCode(HttpStatusCode.InternalServerError.GetHashCode(), ex.Message);
            }
        }

        [HttpPost("add")]
        [ProducesResponseType(typeof(BookModel), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(BadRequestObjectResult), (int)HttpStatusCode.BadRequest)]

        public IActionResult AddBook(BookModel model)
        {
            try
            {
                if (model == null)
                    return StatusCode(HttpStatusCode.BadRequest.GetHashCode(), "Model is null");
                Book book = new Book()
                {
                    Id = model.id,
                    Name = model.name,
                    Price = model.price,
                    Description = model.description,
                    Categoryid = model.categoryId,
                    Publisherid = model.publisherId,
                    Quantity = model.quantity,
                    Base64image=model.base64image
                };
                var response = _bookrepository.AddBook(book);
                BookModel bookModel = new BookModel(response);

                return StatusCode(HttpStatusCode.OK.GetHashCode(), bookModel);
            }
            catch (Exception ex)
            {

                return StatusCode(HttpStatusCode.InternalServerError.GetHashCode(), ex.Message);
            }
        }

        [HttpPut("update")]
        [ProducesResponseType(typeof(BookModel), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(BadRequestObjectResult), (int)HttpStatusCode.BadRequest)]
        public IActionResult UpdateBook(BookModel model)
        {
            try
            {
                if (model == null)
                    return StatusCode(HttpStatusCode.BadRequest.GetHashCode(), "Model is null");
                Book book = new Book()
                {
                    Id = model.id,
                    Name = model.name,
                    Price = model.price,
                    Description = model.description,
                    Categoryid = model.categoryId,
                    Publisherid = model.publisherId,
                    Quantity = model.quantity,
                    Base64image = model.base64image
                };
                var response = _bookrepository.UpdateBook(book);
                BookModel bookModel = new BookModel(response);

                return StatusCode(HttpStatusCode.OK.GetHashCode(), bookModel);
            }
            catch (Exception ex)
            {

                return StatusCode(HttpStatusCode.InternalServerError.GetHashCode(), ex.Message);
            }
        }

        [HttpDelete("delete/{id}")]
        [ProducesResponseType(typeof(bool), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(BadRequestObjectResult), (int)HttpStatusCode.BadRequest)]
        public IActionResult DeleteBook(int id)
        {
            try
            {
                if (id == 0)
                    return StatusCode(HttpStatusCode.BadRequest.GetHashCode(), "id is null");
                if (id > 0)
                {
                    var response = _bookrepository.DeleteBook(id);
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
