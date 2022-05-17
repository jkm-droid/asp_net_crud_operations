using System;
using System.Linq;
using System.Linq.Dynamic.Core;
using Domain.Entities;
using Infrastructure.Extensions.Utility;

namespace Infrastructure.Extensions
{
    public static class OwnerExtensions
    {
        // public static IQueryable<Owner> FilterAccounts(this IQueryable<Owner> owners, DateTime periodFrom,
        //     DateTime periodTo)
        // {
        //     return owners.Where(a => (a.CreatedAt.Date >= periodFrom && a.CreatedAt.Date <= periodTo));
        // }

        public static IQueryable<Owner> Search(this IQueryable<Owner> owners, string searchTerm)
        {
            if (string.IsNullOrWhiteSpace(searchTerm))
                return owners;

            var lowerCaseSearchTerm = searchTerm.Trim().ToLower();

            return owners.Where(o =>
                o.Name.ToLower().Contains(lowerCaseSearchTerm) || o.Email.ToLower().Contains(lowerCaseSearchTerm));
        }

        public static IQueryable<Owner> Sort(this IQueryable<Owner> owners, string orderByQueryTerm)
        {
            if (string.IsNullOrWhiteSpace(orderByQueryTerm))
                return owners.OrderBy(a => a.Name);

            var orderQuery = OrderByQueryBuilder.CreateOrderQuery<Owner>(orderByQueryTerm);
            
            if (string.IsNullOrWhiteSpace(orderQuery))
                return owners.OrderBy(a => a.Name);

            return owners.OrderBy(orderQuery);
        }
    }
}