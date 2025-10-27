using LibraryManagementSystem.Services.Models;

namespace LibraryManagementSystem.Services.EntityFramework;

public class BookService : IBookService
{
    public Task<IEnumerable<Book>> GetAllBooksAsync()
    {
        throw new NotImplementedException();
    }

    public Task<Book?> GetBookByIdAsync(long id)
    {
        throw new NotImplementedException();
    }

    public Task<Book> CreateBookAsync(Book book)
    {
        throw new NotImplementedException();
    }

    public Task UpdateBookAsync(Book book)
    {
        throw new NotImplementedException();
    }

    public Task DeleteBookAsync(long id)
    {
        throw new NotImplementedException();
    }
}