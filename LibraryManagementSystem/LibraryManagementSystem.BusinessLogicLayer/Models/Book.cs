namespace LibraryManagementSystem.BusinessLogicLayer.Models;

public class Book
{
    public Guid Id { get; set; }
    public string Title { get; set; }
    public int PublisherYear { get; set; }
    public Guid AuthorId { get; set; }
}