using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SharingPlatform.Application.Abstractions;
using SharingPlatform.WebApi.Core.Requests;
using SharingPlatform.WebApi.Core.Responses;
using SharingPlatform.WebApi.Core.Tags.Requests;

namespace SharingPlatform.WebApi.Core.Tags;

[ApiController]
[Route("api/tags")]
public sealed class TagsController(ITagsService tagsService) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> Get(
        [FromQuery] PaginatedRequest request)
    {
        var tags = tagsService.GetTags();
        var response = PaginatedResponse.From(request, tags);
        
        return Ok(response);
    }
    
    [Authorize, HttpPost]
    public async Task<IActionResult> Create(
        [FromBody] CreateTagRequest request)
    {
        var tag = request.ToModel();
        await tagsService.CreateTagAsync(tag);
        
        return NoContent();
    }
    
    [Authorize, HttpPut]
    public async Task<IActionResult> Update(
        [FromBody] UpdateTagRequest request)
    {
        var tag = request.ToModel();
        await tagsService.UpdateTagAsync(tag);
        
        return NoContent();
    }
    
    [Authorize, HttpDelete]
    public async Task<IActionResult> Remove(
        [FromBody] DeleteTagRequest request)
    {
        await tagsService.DeleteTagAsync(request.Id);
        
        return NoContent();
    }
}