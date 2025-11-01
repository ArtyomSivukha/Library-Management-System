using System.ComponentModel.DataAnnotations;

namespace LibraryManagementSystem.BusinessLogicLayer.Models;

public class Book
{
    public Guid Id { get; set; }
    
    [Required(ErrorMessage = "Title is required")]
    public string Title { get; set; }
    
    [Range(1, int.MaxValue, ErrorMessage = "Please enter a positive number")]
    public int PublisherYear { get; set; }
    public Guid AuthorId { get; set; }
}