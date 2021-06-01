using Books.Data.Services;
using Books.Data.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace Books.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class AuthorsController : ControllerBase
    {
        private AuthorsService _authorService;

        public AuthorsController(AuthorsService authorService)
        {
            _authorService = authorService;
        }

         [HttpPost]
        [Route("addAuthor")]
        public IActionResult AddAuthor([FromBody] AuthorVM author)
        {
            _authorService.AddAuthor(author);
            return Ok();
        }

        [HttpGet("id")]
        [Route("getAuthorWId")]
        public IActionResult GetAuthorsWithBooks([FromQuery] int id)
        {
            var response = _authorService.GetAuthorWithBooks(id);
            return Ok(response);
        }



        [HttpDelete]
        [Route("Delete")]
        public IActionResult DeleteAuthorById (int id)
        {
            _authorService.DeleteAuthorById(id);
            return NoContent();
        }

    }
}