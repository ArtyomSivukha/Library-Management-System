using LibraryManagementSystem.Services.Local;
using LibraryManagementSystem.Services.Models;

namespace LibraryManagementSystem.WebApi.Controllers;

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
    
    [HttpGet("{id:long}")]
    public IActionResult GetById(long id)
    {
        var author = _authorService.GetAuthorById(id);
        if (author is null) 
        {
            return NotFound();
        }
        return Ok(author);
    }

    [HttpPost]
    public IActionResult CreateAuthors(Author author)
    {
        if (ModelState.IsValid)
        {
            var createdAuthor = _authorService.CreateAuthor(author);
            return Ok(createdAuthor);
        }

        return BadRequest();
    }

    [HttpPut("{id:long}")]
    public IActionResult UpdateAuthor(Author author, long id)
    {
        if (id == author.Id)
        {
            _authorService.UpdateAuthor(author);
            return Ok();
        }
        return BadRequest();
    }

    [HttpDelete("{id:long}")]
    public IActionResult Delete(long id)
    {
        try
        {
            _authorService.DeleteAuthor(id);
            return Ok();
        }
        catch (ArgumentException e)
        {
            return BadRequest(e.Message);
        }
    }
}