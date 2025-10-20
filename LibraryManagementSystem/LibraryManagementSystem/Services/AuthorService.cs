using LibraryManagementSystem.Models;

namespace LibraryManagementSystem.Services;

public class AuthorService
{
    private static List<Author> _authors = new();
    
    private static long _idAuthor;

    public long GetNextAuthorId()
    {
        return ++_idAuthor;
    }

    public IEnumerable<Author> GetAllAuthors() => _authors;
    public Author? GetAuthorById(long id) => _authors.FirstOrDefault(p => p.Id == id);
    
    public Author CreateAuthor(Author author)
    {
        author.Id = GetNextAuthorId();
        _authors.Add(author);
        return author;
    }

    public void UpdateAuthor(Author author)
    {
        var updatedAuthor = GetAuthorById(author.Id);
        if (updatedAuthor is not null)
        {
            updatedAuthor.Name = author.Name;
            updatedAuthor.DateOfBirth = author.DateOfBirth;
        }
    }

    public void DeleteAuthor(long id)
    {
        var deleteAuthor = GetAuthorById(id);
        if (deleteAuthor is not null)
        {
            _authors.Remove(deleteAuthor);
        }
    }
}