using LibraryManagementSystem.Services.Models;

namespace LibraryManagementSystem.Services;

public interface IBookService
{
    IEnumerable<Book> GetAllBooks();
    Book? GetBookById(long id);
    void DeleteBook(long id);
    Book CreateBook(Book book);
    void UpdateBook(Book book);
}