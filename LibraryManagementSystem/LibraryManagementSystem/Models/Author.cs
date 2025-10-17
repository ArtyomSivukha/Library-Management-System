namespace LibraryManagementSystem.Models;

public class Author
{
    public long Id { get; set; }
    public string Name { get; set; }
    public DateTime DateOfBirth { get; set; }
    // public ICollection<Book> Books { get; set; }
}