using LibraryManagementSystem.BusinessLogicLayer.Repositories;
using Models_Book = LibraryManagementSystem.BusinessLogicLayer.Models.Book;

namespace LibraryManagementSystem.BusinessLogicLayer.Services;

public class BookService : IBookService
{
    private readonly IBookRepository _bookRepository;
    private readonly IAuthorRepository _authorRepository;

    public BookService(IBookRepository bookRepository, IAuthorRepository authorRepository)
    {
        _bookRepository = bookRepository;
        _authorRepository = authorRepository;
    }

    public async Task<IEnumerable<Models_Book>> GetAllBooksAsync()
    {
        return await _bookRepository.GetAllAsync();
    }

    public async Task<Models_Book> GetBookByIdAsync(Guid id)
    {
        var book = await _bookRepository.GetByIdAsync(id);
        if (book is null)
        {
            throw new ArgumentNullException(nameof(book), $"{nameof(book)} is null");
        }

        return book;
    }

    public async Task<Models_Book> CreateBookAsync(Models_Book book)
    {
        if (book is null)
        {
            throw new ArgumentNullException(nameof(book), $"{nameof(book)} is null");
        }
        return await _bookRepository.CreateAsync(book);
    }

    public async Task UpdateBookAsync(Models_Book book)
    {
        if (book is null)
        {
            throw new ArgumentNullException(nameof(book), $"{nameof(book)} is null");
        }

        var author = await _authorRepository.GetByIdAsync(book.AuthorId);
        if (author is null)
        {
            throw new ArgumentNullException($"Author with id {book.AuthorId} does not exist");
        }

        await _bookRepository.UpdateAsync(book);
    }

    public async Task DeleteBookAsync(Guid id)
    {
        var deleteBook = await _bookRepository.GetByIdAsync(id);
        if (deleteBook is null)
        {
            throw new ArgumentNullException(nameof(deleteBook), $"{nameof(deleteBook)} is null");
        }

        await _bookRepository.DeleteAsync(id);
    }

    public async Task<IEnumerable<Models_Book>> GetBooksPublishedAfterAsync(int year)
    {
        if (year < 0)
        {
            throw new ArgumentException("Incorrect year");
        }

        return await _bookRepository.GetBooksPublishedAfterAsync(year);
    }
}