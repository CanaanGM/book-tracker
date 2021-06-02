using Books.Data.Models;
using Books.Data.ViewModels;
using System;
using System.Linq;

namespace Books.Data.Services
{
    public class AuthorsService
    {
        
        public AppDbContext _context { get; }
        public AuthorsService(AppDbContext context)
        {
            _context = context;

        }

        public void AddAuthor (AuthorVM author)
        {
            var _author = new Author()
            {
                FullName = author.FullName
            };

            _context.Authors.Add(_author);
            _context.SaveChanges();
        }

        public AuthorWithBooksVM GetAuthorWithBooks(int authorId)
        {
            var _author = _context.Authors.Where(n => n.Id == authorId).Select(n => new AuthorWithBooksVM()
            {
                FullName = n.FullName,
                BookTitles = n.Book_Authors.Select(n => n.Book.Title).ToList()
            }).FirstOrDefault();

            return _author;
        }

        public void DeleteAuthorById(int id)
        {
            var _author = _context.Authors.FirstOrDefault(n => n.Id == id);
            if(_author != null)
            {
                _context.Authors.Remove(_author);
                _context.SaveChanges();

            }
        }
    }
}