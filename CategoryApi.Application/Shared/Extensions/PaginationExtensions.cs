using CategoryApi.Application.Shared.Models;
using Microsoft.EntityFrameworkCore;

namespace CategoryApi.Application.Shared.Extensions;

public static class PaginationExtensions
{   
    public static  async Task<PagedList<T>> ToPagedListAsync<T>(this IQueryable<T> query, PaginationFilter paginationFilter)
    {
        if (query == null)
            throw new ArgumentNullException(nameof(query));

        if (paginationFilter == null)
            throw new ArgumentNullException(nameof(paginationFilter));

        int skip = (paginationFilter.PageNumber - 1) * paginationFilter.PageSize;
        
        List<T> data = await query
            .Skip(skip)
            .Take(paginationFilter.PageSize)
            .ToListAsync();
            
        int totalRecords = await query.CountAsync();
        
        int totalPages = totalRecords > 0 ? (int) Math.Ceiling((double) totalRecords / paginationFilter.PageSize) : 0;

        return new PagedList<T>(data)
        {
            PageNumber = paginationFilter.PageNumber,
            PageSize = paginationFilter.PageSize,
            TotalRecords = totalRecords,
            TotalPages = totalPages,
            NextPage = paginationFilter.PageNumber < totalPages ? paginationFilter.PageNumber + 1 : default(int?),
            PreviousPage = paginationFilter.PageNumber > 1 ? paginationFilter.PageNumber - 1 : default(int?)
        };
    }
}