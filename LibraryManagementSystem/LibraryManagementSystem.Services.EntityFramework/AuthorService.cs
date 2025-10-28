using System.Linq.Expressions;
using LibraryManagementSystem.Services.EntityFramework.Entities;
using ServiceEntities = LibraryManagementSystem.Services.Models;


namespace LibraryManagementSystem.Services.EntityFramework;

public class AuthorService : IAuthorService
{
    private readonly LibraryDbContext _dbContext;

    public AuthorService(LibraryDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<IEnumerable<ServiceEntities.Author>> GetAllAuthorsAsync()
    {
        return _dbContext.Authors.Select(FromEntity());
    }

    public async Task<ServiceEntities.Author?> GetAuthorByIdAsync(long id)
    {
       var author = await _dbContext.Authors.FindAsync(id);
       return author is null ? null : MapUserToServiceEntity(author);
    }

    public async Task<ServiceEntities.Author> CreateAuthorAsync(ServiceEntities.Author author)
    {
        if (author is null)
        {
            throw new ArgumentNullException(nameof(author), $"{nameof(author)} is null");
        }

        var newAuthor = ToEntity(author);
        await _dbContext.Authors.AddAsync(newAuthor);
        _dbContext.SaveChanges();
        
        author.Id = newAuthor.Id;
        return author;
    }

    public Task UpdateAuthorAsync(ServiceEntities.Author author)
    {
        throw new NotImplementedException();
    }
    

    public async Task DeleteAuthorAsync(long id)
    {
        var author = await _dbContext.Authors.FindAsync(id);
        if (author is null)
        {
            throw new ArgumentNullException(nameof(author), $"{nameof(author)} is null");
        }
        
        _dbContext.Authors.Remove(author);
        await _dbContext.SaveChangesAsync();
    }
    
    private static Author ToEntity(ServiceEntities.Author author) =>
        new Author
        {
            Id = author.Id,
            Name = author.Name,
            DateOfBirth = author.DateOfBirth,
        };

    private static Expression<Func<Author, ServiceEntities.Author>> FromEntity() => user => MapUserToServiceEntity(user);

    private static ServiceEntities.Author MapUserToServiceEntity(Author author) =>
        new ServiceEntities.Author
        {
            Id = author.Id,
            Name = author.Name,
            DateOfBirth = author.DateOfBirth,
        };
    
    
}