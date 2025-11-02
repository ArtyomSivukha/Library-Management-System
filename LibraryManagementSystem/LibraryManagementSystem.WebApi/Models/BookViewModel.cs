using System.ComponentModel.DataAnnotations;

namespace LibraryManagementSystem.WebApi.Models;

public class BookViewModel
{
    public Guid Id { get; set; }
    
    [Required(ErrorMessage = "Title is required")]
    public string Title { get; set; }
    
    [Range(1, int.MaxValue, ErrorMessage = "Please enter a positive number")]
    public int PublisherYear { get; set; }
    
    public Guid AuthorId { get; set; }
}