using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SharingPlatform.Application.Abstractions;
using SharingPlatform.WebApi.Core.Tags.Requests;
using SharingPlatform.WebApi.Models.Requests;

namespace SharingPlatform.WebApi.Core.Tags;

[ApiController]
[Route("api/tags")]
public sealed class TagsController(ITagsService tagsService) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> Get(
        [FromQuery] PaginatedRequest request)
    {
        var paginatedTags = tagsService.Get(request.Page, request.PageSize);
        
        return Ok(paginatedTags);
    }
    
    [Authorize, HttpPost]
    public async Task<IActionResult> Create(
        [FromBody] CreateTagRequest request)
    {
        var tag = request.ToModel();
        await tagsService.CreateAsync(tag);
        
        return NoContent();
    }
    
    [Authorize, HttpPut]
    public async Task<IActionResult> Update(
        [FromBody] UpdateTagRequest request)
    {
        var tag = request.ToModel();
        await tagsService.UpdateAsync(tag);
        
        return NoContent();
    }
    
    [Authorize, HttpDelete]
    public async Task<IActionResult> Remove(
        [FromBody] DeleteTagRequest request)
    {
        await tagsService.DeleteAsync(request.Id);
        
        return NoContent();
    }
}