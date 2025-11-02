using LibraryManagementSystem.BusinessLogicLayer.Repositories;
using LibraryManagementSystem.DataAccessLayer.Entities;
using Microsoft.EntityFrameworkCore;
using Model_Book = LibraryManagementSystem.BusinessLogicLayer.Models.Book;

namespace LibraryManagementSystem.DataAccessLayer.Repositories;

public class BookRepository : IBookRepository
{
    private readonly LibraryDbContext _dbContext;

    public BookRepository(LibraryDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<IEnumerable<Model_Book>> GetAllAsync()
    {
        var bookEntities = await _dbContext.Books
            .Include(b => b.Author)
            .ToArrayAsync();

        return bookEntities.Select(FromEntityToModel);
    }

    public async Task<Model_Book?> GetByIdAsync(Guid id)
    {
        var bookEntity = await _dbContext.Books
            .Include(b => b.Author)
            .FirstOrDefaultAsync(b => b.Id == id);

        return bookEntity != null ? FromEntityToModel(bookEntity) : null;
    }

    public async Task<Model_Book> CreateAsync(Model_Book book)
    {
        var authorEntity = await _dbContext.Authors
            .FirstOrDefaultAsync(a => a.Id == book.AuthorId);

        if (authorEntity is null)
        {
            throw new ArgumentNullException($"Author with id {book.AuthorId} does not exist");
        }

        var bookEntity = ToEntity(book);
        bookEntity.Author = authorEntity;

        _dbContext.Books.Add(bookEntity);
        await _dbContext.SaveChangesAsync();

        book.Id = bookEntity.Id;
        return book;
    }

    public async Task UpdateAsync(Model_Book book)
    {
        var bookToUpdate = await _dbContext.Books
            .FirstOrDefaultAsync(a => a.Id == book.Id);

        if (bookToUpdate is null)
        {
            throw new ArgumentNullException(nameof(bookToUpdate), $"{nameof(bookToUpdate)} is null");
        }

        var author = await _dbContext.Authors.FirstAsync(a => a.Id == book.AuthorId);

        bookToUpdate.Title = book.Title;
        bookToUpdate.PublisherYear = book.PublisherYear;
        bookToUpdate.Author = author;
        await _dbContext.SaveChangesAsync();
    }

    public async Task DeleteAsync(Guid id)
    {
        var bookEntity = await _dbContext.Books.FindAsync(id);
        if (bookEntity != null)
        {
            _dbContext.Books.Remove(bookEntity);
            await _dbContext.SaveChangesAsync();
        }
    }

    public async Task<IEnumerable<Model_Book>> GetBooksPublishedAfterAsync(int year)
    {
        var bookEntities = await _dbContext.Books
            .Include(b => b.Author)
            .Where(b => b.PublisherYear > year)
            .ToArrayAsync();

        return bookEntities.Select(FromEntityToModel);
    }

    private static Book ToEntity(Model_Book book) =>
        new()
        {
            Id = book.Id,
            Title = book.Title,
            PublisherYear = book.PublisherYear
        };

    private static Model_Book FromEntityToModel(Book book) =>
        new()
        {
            Id = book.Id,
            Title = book.Title,
            PublisherYear = book.PublisherYear,
            AuthorId = book.Author.Id
        };
}