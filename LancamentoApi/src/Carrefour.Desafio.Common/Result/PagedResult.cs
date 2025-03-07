using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Carrefour.Desafio.Common.Result
{

    /// <summary>
    /// Represents a paginated result set.
    /// </summary>
    /// <typeparam name="T">The type of items in the result set.</typeparam>
    public class PagedResult<T>
    {
        /// <summary>
        /// List of items in the current page.
        /// </summary>
        public IEnumerable<T> Data { get; set; } = new List<T>();

        /// <summary>
        /// Total number of items across all pages.
        /// </summary>
        public int TotalItems { get; set; }

        /// <summary>
        /// Current page number.
        /// </summary>
        public int CurrentPage { get; set; }

        /// <summary>
        /// Total number of pages available.
        /// </summary>
        public int TotalPages { get; set; }



        /// <summary>
        /// Default constructor.
        /// </summary>
        public PagedResult() { }


        /// <summary>
        /// Constructor with parameters.
        /// </summary>
        public PagedResult(IEnumerable<T> data, int totalItems, int totalPages)
        {
            Data = data ?? new List<T>();
            TotalItems = totalItems;
            TotalPages = totalPages;
        }

        /// <summary>
        /// Constructor with parameters.
        /// </summary>
        public PagedResult(IEnumerable<T> data, int totalItems, int currentPage, int totalPages)
        {
            Data = data ?? new List<T>();
            TotalItems = totalItems;
            CurrentPage = currentPage;
            TotalPages = totalPages;
        }
    }
}
