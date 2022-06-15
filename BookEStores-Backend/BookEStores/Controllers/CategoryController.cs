using BookStore.Models.Model;
using BookStore.Models.Models;
using BookStore.Repository;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;

namespace BookEStore.Controllers
{
    [ApiController]
    [Route("api/category")]
    public class CategoryController : ControllerBase
    {
        CategoryRepository _categoryrepository = new CategoryRepository();
        [Route("list")]
        [HttpGet]
        [ProducesResponseType(typeof(ListResponse<CategoryModel>),(int)HttpStatusCode.OK)]
    
        public IActionResult GetCategorie(int pageIndex = 1, int pageSize = 10, string? keyword = "")
        {
            try
            {
                var categories = _categoryrepository.GetCategories(pageIndex, pageSize, keyword);
                ListResponse<CategoryModel> listResponse = new ListResponse<CategoryModel>()
                {
                    records = categories.records.Select(c => new CategoryModel(c)),
                    totalRecords = categories.totalRecords,
                };
                return StatusCode(HttpStatusCode.OK.GetHashCode(), listResponse);
            }
            catch (Exception ex)
            {
                return StatusCode(HttpStatusCode.InternalServerError.GetHashCode(), ex.Message);
            }
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(CategoryModel), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(CategoryModel), (int)HttpStatusCode.NotFound)]

        public IActionResult GetCategory(int id)
        {
            try
            {
                if (id>0)
                {
                    var categories = _categoryrepository.GetCategory(id);
                    CategoryModel categoryModel = new CategoryModel(categories);
                    return StatusCode(HttpStatusCode.OK.GetHashCode(), categoryModel);
                }

                return StatusCode(HttpStatusCode.NotFound.GetHashCode(), "Provide right Id..");
            }
            catch (Exception ex)
            {

                return StatusCode(HttpStatusCode.InternalServerError.GetHashCode(), ex.Message);
            }
        }

        [HttpPost("add")]
        [ProducesResponseType(typeof(CategoryModel), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(BadRequestObjectResult), (int)HttpStatusCode.BadRequest)]

        public IActionResult AddCategory(CategoryModel model)
        {
            try
            {
                if (model == null)
                    return StatusCode(HttpStatusCode.BadRequest.GetHashCode(), "Model is null");
                Category category = new Category()
                {
                    Id = model.Id,
                    Name = model.Name,
                };
                var response = _categoryrepository.AddCategory(category);
                CategoryModel categoryModel = new CategoryModel(response);

                return StatusCode(HttpStatusCode.OK.GetHashCode(), categoryModel);
            }
            catch (Exception ex)
            {

                return StatusCode(HttpStatusCode.InternalServerError.GetHashCode(), ex.Message);
            }
        }

        [HttpPut("update")]
        [ProducesResponseType(typeof(CategoryModel), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(BadRequestObjectResult), (int)HttpStatusCode.BadRequest)]
        public IActionResult UpdateCategory(CategoryModel model)
        {
            try
            {
                if (model == null)
                    return StatusCode(HttpStatusCode.BadRequest.GetHashCode(), "Model is null");
                Category category = new Category()
                {
                    Id = model.Id,
                    Name = model.Name
                };
                var response = _categoryrepository.UpdateCategory(category);
                CategoryModel categoryModel = new CategoryModel(response);

                return StatusCode(HttpStatusCode.OK.GetHashCode(), categoryModel);
            }
            catch (Exception ex)
            {

                return StatusCode(HttpStatusCode.InternalServerError.GetHashCode(), ex.Message);
            }
        }

        [HttpDelete("delete")]
        [ProducesResponseType(typeof(bool), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(BadRequestObjectResult), (int)HttpStatusCode.BadRequest)]
        public IActionResult DeleteCategory(int id)
        {
            try
            {
                if (id == 0)
                    return StatusCode(HttpStatusCode.BadRequest.GetHashCode(), "id is null");
                if (id>0)
                {
                    var response = _categoryrepository.DeleteCategory(id);
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
