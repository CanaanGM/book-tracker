using System;
using System.Linq;
using Books.Data.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace Books.Data
{
    public class AppDbInitilizer
    {
        public static void Seed(IApplicationBuilder applicationBuilder){
            using (var serviceScope = applicationBuilder.ApplicationServices.CreateScope())
            {
                var context  = serviceScope.ServiceProvider.GetService<AppDbContext>();

                if(!context.Books.Any())
                {
                    context.Books.AddRange(new Book() 
                    {
                       Title= "Horus Rising",
                       DateAdded = DateTime.Now,
                       DateRead = DateTime.Now.AddYears(- 6),
                       Description = "First book in the Horus heresy series, it sets the tone for the rest of it. \n it is Awesome~!",
                       Genre ="Science Fiction",
                       IsRead = true,
                       Rate = 5,
                       CoverUrl = "https://wh40k.lexicanum.com/mediawiki/images/d/d0/Horusrising.jpg",
                       

                    },new Book() 
                    {
                       Title= "c# in depth",
                       Description = "C# book that goes in depth up untill c# 8",
                       DateAdded = DateTime.Now,
                       Genre ="Computer Science",
                       IsRead = false,
                       CoverUrl = "https://csharpindepth.com/images/Cover.png" 

                    });

                    context.SaveChanges();
                }
            }
        }
    }
}