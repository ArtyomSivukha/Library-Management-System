using LibraryManagementSystem.DataAccessLayer.Entities;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagementSystem.DataAccessLayer.Repositories;

public class AuthorRepository : IAuthorRepository
{
    private readonly LibraryDbContext _dbContext;

    public AuthorRepository(LibraryDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<IEnumerable<Author>> GetAllAsync()
    {
        return await _dbContext.Authors.ToArrayAsync();
    }

    public async Task<Author?> GetByIdAsync(Guid id)
    {
        return await _dbContext.Authors.FindAsync(id);
    }

    public async Task<Author> CreateAsync(Author author)
    {
        _dbContext.Authors.Add(author);
        await _dbContext.SaveChangesAsync();
        return author;
    }

    public async Task UpdateAsync(Author author)
    {
        _dbContext.Authors.Update(author);
        await _dbContext.SaveChangesAsync();
    }

    public async Task DeleteAsync(Guid id)
    {
        var author = await _dbContext.Authors.FindAsync(id);
        if (author != null)
        {
            _dbContext.Authors.Remove(author);
            await _dbContext.SaveChangesAsync();
        }
    }

    public async Task<IEnumerable<Author>> FindByNameAsync(string name)
    {
        return await _dbContext.Authors
            .Where(a => a.Name.Contains(name))
            .ToArrayAsync();
    }

    public async Task<Dictionary<Guid, int>> GetBooksCountByAuthorAsync()
    {
        return await _dbContext.Authors
            .Select(a => new { a.Id, Count = a.Books.Count })
            .ToDictionaryAsync(x => x.Id, x => x.Count);
    }
}

