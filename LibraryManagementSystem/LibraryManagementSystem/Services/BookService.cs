using LibraryManagementSystem.Models;

namespace LibraryManagementSystem.Services;

public class BookService
{
    private List<Book> _books = new()
    {
        new Book{Id = 1, Title = "Ноутбук", PublisherYear = 50000}
    };
    
    public List<Book> GetAllBooks() => _books;
    public Book GetBookById(int id) => _books.FirstOrDefault(p => p.Id == id);
}