using Books.Data.Models;
using Books.Data.Paging;
using Books.Data.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Books.Data.Services
{
    public class PublishersService
    {
        
        public AppDbContext _context { get; }
        public PublishersService(AppDbContext context)
        {
            _context = context;

        }

        public Publisher AddPublisher (PublisherVM publisher)
        {
            var _publisher = new Publisher()
            {
                Name = publisher.Name,

            };

            _context.Publishers.Add(_publisher);
            _context.SaveChanges();

            return _publisher;
        }

        public List<Publisher> GetAll(string sortBy, string searchString, int? pageNumber)
        {
            var allPublishers = _context.Publishers.OrderBy(n => n.Name).ToList();

            // sorting
            if (!string.IsNullOrEmpty(sortBy))
            {
                switch (sortBy)
                {
                    case "name_desc":
                        allPublishers = allPublishers.OrderByDescending(n => n.Name).ToList();
                        break;
                    default:
                        break;
                }
            }
            // filtering
            if (!string.IsNullOrEmpty(searchString))
            {
                allPublishers = allPublishers.Where(n => n.Name.Contains(searchString, StringComparison.CurrentCultureIgnoreCase)).ToList();
            }

            //paging

            int pageSize = 5;
            allPublishers = PaginatedList<Publisher>.Create(allPublishers.AsQueryable(), pageNumber ?? 1, pageSize);
            return allPublishers;
        }


        public PublisherWithBooksAndAuthorsVM GetPublisherData(int publisherId)
        {
            var _publisherData = _context.Publishers.Where(n => n.Id == publisherId)
                .Select(n => new PublisherWithBooksAndAuthorsVM()
                {
                    Name = n.Name,
                    BookAuthors= n.Books.Select(
                        n => new BookAuthorVM() { 
                        BookAuthor = n.Title,
                        BookAuthors = n.Book_Authors.Select(n => n.Author.FullName).ToList()
                    }).ToList()
                }).FirstOrDefault();
            return _publisherData;

        }

        public Publisher GetPublisherById(int id)
        {
            return _context.Publishers.FirstOrDefault(n => n.Id == id);
        }

        internal void DeletePublisherById(int id)
        {
            var publ = _context.Publishers.FirstOrDefault(n => n.Id == id);

            if (publ != null)
            {
                _context.Publishers.Remove(publ);
                _context.SaveChanges();
            }
            else
            {
                throw new Exception($"the publisher with the id:{id} doesn't exist...");
            }

        }
    }
}