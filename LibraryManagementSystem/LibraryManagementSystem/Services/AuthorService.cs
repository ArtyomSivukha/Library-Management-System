using LibraryManagementSystem.Models;

namespace LibraryManagementSystem.Controllers;

public class AuthorService
{
    
    private static readonly List<Author> _authors = new()
    {
        new Author{Id = 1, Name = "Author", DateOfBirth = new DateTime(1950,1,1)},
    };
    
    public IEnumerable<Author> GetAllAuthors() => _authors;
    public Author? GetAuthorById(int id) => _authors.FirstOrDefault(p => p.Id == id);

    
    public Author CreateAuthor(Author author)
    {
        _authors.Add(author);
        return author;
    }

    public Author UpdateAuthor(Author author)
    {
        
    }

    public void DeleteAuthor(long id)
    {
        var deleteAuthor = _authors.FirstOrDefault(p => p.Id == id);
        if (deleteAuthor is not null)
        {
            _authors.Remove(deleteAuthor);
        }
    }
}