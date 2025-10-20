using LibraryManagementSystem.Services.Models;

namespace LibraryManagementSystem.Services.Local;

public class AuthorService : IAuthorService
{
    private static readonly List<Author> Authors = new();

    private static long _idAuthor;

    private long GetNextAuthorId()
    {
        return ++_idAuthor;
    }

    public IEnumerable<Author> GetAllAuthors() => Authors;
    public Author? GetAuthorById(long id) => Authors.FirstOrDefault(p => p.Id == id);

    public Author CreateAuthor(Author author)
    {
        author.Id = GetNextAuthorId();
        Authors.Add(author);
        return author;
    }

    public void UpdateAuthor(Author author)
    {
        var updatedAuthor = GetAuthorById(author.Id);
        if (updatedAuthor is null)
        {
            throw new ArgumentException("Author could not be found.");
        }

        updatedAuthor.Name = author.Name;
        updatedAuthor.DateOfBirth = author.DateOfBirth;
    }

    public void DeleteAuthor(long id)
    {
        var deleteAuthor = GetAuthorById(id);
        if (deleteAuthor is null)
        {
            throw new ArgumentException("Author could not be found.");
        }

        Authors.Remove(deleteAuthor);
    }
}