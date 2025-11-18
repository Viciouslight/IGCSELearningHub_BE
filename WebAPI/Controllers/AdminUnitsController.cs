using Asp.Versioning;
using IGCSELearningHub.Application.DTOs.Units;
using IGCSELearningHub.Application.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace IGCSELearningHub.WebAPI.Controllers
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/units")]
    //[Authorize(Roles = "Admin,Teacher")]
    public class AdminUnitsController : ControllerBase
    {
        private readonly IUnitService _service;
        public AdminUnitsController(IUnitService service) => _service = service;

        [HttpGet("{unitId:int}")]
        public async Task<IActionResult> GetById([FromRoute] int unitId)
        {
            var result = await _service.GetByIdAsync(unitId);
            return StatusCode(result.StatusCode, result);
        }

        [HttpGet("~/api/v{version:apiVersion}/courses/{courseId:int}/units")]
        public async Task<IActionResult> GetByCourse([FromRoute] int courseId)
        {
            var result = await _service.GetByCourseAsync(courseId);
            return StatusCode(result.StatusCode, result);
        }

        [HttpPost("~/api/v{version:apiVersion}/courses/{courseId:int}/units")]
        public async Task<IActionResult> Create([FromRoute] int courseId, [FromBody] UnitCreateDTO dto)
        {
            var result = await _service.CreateAsync(courseId, dto);
            return StatusCode(result.StatusCode, result);
        }

        [HttpPut("{unitId:int}")]
        public async Task<IActionResult> Update([FromRoute] int unitId, [FromBody] UnitUpdateDTO dto)
        {
            var result = await _service.UpdateAsync(unitId, dto);
            return StatusCode(result.StatusCode, result);
        }

        [HttpDelete("{unitId:int}")]
        public async Task<IActionResult> Delete([FromRoute] int unitId)
        {
            var result = await _service.DeleteAsync(unitId);
            return StatusCode(result.StatusCode, result);
        }
    }
}
