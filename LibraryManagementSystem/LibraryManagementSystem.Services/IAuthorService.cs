using LibraryManagementSystem.Services.Models;

namespace LibraryManagementSystem.Services;

public interface IAuthorService
{
    IEnumerable<Author> GetAllAuthors();
    Author? GetAuthorById(long id);
    Author CreateAuthor(Author author);
    void UpdateAuthor(Author author);
    void DeleteAuthor(long id);
}