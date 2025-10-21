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
    public async Task<IActionResult> GetAllAuthorsAsync()
    {
        var authors = await _authorService.GetAllAuthorsAsync();
        return Ok(authors);
    }

    [HttpGet("{id:long}")]
    public async Task<IActionResult> GetAuthorByIdAsync(long id)
    {
        var author = await _authorService.GetAuthorByIdAsync(id);
        return author == null ? NotFound() : Ok(author);
    }

    [HttpPost]
    public async Task<IActionResult> CreateAuthorsAsync(Author author)
    {
        var createdAuthor = await _authorService.CreateAuthorAsync(author);
        return Ok(createdAuthor);
    }

    [HttpPut("{id:long}")]
    public async Task<IActionResult> UpdateAuthorAsync(Author author, long id)
    {
        if (id != author.Id)
        {
            return BadRequest("Route ID and author ID do not match");
        }
        try
        {
            await _authorService.UpdateAuthorAsync(author);
            return Ok();
        }
        catch (ArgumentException e)
        {
            return BadRequest(e.Message);            
        }
    }

    [HttpDelete("{id:long}")]
    public async Task<IActionResult> DeleteAuthorAsync(long id)
    {
        try
        {
            await _authorService.DeleteAuthorAsync(id);
            return Ok();
        }
        catch (ArgumentException e)
        {
            return BadRequest(e.Message);
        }
    }
}