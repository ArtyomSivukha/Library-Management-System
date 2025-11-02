using System.ComponentModel.DataAnnotations;

namespace LibraryManagementSystem.WebApi.Models;

public class AuthorViewModel
{
    public Guid Id { get; set; }

    [Required(ErrorMessage = "Name is required")]
    public string Name { get; set; }

    [Required(ErrorMessage = "DateOfBirth is required")]
    public DateTime DateOfBirth { get; set; }
}