using Books.Data.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Books
{
    [Route("[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        public BooksService _booksService;
        public BooksController(BooksService booksService)
        {
            _booksService = booksService;
        }
        
        [HttpPost]
        [Route("addBook")]
        public IActionResult AddBook([FromBody] BookVM book)
        {
            _booksService.AddBookWithAuthors(book);
            return Ok();
        }

        [HttpGet]
        [Route("getAll")]
        public IActionResult GetAll()
        {
          return Ok( _booksService.GetAll().ToList().OrderBy(s => s.Id) );
        }

        [HttpGet("id")]
        [Route("getById")]
        public IActionResult GetBookById([FromQuery] int id)
        {
            var book = _booksService.GetBookById(id);
            return  book != null ? Ok( book ) : NotFound();
            
        }

        [HttpPut("id ,book")]
        [Route("updateBookById")]
        public IActionResult UpdateBookById ( [FromQuery] int id, [FromBody] BookVM book)
        {

            return Ok(_booksService.UpdateBookById(id, book));

        }


        [HttpDelete("id")]
        [Route("delete")]
        public IActionResult DeleteBookById([FromQuery] int id)
        {
            _booksService.DeleteBookbyId(id);
               return Ok();
            
        }
    }
}