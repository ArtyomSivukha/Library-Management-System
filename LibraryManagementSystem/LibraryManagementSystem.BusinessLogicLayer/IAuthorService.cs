using LibraryManagementSystem.BusinessLogicLayer.Models;

namespace LibraryManagementSystem.BusinessLogicLayer;

public interface IAuthorService
{
    Task<IEnumerable<Author>> GetAllAuthorsAsync();
    Task<Author?> GetAuthorByIdAsync(long id);
    Task<Author> CreateAuthorAsync(Author author);
    Task UpdateAuthorAsync(Author author);
    Task DeleteAuthorAsync(long id);
    Task<IEnumerable<Author>> FindAuthorsByNameAsync(string name);
    Task<IEnumerable<AuthorWithCount>> GetAllAuthorsWithBooksCountAsync();
}