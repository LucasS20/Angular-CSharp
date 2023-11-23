using System.Text.Json;

namespace Proeventos.Extentions;

public static class Pagination
{
    public static void AddPagination(this HttpResponse response,
        int currentPage, int itemsPerPage, int totalItems, int totalPages)
    {
        var options = new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase
        };
        response.Headers.Add("Pagination",
            JsonSerializer.Serialize(new { currentPage = currentPage, itemsPerPage = itemsPerPage }, options));
        
        response.Headers.Add("Acess-Control-Expose-Headers",nameof(Pagination));
    }
}