using MediatR;
using Microsoft.AspNetCore.Mvc;
using Square.Application.Commands;

namespace Square.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PointsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public PointsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Imports list of points
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     POST /point/import
        ///     [
        ///         {
        ///             "x": 0,
        ///             "y": 0
        ///         },
        ///         {
        ///             "x": 0,
        ///             "y": 1
        ///         }
        ///      ]
        ///
        /// </remarks>
        /// <param name="points"></param>
        [HttpPost("import")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> AddPoints(PointListAddCommand command)
        {
            var result = await _mediator.Send(command);

            return CreatedAtAction(nameof(AddPoints), result);
        }

        /// <summary>
        /// Adds point
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///     
        ///     POST /point
        ///     
        ///         {
        ///             "x": 1,
        ///             "y": 0
        ///         }
        ///     
        /// </remarks>
        /// <param name="point"></param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> AddPoint(PointAddCommand command)
        {
            var result = await _mediator.Send(command);

            return CreatedAtAction(nameof(AddPoint), result);
        }

        /// <summary>
        /// Deletes a specific point
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// 
        ///     DELETE /points
        ///         {
        ///             "x": 0,
        ///             "y": 0
        ///         }
        /// </remarks>
        /// <param name="point"></param>
        /// <returns></returns>
        [HttpDelete]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeletePoint(PointDeleteCommand command)
        {
            await _mediator.Send(command);

            return NoContent();
        }
    }
}
