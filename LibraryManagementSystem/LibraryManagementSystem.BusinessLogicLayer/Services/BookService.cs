using LibraryManagementSystem.DataAccessLayer.Repositories;
using LibraryManagementSystem.DataAccessLayer.Entities;
using ModelsBook = LibraryManagementSystem.BusinessLogicLayer.Models.Book;

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

    public async Task<IEnumerable<ModelsBook>> GetAllBooksAsync()
    {
        var bookEntities = await _bookRepository.GetAllAsync();
        return bookEntities.Select(FromEntityToModel);
    }

    public async Task<ModelsBook> GetBookByIdAsync(Guid id)
    {
        var bookEntity = await _bookRepository.GetByIdAsync(id);
        if (bookEntity is null)
        {
            throw new ArgumentException($"Book with id {id} not found");
        }

        return FromEntityToModel(bookEntity);
    }

    public async Task<ModelsBook> CreateBookAsync(ModelsBook book)
    {
        if (book is null)
        {
            throw new ArgumentNullException(nameof(book));
        }

        var authorEntity = await _authorRepository.GetByIdAsync(book.AuthorId);
        if (authorEntity is null)
        {
            throw new ArgumentException($"Author with id {book.AuthorId} not found");
        }

        var bookEntity = ToEntity(book, authorEntity);
        var createdEntity = await _bookRepository.CreateAsync(bookEntity);
        return FromEntityToModel(createdEntity);
    }

    public async Task UpdateBookAsync(ModelsBook book)
    {
        if (book is null)
        {
            throw new ArgumentNullException(nameof(book));
        }

        var existingBook = await _bookRepository.GetByIdAsync(book.Id);
        if (existingBook is null)
        {
            throw new ArgumentException($"Book with id {book.Id} not found");
        }

        var authorEntity = await _authorRepository.GetByIdAsync(book.AuthorId);
        if (authorEntity is null)
        {
            throw new ArgumentException($"Author with id {book.AuthorId} not found");
        }

        var bookEntity = ToEntity(book, authorEntity);
        await _bookRepository.UpdateAsync(bookEntity);
    }

    public async Task DeleteBookAsync(Guid id)
    {
        var book = await _bookRepository.GetByIdAsync(id);
        if (book is null)
        {
            throw new ArgumentException($"Book with id {id} not found");
        }

        await _bookRepository.DeleteAsync(id);
    }

    public async Task<IEnumerable<ModelsBook>> GetBooksPublishedAfterAsync(int year)
    {
        if (year < 0)
        {
            throw new ArgumentException("Year cannot be negative");
        }

        var bookEntities = await _bookRepository.GetBooksPublishedAfterAsync(year);
        return bookEntities.Select(FromEntityToModel);
    }
    
    private static Book ToEntity(ModelsBook model, Author author)
    {
        return new Book
        {
            Id = model.Id,
            Title = model.Title,
            PublisherYear = model.PublisherYear,
            Author = author
        };
    }

    private static ModelsBook FromEntityToModel(Book book)
    {
        return new ModelsBook
        {
            Id = book.Id,
            Title = book.Title,
            PublisherYear = book.PublisherYear,
            AuthorId = book.Author.Id
        };
    }


}