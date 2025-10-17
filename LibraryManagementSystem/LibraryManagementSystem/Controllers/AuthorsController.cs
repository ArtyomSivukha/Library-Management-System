namespace LibraryManagementSystem.Controllers;

using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class AuthorsController : ControllerBase
{
    private readonly AuthorService _authorService;

    public AuthorsController(AuthorService authorService)
    {
        _authorService = authorService;
    }
    
    [HttpGet]
    public IActionResult GetAll()
    {
        var authors = _authorService.GetAllAuthors();
        return Ok(authors);
    }
    
    [HttpGet("{id}")]
    public IActionResult GetById(int id)
    {
        var author = _authorService.GetAuthorById(id);
        if (author == null)
            return NotFound();
        return Ok(author);
    }
}