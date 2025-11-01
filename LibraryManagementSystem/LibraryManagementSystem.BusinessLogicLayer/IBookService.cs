using LibraryManagementSystem.BusinessLogicLayer.Models;

namespace LibraryManagementSystem.BusinessLogicLayer;

public interface IBookService
{
    Task<IEnumerable<Book>> GetAllBooksAsync();
    Task<Book?> GetBookByIdAsync(long id);
    Task<Book> CreateBookAsync(Book book);
    Task UpdateBookAsync(Book book);
    Task DeleteBookAsync(long id);
    Task<IEnumerable<Book>> GetBooksPublishedAfterAsync(int year);
}