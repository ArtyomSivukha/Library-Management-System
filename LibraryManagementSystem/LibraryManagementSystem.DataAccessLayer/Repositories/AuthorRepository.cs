using LibraryManagementSystem.BusinessLogicLayer.Repositories;
using Microsoft.EntityFrameworkCore;
using LibraryManagementSystem.DataAccessLayer.Entities;
using ModelAuthor = LibraryManagementSystem.BusinessLogicLayer.Models.Author;

namespace LibraryManagementSystem.DataAccessLayer.Repositories;

public class AuthorRepository : IAuthorRepository
{
    private readonly LibraryDbContext _dbContext;

    public AuthorRepository(LibraryDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<IEnumerable<ModelAuthor>> GetAllAsync()
    {
        var authorEntities = await _dbContext.Authors.Include(a => a.Books).ToArrayAsync();
        return authorEntities.Select(FromEntityToModel);
    }

    public async Task<ModelAuthor?> GetByIdAsync(Guid id)
    {
        var authorEntity = await _dbContext.Authors
            .Include(a => a.Books)
            .FirstOrDefaultAsync(a => a.Id == id);

        return authorEntity != null ? FromEntityToModel(authorEntity) : null;
    }

    public async Task<ModelAuthor> CreateAsync(ModelAuthor author)
    {
        var authorEntity = ToEntity(author);
        _dbContext.Authors.Add(authorEntity);
        await _dbContext.SaveChangesAsync();

        author.Id = authorEntity.Id;
        return author;
    }

    public async Task UpdateAsync(ModelAuthor author)
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

    public async Task<IEnumerable<ModelAuthor>> FindByNameAsync(string name)
    {
        var authorEntities = await _dbContext.Authors
            .Include(a => a.Books)
            .Where(a => a.Name.Contains(name))
            .ToArrayAsync();

        return authorEntities.Select(FromEntityToModel);
    }

    private static Author ToEntity(ModelAuthor author) =>
        new()
        {
            Id = author.Id,
            Name = author.Name,
            DateOfBirth = author.DateOfBirth,
        };

    private static ModelAuthor FromEntityToModel(Author author) =>
        new()
        {
            Id = author.Id,
            Name = author.Name,
            DateOfBirth = author.DateOfBirth,
            Books = author.Books.Select(a => new BusinessLogicLayer.Models.Book
            {
                Id = a.Id,
                Title = a.Title,
                PublisherYear = a.PublisherYear,
                AuthorId = author.Id
            })
        };
}