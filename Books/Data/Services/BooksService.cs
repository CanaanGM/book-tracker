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

        public Book GetBookById(int id)
        {
            var book = _context.Books.FirstOrDefault(b => b.Id == id);
            if (book != null)
                return book;
            return null;
        }

        public void DeleteBookbyId(int id)
        {
            var book = GetBookById(id);

            if (book != null)
            {
                _context.Books.Remove(book);
                _context.SaveChanges();
            }
        }

        public Book UpdateBookById(int id, BookVM book)
        {
            var _book = _context.Books.FirstOrDefault(b => b.Id == id);
            if(book != null)
            {
                _book.Title = book.Title;
                _book.Description = book.Description;
                _book.IsRead = book.IsRead;
                _book.Rate = book.IsRead ? book.Rate : null;
                _book.DateRead = book.IsRead ? book.DateRead : null;
                _book.Genre = book.Genre;
                _book.CoverUrl = book.CoverUrl;
            _context.SaveChanges();
            }
            return _book;
        }

    }
}