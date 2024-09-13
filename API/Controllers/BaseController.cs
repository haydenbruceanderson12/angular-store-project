using System.Net;
using API.RequestHelpers;
using Core.Entities;
using Core.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [ProducesResponseType((int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.NoContent)]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    [ProducesResponseType((int)HttpStatusCode.Unauthorized)]
    [ProducesResponseType((int)HttpStatusCode.NotFound)]
    [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
    public class BaseController : ControllerBase
    {
        [HttpGet]
        protected async Task<IActionResult> CreatePagedResult<T>(IGenericRepository<T> repository, 
            IBaseSpecification<T> specification, int pageSize, int pageIndex) where T : BaseEntity
        {
            var items = await repository.GetAllWithSpecificationAsync(specification);
            var count = await repository.CountAsync(specification);

            var paginatedResult = new Pagination<T>(pageIndex, pageSize, count, items);

            return Ok(paginatedResult);
        }
    }
}
