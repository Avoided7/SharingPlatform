namespace SharingPlatform.Domain.Helpers;

public static class PaginatedList
{
	public static PaginatedList<T> From<T>(IEnumerable<T> items, int page, int pageSize)
	{
		var itemsList = items.ToList();
		var totalItems = itemsList.Count;
		var paginatedItems = itemsList
			.Skip((page - 1) * pageSize)
			.Take(pageSize);

		return new PaginatedList<T>(paginatedItems, page, pageSize, totalItems);
	}

	public static PaginatedList<T> From<T>(IQueryable<T> items, int page, int pageSize)
	{
		var totalItems = items.Count();
		var paginatedItems = items
			.Skip((page - 1) * pageSize)
			.Take(pageSize);

		return new PaginatedList<T>(paginatedItems, page, pageSize, totalItems);
	}
}

public sealed record PaginatedList<T>(IEnumerable<T> Items, int Page, int PageSize, int TotalItems)
{
	public PaginatedList<TOut> ChangeType<TOut>(Func<T, TOut> func)
	{
		var items = Items.Select(func);

		return new PaginatedList<TOut>(items, Page, PageSize, TotalItems);
	}
}