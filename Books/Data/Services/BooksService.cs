using System;
using System.Collections.Generic;
using System.Linq;
using Books.Data;
using Books.Data.Models;
using Books.Data.ViewModels;

namespace Books
{
    public class BooksService
    {
        public AppDbContext _context { get; }
        public BooksService(AppDbContext context)
        {
            _context = context;

        }

        public void AddBookWithAuthors (BookVM book)
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
                DateAdded = DateTime.Now,
                PublisherId = book.PublisherId,
            };

            _context.Books.Add(_book);
            _context.SaveChanges();

            foreach(var id in book.AuthorIds)
            {
                var _book_author = new Book_Author()
                {
                    BookId = _book.Id,
                    AuthorId = id
                };
                _context.Books_Authors.Add(_book_author);
                _context.SaveChanges();
            }
        }

        public List<Book> GetAll()
        {
            return _context.Books.ToList();
        }

        public BookWithAuthorsVM GetBookById(int id)
        {
            var _bookWithAuthors = _context.Books.Where(n => n.Id == id).Select(book => new BookWithAuthorsVM() {
                Title = book.Title,
                Description = book.Description,
                IsRead = book.IsRead,
                Rate = book.IsRead ? book.Rate : null,
                DateRead = book.IsRead ? book.DateRead : null,
                Genre = book.Genre,
                CoverUrl = book.CoverUrl,
                PublisherName = book.Publisher.Name,
                AuthorNames = book.Book_Authors.Select(n => n.Author.FullName).ToList()

            }).FirstOrDefault();

            return _bookWithAuthors;
        }

        public void DeleteBookbyId(int id)
        {
            var book = GetBookById(id);

            if (book != null)
            {
                //_context.Books.Remove(book);
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