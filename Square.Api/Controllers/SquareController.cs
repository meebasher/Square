using MediatR;
using Microsoft.AspNetCore.Mvc;
using Square.Application.Queries;

namespace Square.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SquareController : ControllerBase
    {
        private IMediator _mediator;

        public SquareController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Returns all possible squares with all 4 points forming that square.
        /// </summary>
        /// <remarks>
        /// Sample response:
        /// 
        ///     GET: /squares
        ///         [
        ///             {
        ///                 "points": [
        ///                   {
        ///                     "x": 0,
        ///                     "y": 0
        ///                   },
        ///                   {
        ///                     "x": 1,
        ///                     "y": 0
        ///                   },
        ///                   {
        ///                     "x": 1,
        ///                     "y": 1
        ///                   },
        ///                   {
        ///                     "x": 0,
        ///                     "y": 1
        ///                   }
        ///                 ]
        ///             }
        ///         ]
        /// </remarks>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<SquareListResponse>> GetSquares()
        {
            var result = await _mediator.Send(new GetSquareQuery());
            if (result.Squares.Any())
                return Ok(result);

            return NotFound();
        }
    }
}
