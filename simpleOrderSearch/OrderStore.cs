using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;

namespace simpleOrderSearch
{
    public class OrderStore
    {
        public static OrderStore instance = new OrderStore();
        private Order[] store;

        private OrderStore()
        {
            string json = System.IO.File.ReadAllText(@"../data/orderInfo.json");
            store = JsonConvert.DeserializeObject<Order[]>(json);
        }

        internal IEnumerable<Order> fetch(long id, DateTime date, int page, int size)
        {
            //size and page of 0 means that the user wants all instances that meet the criteria
            if (page == 0 && size == 0)
            {
                return store.Where(q => q.OrderId == id && q.CompletionDte == date);
            }
            else
            {
                //since the user has opted for pagination, we change the number of items taken to the default page size of 25 if page size is not initially given
                size = (size == 0) ? 25 : size;
                var skipped = page * size;
                return store.Where(q => q.OrderId == id && q.CompletionDte == date).Skip(skipped).Take(size);
            }
        }

        internal IEnumerable<Order> fetch(long msa, long status, DateTime date, int page, int size)
        {
            //size of 0 means that the user wants all instances that meet the criteria
            if (page == 0 && size == 0)
            {
                return store.Where(q => q.Msa == msa && q.Status == status && q.CompletionDte == date);
            }
            else
            {
                //since the user has opted for pagination, we change the number of items taken to the default page size of 25 if page size is not initially given
                size = (size == 0) ? 25 : size;
                var skipped = page * size;
                return store.Where(q => q.Msa == msa && q.Status == status && q.CompletionDte == date).Skip(skipped).Take(size);
            }
        }
    }
}
