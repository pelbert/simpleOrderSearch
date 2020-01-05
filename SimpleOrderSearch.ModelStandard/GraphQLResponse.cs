using System.Collections.Generic;

namespace SimpleOrderSearch.Model
{
    public class GraphQLResponse<T>
    {
        public PageInfo PageInfo { get; set; }

        public List<Edge<T>> Edges { get; set; }

        public int TotalCount { get; set; }
    }

    public class PageInfo
    {
        public bool HasNextPage { get; set; }
        public bool HasPreviousPage { get; set; }
        public string StartCursor { get; set; }
        public string EndCursor { get; set; }
    }

    public class Edge<T>
    {
        public string Cursor { get; set; }
        public T Node { get; set; }
    }
}
