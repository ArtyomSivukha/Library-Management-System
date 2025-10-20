namespace LibraryManagementSystem.Models;

public class Book
{
    public long Id { get; set; }
    public string Title { get; set; }
    public int PublisherYear { get; set; }
    public long AuthorId { get; set; }
}