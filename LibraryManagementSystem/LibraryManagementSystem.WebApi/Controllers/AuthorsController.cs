using LibraryManagementSystem.BusinessLogicLayer;
using LibraryManagementSystem.BusinessLogicLayer.Models;

namespace LibraryManagementSystem.WebApi.Controllers;

using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class AuthorsController : ControllerBase
{
    private readonly IAuthorService _authorService;

    public AuthorsController(IAuthorService authorService)
    {
        _authorService = authorService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllAuthorsAsync()
    {
        var authors = await _authorService.GetAllAuthorsAsync();
        return Ok(authors);
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetAuthorByIdAsync(Guid id)
    {
        var author = await _authorService.GetAuthorByIdAsync(id);
        return Ok(author);
    }

    [HttpPost]
    public async Task<IActionResult> CreateAuthorsAsync(Author author)
    {
        var createdAuthor = await _authorService.CreateAuthorAsync(author);
        return Ok(createdAuthor);
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> UpdateAuthorAsync(Author author, Guid id)
    {
        if (id != author.Id)
        {
            return BadRequest("Route ID and author ID do not match");
        }

        await _authorService.UpdateAuthorAsync(author);
        return Ok();
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> DeleteAuthorAsync(Guid id)
    {
        await _authorService.DeleteAuthorAsync(id);
        return Ok();
    }

    [HttpGet("search")]
    public async Task<IActionResult> FindAuthorsByNameAsync(string name)
    {
        var authorsByName = await _authorService.FindAuthorsByNameAsync(name);
        return Ok(authorsByName);
    }

    [HttpGet("booksCount")]
    public async Task<IActionResult> GetAllAuthorsWithBooks()
    {
        var authors = await _authorService.GetAllAuthorsWithBooksCountAsync();
        return Ok(authors);
    }
}