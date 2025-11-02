using System.ComponentModel.DataAnnotations;

namespace LibraryManagementSystem.WebApi.Models;

public class AuthorWithBooksViewModel
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public DateTime DateOfBirth { get; set; }
    public IEnumerable<BookViewModel> Books { get; set; } 
}