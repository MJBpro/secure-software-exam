using SecureTeamSimulator.Application.Services.Interfaces;
using SecureTeamSimulator.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SecureTeamSimulator.Application.Services
{
    public class BookService : IBookService
    {
        // In-memory storage for simplicity. Replace with actual database context.
        private readonly List<Book> _books = new List<Book>();

        public Task<IEnumerable<Book>> GetAllBooksAsync()
        {
            return Task.FromResult<IEnumerable<Book>>(_books);
        }

        public Task<Book> GetBookByIdAsync(Guid id)
        {
            var book = _books.SingleOrDefault(b => b.Id == id);
            return Task.FromResult(book);
        }

        public Task AddBookAsync(Book book)
        {
            book.Id = Guid.NewGuid();
            _books.Add(book);
            return Task.CompletedTask;
        }

        public Task UpdateBookAsync(Book book)
        {
            var existingBook = _books.SingleOrDefault(b => b.Id == book.Id);
            if (existingBook != null)
            {
                existingBook.Title = book.Title;
                existingBook.Author = book.Author;
                existingBook.PublishedDate = book.PublishedDate;
                existingBook.ISBN = book.ISBN;
            }
            return Task.CompletedTask;
        }

        public Task DeleteBookAsync(Guid id)
        {
            var book = _books.SingleOrDefault(b => b.Id == id);
            if (book != null)
            {
                _books.Remove(book);
            }
            return Task.CompletedTask;
        }
    }
}