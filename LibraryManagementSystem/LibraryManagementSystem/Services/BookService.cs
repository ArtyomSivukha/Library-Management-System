using LibraryManagementSystem.Models;

namespace LibraryManagementSystem.Services;

public class BookService
{
    private static readonly List<Book> _books = new()
    {
        new Book{Id = 1, Title = "Ноутбук", PublisherYear = 50000, AuthorId = 1}
    };
    
    public IEnumerable<Book> GetAllBooks() => _books;
    public Book? GetBookById(long id) => _books.FirstOrDefault(p => p.Id == id);

    public void DeleteBook(long id)
    {
        var bookToDelete = _books.FirstOrDefault(p => p.Id == id);
        if (bookToDelete is not null)
        {
            _books.Remove(bookToDelete);
        }
    }
}