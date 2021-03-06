﻿namespace StoreSample.Data.Repositories
{
    using Infrastructure;
    using Interfaces;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// Provides the basic implementation of the <see cref="IStoreSampleDataSource"/>. The implementation
    /// is specific to SQL Server and leverages Entity Framework. Therefore has a dependency to the underlying context.
    /// </summary>
    public class SqlStoreSampleDataSource : IStoreSampleDataSource
    {
        private List<Book> books;
        private List<Order> orders;

        StoreSampleDbContext storeSampleDbContext;

        public SqlStoreSampleDataSource()
        {
            this.storeSampleDbContext = new StoreSampleDbContext();
        }

        public List<Book> Books
        {
            get
            {
                this.SyncBookCollectionWithDatabase();

                return this.books;
            }

            private set
            {
                this.books = value;
            }
        }

        public List<Order> Orders
        {
            get
            {
                this.SyncOrderCollectionWithDatabase();

                return this.orders;
            }

            private set
            {
                this.orders = value;
            }
        }

        public Order AddNewOrder(Order order)
        {
            Guard.NotNull(order, "The provided order was null. Cannot create a new order in the order database from a null order.");

            Order newOrder = this.storeSampleDbContext.Orders.Add(order);

            return newOrder;
        }

        public bool SaveChanges()
        {
            int result = this.storeSampleDbContext.SaveChanges();

            return result != 0;
        }

        private void SyncBookCollectionWithDatabase()
        {
            this.Books = this.storeSampleDbContext.Books.ToList();
        }

        private void SyncOrderCollectionWithDatabase()
        {
            this.Orders = this.storeSampleDbContext.Orders.ToList();
        }
    }
}
