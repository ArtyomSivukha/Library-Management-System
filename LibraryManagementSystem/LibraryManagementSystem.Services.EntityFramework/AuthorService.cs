using LibraryManagementSystem.Services.Models;

namespace LibraryManagementSystem.Services.EntityFramework;

public class AuthorService : IAuthorService
{
    public Task<IEnumerable<Author>> GetAllAuthorsAsync()
    {
        throw new NotImplementedException();
    }

    public Task<Author?> GetAuthorByIdAsync(long id)
    {
        throw new NotImplementedException();
    }

    public Task<Author> CreateAuthorAsync(Author author)
    {
        throw new NotImplementedException();
    }

    public Task UpdateAuthorAsync(Author author)
    {
        throw new NotImplementedException();
    }

    public Task DeleteAuthorAsync(long id)
    {
        throw new NotImplementedException();
    }
    
    
}