using System;
using System.Linq;
using System.Linq.Dynamic.Core;
using Domain.Entities;
using Infrastructure.Extensions.Utility;

namespace Infrastructure.Extensions
{
    public static class AccountExtensions
    {
        public static IQueryable<Account> FilterAccounts(this IQueryable<Account> accounts, DateTime periodFrom,
            DateTime periodTo)
        {
            return accounts.Where(a => (a.CreatedAt.Date >= periodFrom && a.CreatedAt.Date <= periodTo));
        }

        public static IQueryable<Account> Search(this IQueryable<Account> accounts, string searchTerm)
        {
            if (string.IsNullOrWhiteSpace(searchTerm))
                return accounts;

            var lowerCaseSearchTerm = searchTerm.Trim().ToLower();

            return accounts.Where(a =>
                a.Name.ToLower().Contains(lowerCaseSearchTerm) || a.AccountType.ToLower().Contains(lowerCaseSearchTerm));
        }

        public static IQueryable<Account> Sort(this IQueryable<Account> accounts, string orderByQueryTerm)
        {
            if (string.IsNullOrWhiteSpace(orderByQueryTerm))
                return accounts.OrderBy(a => a.Name);

            var orderQuery = OrderByQueryBuilder.CreateOrderQuery<Account>(orderByQueryTerm);
            
            if (string.IsNullOrWhiteSpace(orderQuery))
                return accounts.OrderBy(a => a.Name);

            return accounts.OrderBy(orderQuery);
        }
    }
}