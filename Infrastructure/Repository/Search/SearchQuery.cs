using System.Linq.Expressions;

namespace Infrastructure.Repository.Search
{
    public class SearchQuery
    {
        public string? Filter { get; set; } // filter table
        public string? OrderBy { get; set; } // Table name
        public string? OrderByDirection { get; set; } // "asc" o "desc"
        public int? Skip { get; set; } // Skip
        public int? Take { get; set; } // Take
        public string[]? Includes { get; set; } // Joins
    }
}
