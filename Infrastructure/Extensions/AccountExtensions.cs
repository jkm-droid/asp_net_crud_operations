using System;
using System.Linq;
using Domain.Entities;
using Shared.DataTransferObjects;

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
    }
}