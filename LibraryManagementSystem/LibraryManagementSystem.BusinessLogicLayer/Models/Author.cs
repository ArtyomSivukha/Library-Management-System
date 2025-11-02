namespace LibraryManagementSystem.BusinessLogicLayer.Models;

public class Author
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public DateTime DateOfBirth { get; set; }
    public IEnumerable<Book> Books { get; set; }
}