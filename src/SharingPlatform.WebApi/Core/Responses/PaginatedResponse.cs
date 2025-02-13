using SharingPlatform.WebApi.Core.Requests;

namespace SharingPlatform.WebApi.Core.Responses;

public sealed record PaginatedResponse<T>(IEnumerable<T> Items, int TotalCount, int Page, int PageSize);

public static class PaginatedResponse
{
    public static PaginatedResponse<TOut> From<TIn, TOut>(PaginatedRequest request, IQueryable<TIn> queryable, Func<TIn, TOut> mapper)
    {
        var items = queryable.Skip((request.Page - 1) * request.PageSize).Take(request.PageSize);
        var mappedItems = items.AsEnumerable().Select(mapper);
        
        return new PaginatedResponse<TOut>(
            mappedItems,
            queryable.Count(),
            request.Page,
            request.PageSize);
    }
    
    public static PaginatedResponse<T> From<T>(PaginatedRequest request, IQueryable<T> queryable)
    {
        var items = queryable.Skip((request.Page - 1) * request.PageSize).Take(request.PageSize);
        
        return new PaginatedResponse<T>(
            items,
            queryable.Count(),
            request.Page,
            request.PageSize);
    }
}