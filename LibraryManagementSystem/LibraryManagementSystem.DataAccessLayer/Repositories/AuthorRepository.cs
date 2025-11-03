using Microsoft.EntityFrameworkCore;
using LibraryManagementSystem.DataAccessLayer.Entities;

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
        return await _dbContext.Authors.Include(a => a.Books).ToArrayAsync();
    }

    public async Task<Author?> GetByIdAsync(Guid id)
    {
        return await _dbContext.Authors
            .Include(a => a.Books)
            .FirstOrDefaultAsync(a => a.Id == id);
    }

    public async Task<Author> CreateAsync(Author author)
    {
        _dbContext.Authors.Add(author);
        await _dbContext.SaveChangesAsync();
        return author;
    }

    public async Task UpdateAsync(Author author)
    {
        var authorEntity = await _dbContext.Authors
            .Include(a => a.Books)
            .FirstOrDefaultAsync(a => a.Id == author.Id);
        if (authorEntity is null)
        {
            throw new ArgumentNullException(nameof(authorEntity), $"{nameof(authorEntity)} is null");
        }

        authorEntity.Name = author.Name;
        authorEntity.DateOfBirth = author.DateOfBirth;
        await _dbContext.SaveChangesAsync();
    }

    public async Task DeleteAsync(Guid id)
    {
        var authorEntity = await _dbContext.Authors.FindAsync(id);
        if (authorEntity != null)
        {
            _dbContext.Authors.Remove(authorEntity);
            await _dbContext.SaveChangesAsync();
        }
    }

    public async Task<IEnumerable<Author>> FindByNameAsync(string name)
    {
        return await _dbContext.Authors
            .Include(a => a.Books)
            .Where(a => a.Name.Contains(name))
            .ToArrayAsync();
    }
}