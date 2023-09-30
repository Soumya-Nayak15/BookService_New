using BookService.Data;
using BookService.Data.Models;
using BookService.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace BookService.Services
{
    public interface IBookService
    {
        List<Book> GetBooks();
        int Save(Book book);

        Book? Update(Book book);

        Book? Get(int id);
    }
    public class BookService : IBookService
    {
        public List<Book> GetBooks()
        {
            using var context = new ApiContext();
            var books = context.Books.ToList();
            return books;
        }

        public int Save(Book book)
        {
            using var context = new ApiContext();
            var addedBook = context.Books.Add(book);
            context.SaveChanges();
            return addedBook.Entity.Id;
        }

        public Book? Update(Book book)
        {
           
            if (book != null)
            {
                using var context = new ApiContext();
                context.Books.Attach(book);
                var entry = context.Entry(book);
                if (book.Name != null)
                {
                    entry.Entity.Name = book.Name;
                    entry.Property(e => e.Name).IsModified = true;

                }

                if (book.Description != null)
                {
                    entry.Entity.Description = book.Description;
                    entry.Property(e => e.Description).IsModified = true;

                }

                if (book.Category != null)
                {
                    entry.Entity.Category = book.Category;
                    entry.Property(e => e.Category).IsModified = true;

                }

                if (book.Author != null)
                {
                    entry.Entity.Author = book.Author;
                    entry.Property(e => e.Author).IsModified = true;

                }

                context.SaveChanges();

                return this.Get(book.Id);

            }

            return null;
        }

        public Book? Get(int id)
        {
            using var context = new ApiContext();
            return context.Books.Find(id);
        }
    }
}
