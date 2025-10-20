using LibraryManagementSystem.Models;

namespace LibraryManagementSystem.Services;

public class BookService
{
    private static readonly List<Book> _books = new();
    private static long _bookId;


    public long GetNextBookId()
    {
        return ++_bookId;
    }

    public IEnumerable<Book> GetAllBooks() => _books;
    public Book? GetBookById(long id) => _books.FirstOrDefault(p => p.Id == id);

    public void DeleteBook(long id)
    {
        var bookToDelete = GetBookById(id);
        if (bookToDelete is not null)
        {
            _books.Remove(bookToDelete);
        }
    }

    public Book CreateBook(Book book)
    {
        book.Id = GetNextBookId();
        _books.Add(book);
        return book;
    }

    public void UpdateBook(Book book)
    {
        var bookToUpdate = GetBookById(book.Id);
        if (bookToUpdate is null)
        {
            throw new ArgumentException($"Author with ID {book.Id} not found");
        }
        bookToUpdate.Title = book.Title;
        bookToUpdate.PublisherYear = book.PublisherYear;
        bookToUpdate.AuthorId = book.AuthorId;
    }
}