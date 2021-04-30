using Books.Data.Models;
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
        public IActionResult AddBook([FromBody] BookVM book)
        {
            _booksService.AddBook(book);
            return Ok();
        }

        [HttpGet]
        public IEnumerable<Book> GetAll()
        {
          return  _booksService.GetAll().ToList().OrderBy(s => s.Id);
        }
    }
}