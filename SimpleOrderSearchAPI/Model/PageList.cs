using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SimpleOrderSearchAPI.Model
{
    public class PageList<T> : List<T>
    {
        [JsonProperty("currentPage")]
        public int CurrentPage { get; set; } = 1;
        [JsonProperty("itemPerPage")]
        public int ItemPerPage { get; set; } = 2;
        [JsonProperty("totalItems")]
        public int TotalItem { get; set; }
        [JsonProperty("totalPages")]
        public int TotalPages { get; set; }
        [JsonProperty("offset")]
        public int Offset { get; set; }

        public PageList(List<T> items, int count, int pageNumber, int pageSize, int offset)
        {
            TotalItem = count;
            ItemPerPage = pageSize;
            Offset = offset;
            CurrentPage = pageNumber;
            TotalPages = (int)Math.Ceiling(count  / (double)pageSize);
            this.AddRange(items);
        }
        public static PageList<T> CreateAsync(List<T> source, int pageNumber, int pageSize, int offset)
        {
            var count = (source.Count() - offset) < 0 ? 0 : source.Count() - offset;
            var items = source.Skip((pageNumber - 1) * pageSize + offset).Take(pageSize).ToList();
            return new PageList<T>(items, count, pageNumber, pageSize, offset);
        }
    }
}
