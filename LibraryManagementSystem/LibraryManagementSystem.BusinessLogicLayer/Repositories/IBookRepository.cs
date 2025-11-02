using LibraryManagementSystem.BusinessLogicLayer.Models;

namespace LibraryManagementSystem.BusinessLogicLayer.Repositories;

public interface IBookRepository
{
    Task<IEnumerable<Book>> GetAllAsync();
    Task<Book?> GetByIdAsync(Guid id);
    Task<Book> CreateAsync(Book book);
    Task UpdateAsync(Book book);
    Task DeleteAsync(Guid id);
    Task<IEnumerable<Book>> GetBooksPublishedAfterAsync(int year);
}

