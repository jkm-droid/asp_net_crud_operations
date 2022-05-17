using System;

namespace Shared.RequestFeatures
{
    public class AccountParameters : RequestParameters
    {
        public DateTime PeriodFrom { get; set; }
        public DateTime PeriodTo { get; set; } = DateTime.Now;
        public bool ValidDateRange => PeriodTo > PeriodFrom;
    }
}