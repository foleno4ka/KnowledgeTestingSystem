using System.Collections.Generic;
using System.Web.Http;
using System.Web.Http.Cors;
using KnowledgeControlSystem.BLL.DTOs;
using KnowledgeControlSystem.BLL.Interfaces;

namespace KnowledgeControlSystem.WebAPI.Controllers
{
    [RoutePrefix("api/Categories")]
    [Authorize]
    public class CategoriesController : ApiController
    {
        private readonly IService<CategoryDTO> _categoriesService;

        public CategoriesController(IService<CategoryDTO> categoryService)
        {
            _categoriesService = categoryService;
        }

        [Route("")]
        [HttpGet]
        public IEnumerable<CategoryDTO> GetCategories()
        {
            return _categoriesService.GetAll();
        }

        [Route("{id}")]
        [HttpGet]
        public IHttpActionResult GetCategory(int id)
        {
            CategoryDTO category = _categoriesService.Get(id);
            if (category == null)
                return NotFound();
            return Ok(category);
        }

        //[Route("")]
        ////[Authorise(Roles="Admin")]
        //[HttpPut]
        //public IHttpActionResult PutCategory(int id, CategoryDTO changedCategory)
        //{
        //    CategoryDTO category = _categoriesService.Get(id);
        //    if (category == null)
        //        return NotFound();
        //    _categoriesService.Update(changedCategory);
        //    return StatusCode(HttpStatusCode.NoContent);
        //}
        //[Route("")]
        ////[Authorise(Roles="Admin")]
        //[HttpPost]
        //public IHttpActionResult PostCategory(CategoryDTO newCategory)
        //{
        //    if (!ModelState.IsValid)
        //        return NotFound();
        //    _categoriesService.Create(newCategory);
        //    return StatusCode(HttpStatusCode.NoContent);
        //}
        //[Route("")]
        ////[Authorise(Roles="Admin")]
        //[HttpDelete]
        //public IHttpActionResult DeleteCategory(CategoryDTO category)
        //{
        //    if (!ModelState.IsValid || _categoriesService.Get(category.CategoryId) == null)
        //        return NotFound();
        //    _categoriesService.Delete(category);
        //    return StatusCode(HttpStatusCode.NoContent);
        //}
    }
}