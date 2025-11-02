using LibraryManagementSystem.BusinessLogicLayer.Models;

namespace LibraryManagementSystem.BusinessLogicLayer.Services;

public interface IBookService
{
    Task<IEnumerable<Book>> GetAllBooksAsync();
    Task<Book> GetBookByIdAsync(Guid id);
    Task<Book> CreateBookAsync(Book book);
    Task UpdateBookAsync(Book book);
    Task DeleteBookAsync(Guid id);
    Task<IEnumerable<Book>> GetBooksPublishedAfterAsync(int year);
}