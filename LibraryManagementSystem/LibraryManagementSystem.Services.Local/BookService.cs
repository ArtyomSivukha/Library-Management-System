using LibraryManagementSystem.Services.Models;

namespace LibraryManagementSystem.Services.Local;

public class BookService : IBookService
{
    private static readonly List<Book> Books = new();
    private static long _bookId;

    private readonly AuthorService _authorService;

    public BookService(AuthorService authorService)
    {
        _authorService = authorService;
    }

    private long GetNextBookId()
    {
        return ++_bookId;
    }

    public IEnumerable<Book> GetAllBooks() => Books;
    public Book? GetBookById(long id) => Books.FirstOrDefault(p => p.Id == id);
    
    public Book CreateBook(Book book)
    {
        if (_authorService.GetAuthorById(book.AuthorId) is null)
        {
            throw new ArgumentException($"Author with id {book.AuthorId} does not exist");
        }

        book.Id = GetNextBookId();
        Books.Add(book);
        return book;
    }

    public void UpdateBook(Book book)
    {
        if (_authorService.GetAuthorById(book.AuthorId) is null)
        {
            throw new ArgumentException($"Author with id {book.AuthorId} does not exist");
        }

        var bookToUpdate = GetBookById(book.Id);
        if (bookToUpdate is null)
        {
            throw new ArgumentException($"Book with ID {book.Id} not found");
        }

        bookToUpdate.Title = book.Title;
        bookToUpdate.PublisherYear = book.PublisherYear;
        bookToUpdate.AuthorId = book.AuthorId;
    }
    
    public void DeleteBook(long id)
    {
        var bookToDelete = GetBookById(id);
        if (bookToDelete is null)
        {
            throw new ArgumentException("Book not found");
        }

        Books.Remove(bookToDelete);
    }

}