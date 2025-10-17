using LibraryManagementSystem.Models;

namespace LibraryManagementSystem.Controllers;

public class AuthorService
{
    public static List<Author> _authors = new()
    {
        new Author{Id = 1, Name = "Author", DateOfBirth = new DateTime(1950,1,1)},
    };
    
    public List<Author> GetAllAuthors() => _authors;
    public Author GetAuthorById(int id) => _authors.FirstOrDefault(p => p.Id == id);
}