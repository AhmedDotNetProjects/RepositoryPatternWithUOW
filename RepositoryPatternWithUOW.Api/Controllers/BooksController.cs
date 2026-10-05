using MediatR;
using Microsoft.AspNetCore.Mvc;
using RepositoryPatternWithUOW.Api.Requests;
using RepositoryPatternWithUOW.Application.Features.Books.Commands.AddBook;
using RepositoryPatternWithUOW.Application.Features.Books.Commands.AddBooks;
using RepositoryPatternWithUOW.Application.Features.Books.Commands.DeleteBook;
using RepositoryPatternWithUOW.Application.Features.Books.Commands.UpdateBook;
using RepositoryPatternWithUOW.Application.Features.Books.Queries.CountBooks;
using RepositoryPatternWithUOW.Application.Features.Books.Queries.FindBookUsingExpression;
using RepositoryPatternWithUOW.Application.Features.Books.Queries.GetAllBooks;
using RepositoryPatternWithUOW.Application.Features.Books.Queries.GetBookById;
using RepositoryPatternWithUOW.Application.Features.Books.Queries.GetBooksByTitleLength;

namespace RepositoryPatternWithUOW.Api.Controllers;

[Route("api/books")]
[ApiController]
[Produces("application/json")]
public sealed class BooksController(IMediator mediator) : ControllerBase
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
            new GetBookByIdQuery(id),
            cancellationToken);

        return Ok(response);
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAll(
        CancellationToken cancellationToken)
    {
        var response = await _mediator.Send(
            new GetAllBooksQuery(),
            cancellationToken);

        return Ok(response);
    }

    [HttpGet("by-title/{title}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetByTitle(
        string title,
        CancellationToken cancellationToken)
    {
        var response = await _mediator.Send(
            new FindBookUsingExpressionQuery(
                book => book.Title == title),
            cancellationToken);

        return Ok(response);
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> AddBook(
        [FromBody] AddBookCommand command,
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
    public async Task<IActionResult> AddBooks(
        [FromBody] AddBooksCommand command,
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
        [FromBody] UpdateBookRequest request,
        CancellationToken cancellationToken)
    {
        var response = await _mediator.Send(
            new UpdateBookCommand(
                id,
                request.Title),
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
            new DeleteBookCommand(id),
            cancellationToken);

        return NoContent();
    }

    [HttpGet("count")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> Count(
        CancellationToken cancellationToken)
    {
        var response = await _mediator.Send(
            new CountBooksQuery(),
            cancellationToken);

        return Ok(response);
    }

    [HttpGet("title-length/{titleLength:int}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetBooksByTitleLength(
        int titleLength,
        CancellationToken cancellationToken)
    {
        var response = await _mediator.Send(
            new GetBooksByTitleLengthQuery(titleLength),
            cancellationToken);

        return Ok(response);
    }
}