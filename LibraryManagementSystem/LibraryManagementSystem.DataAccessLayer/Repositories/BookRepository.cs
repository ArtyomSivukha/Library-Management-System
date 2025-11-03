using LibraryManagementSystem.DataAccessLayer.Entities;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagementSystem.DataAccessLayer.Repositories;

public class BookRepository : IBookRepository
{
    private readonly LibraryDbContext _dbContext;

    public BookRepository(LibraryDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<IEnumerable<Book>?> GetAllAsync()
    {
        return await _dbContext.Books
            .Include(b => b.Author)
            .ToArrayAsync();
    }

    public async Task<Book?> GetByIdAsync(Guid id)
    {
        return await _dbContext.Books
            .Include(b => b.Author)
            .FirstOrDefaultAsync(b => b.Id == id);
    }

    public async Task<Book> CreateAsync(Book book)
    {
        var authorEntity = await _dbContext.Authors
            .FirstOrDefaultAsync(a => a.Id == book.Author.Id);

        if (authorEntity is null)
        {
            throw new ArgumentNullException($"Author with id {book.Author.Id} does not exist");
        }

        book.Author = authorEntity;
        _dbContext.Books.Add(book);
        await _dbContext.SaveChangesAsync();

        return book;
    }

    public async Task UpdateAsync(Book book)
    {
        var bookToUpdate = await _dbContext.Books
            .FirstOrDefaultAsync(a => a.Id == book.Id);

        if (bookToUpdate is null)
        {
            throw new ArgumentNullException(nameof(bookToUpdate), $"{nameof(bookToUpdate)} is null");
        }

        var author = await _dbContext.Authors.FirstAsync(a => a.Id == book.Author.Id);

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

    public async Task<IEnumerable<Book>?> GetBooksPublishedAfterAsync(int year)
    {
        return await _dbContext.Books
            .Include(b => b.Author)
            .Where(b => b.PublisherYear > year)
            .ToArrayAsync();
    }
}