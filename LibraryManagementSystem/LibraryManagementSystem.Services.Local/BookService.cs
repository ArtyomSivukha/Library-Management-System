using LibraryManagementSystem.BusinessLogicLayer;
using LibraryManagementSystem.BusinessLogicLayer.Models;

namespace LibraryManagementSystem.Services.Local;

public class BookService : IBookService
{
    private static readonly List<Book> Books = new();

    private readonly IAuthorService _authorService;

    public BookService(IAuthorService authorService)
    {
        _authorService = authorService;
    }

    private Guid GetNextBookId()
    {
        return Guid.NewGuid();
    }

    public Task<IEnumerable<Book>> GetAllBooksAsync() => Task.FromResult<IEnumerable<Book>>(Books);
    public Task<Book?> GetBookByIdAsync(Guid id) => Task.FromResult(Books.FirstOrDefault(p => p.Id == id));
    
    public async Task<Book> CreateBookAsync(Book book)
    {
        if (await _authorService.GetAuthorByIdAsync(book.AuthorId) is null)
        {
            throw new ArgumentException($"Author with id {book.AuthorId} does not exist");
        }

        book.Id = GetNextBookId();
        Books.Add(book);
        return book;
    }

    public async Task UpdateBookAsync(Book book)
    {
        if (await _authorService.GetAuthorByIdAsync(book.AuthorId) is null)
        {
            throw new ArgumentException($"Author with id {book.AuthorId} does not exist");
        }

        var bookToUpdate = await GetBookByIdAsync(book.Id);
        if (bookToUpdate is null)
        {
            throw new ArgumentException($"Book with ID {book.Id} not found");
        }

        bookToUpdate.Title = book.Title;
        bookToUpdate.PublisherYear = book.PublisherYear;
        bookToUpdate.AuthorId = book.AuthorId;
    }
    
    public async Task DeleteBookAsync(Guid id)
    {
        var bookToDelete = await GetBookByIdAsync(id);
        if (bookToDelete is null)
        {
            throw new ArgumentException("Book not found");
        }

        Books.Remove(bookToDelete);
    }

    public Task<IEnumerable<Book>> GetBooksPublishedAfterAsync(int year)
    {
        throw new NotImplementedException();
    }
    
}