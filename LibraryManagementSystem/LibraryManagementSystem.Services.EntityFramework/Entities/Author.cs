using System.ComponentModel.DataAnnotations;

namespace LibraryManagementSystem.Services.EntityFramework.Entities;

public class Author
{
    public long Id { get; set; }

    [Required(ErrorMessage = "Name is required")]
    public string Name { get; set; }
    public DateTime DateOfBirth { get; set; }
    public ICollection<Book> Books { get; set; }
}