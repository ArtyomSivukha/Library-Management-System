using System.ComponentModel.DataAnnotations;

namespace LibraryManagementSystem.DataAccessLayer.Entities;

public class Book
{
    public Guid Id { get; set; }
    
    [Required(ErrorMessage = "Title is required")]
    public string Title { get; set; }
    [Range(1, int.MaxValue, ErrorMessage = "Please enter a positive number")]
    public int PublisherYear { get; set; }
    public Author Author { get; set; }
}