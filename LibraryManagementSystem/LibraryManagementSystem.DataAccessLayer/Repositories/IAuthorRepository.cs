using LibraryManagementSystem.DataAccessLayer.Entities;

namespace LibraryManagementSystem.DataAccessLayer.Repositories;

public interface IAuthorRepository
{
    Task<IEnumerable<Author>> GetAllAsync();
    Task<Author?> GetByIdAsync(Guid id);
    Task<Author> CreateAsync(Author author);
    Task UpdateAsync(Author author);
    Task DeleteAsync(Guid id);
    Task<IEnumerable<Author>> FindByNameAsync(string name);
    Task<Dictionary<Guid, int>> GetBooksCountByAuthorAsync();
}

