using SecureTeamSimulator.Core.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SecureTeamSimulator.Application.Services.Interfaces
{
    public interface IBookService
    {
        Task<IEnumerable<Book>> GetAllBooksAsync();
        Task<Book> GetBookByIdAsync(Guid id);
        Task AddBookAsync(Book book);
        Task UpdateBookAsync(Book book);
        Task DeleteBookAsync(Guid id);
    }
}