using System;
using System.Collections.Generic;
using System.Linq;
using Books.Data;
using Books.Data.Models;

namespace Books
{
    public class BooksService
    {
        public AppDbContext _context { get; }
        public BooksService(AppDbContext context)
        {
            _context = context;

        }

        public void AddBook (BookVM book)
        {
            var _book = new Book()
            {
                Title = book.Title,
                Description = book.Description,
                IsRead = book.IsRead,
                Rate = book.IsRead ? book.Rate : null,
                DateRead = book.IsRead? book.DateRead : null,
                Genre = book.Genre,
                CoverUrl = book.CoverUrl,
                DateAdded = DateTime.Now
            };

            _context.Books.Add(_book);
            _context.SaveChanges();
        }

        public List<Book> GetAll()
        {
            return _context.Books.ToList();
        }
    }
}