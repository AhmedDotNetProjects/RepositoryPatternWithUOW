using MediatR;
using Microsoft.AspNetCore.Mvc;
using RepositoryPatternWithUOW.Api.Requests;
using RepositoryPatternWithUOW.Application.Features.Authors.Commands.AddAuthor;
using RepositoryPatternWithUOW.Application.Features.Authors.Commands.AddAuthors;
using RepositoryPatternWithUOW.Application.Features.Authors.Commands.DeleteAuthor;
using RepositoryPatternWithUOW.Application.Features.Authors.Commands.UpdateAuthor;
using RepositoryPatternWithUOW.Application.Features.Authors.Commands.UpdateBooksByAuthor;
using RepositoryPatternWithUOW.Application.Features.Authors.Queries.CountAuthors;
using RepositoryPatternWithUOW.Application.Features.Authors.Queries.FindAuthorUsingExpression;
using RepositoryPatternWithUOW.Application.Features.Authors.Queries.GetAllAuthors;
using RepositoryPatternWithUOW.Application.Features.Authors.Queries.GetAuthorById;

namespace RepositoryPatternWithUOW.Api.Controllers;

[Route("api/authors")]
[ApiController]
[Produces("application/json")]
public sealed class AuthorsController(IMediator mediator) : ControllerBase
{
    private readonly IMediator _mediator = mediator;

    [HttpGet("{id:int}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetById(
        int id,
        CancellationToken cancellationToken)
    {
        var response = await _mediator.Send(
            new GetAuthorByIdQuery(id),
            cancellationToken);

        return Ok(response);
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAll(
        CancellationToken cancellationToken)
    {
        var response = await _mediator.Send(
            new GetAllAuthorsQuery(),
            cancellationToken);

        return Ok(response);
    }

    [HttpGet("by-name/{name}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetByName(
        string name,
        CancellationToken cancellationToken)
    {
        var response = await _mediator.Send(
            new FindAuthorUsingExpressionQuery(
                author => author.Name == name),
            cancellationToken);

        return Ok(response);
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> AddAuthor(
        [FromBody] AddAuthorCommand command,
        CancellationToken cancellationToken)
    {
        var response = await _mediator.Send(
            command,
            cancellationToken);

        return Ok(response);
    }

    [HttpPost("bulk")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> AddAuthors(
        [FromBody] AddAuthorsCommand command,
        CancellationToken cancellationToken)
    {
        var response = await _mediator.Send(
            command,
            cancellationToken);

        return Ok(response);
    }

    [HttpPut("{id:int}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Update(
        int id,
        [FromBody] UpdateAuthorRequest request,
        CancellationToken cancellationToken)
    {
        var response = await _mediator.Send(
            new UpdateAuthorCommand(
                id,
                request.Name),
            cancellationToken);

        return Ok(response);
    }

    [HttpPut("{authorId:int}/books")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> UpdateBooks(
        int authorId,
        [FromBody] UpdateBooksRequest request,
        CancellationToken cancellationToken)
    {
        var response = await _mediator.Send(
            new UpdateBooksByAuthorCommand(
                authorId,
                request.Books),
            cancellationToken);

        return Ok(response);
    }

    [HttpDelete("{id:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(
        int id,
        CancellationToken cancellationToken)
    {
        await _mediator.Send(
            new DeleteAuthorCommand(id),
            cancellationToken);

        return NoContent();
    }

    [HttpGet("count")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> Count(
        CancellationToken cancellationToken)
    {
        var response = await _mediator.Send(
            new CountAuthorsQuery(),
            cancellationToken);

        return Ok(response);
    }
}