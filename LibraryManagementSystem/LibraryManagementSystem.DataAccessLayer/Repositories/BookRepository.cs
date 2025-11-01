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

    public async Task<IEnumerable<Book>> GetAllAsync()
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
        _dbContext.Books.Add(book);
        await _dbContext.SaveChangesAsync();
        return book;
    }

    public async Task UpdateAsync(Book book)
    {
        _dbContext.Books.Update(book);
        await _dbContext.SaveChangesAsync();
    }

    public async Task DeleteAsync(Guid id)
    {
        var book = await _dbContext.Books.FindAsync(id);
        if (book != null)
        {
            _dbContext.Books.Remove(book);
            await _dbContext.SaveChangesAsync();
        }
    }

    public async Task<IEnumerable<Book>> GetBooksPublishedAfterAsync(int year)
    {
        return await _dbContext.Books
            .Include(b => b.Author)
            .Where(b => b.PublisherYear > year)
            .ToArrayAsync();
    }
}

