using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuthDemo.Models
{
    public class AccountRate
    {//https://youtu.be/qqfvw6vN1n4 - Appreciating purpose of AccountRate
        public int AccountRateId { get; set; }
        public int CustomerAccountId { get; set; }
        public CustomerAccount CustomerAccount { get; set; }
        public decimal RatePerHour { get; set; }
        public DateTime EffectiveStartDate { get; set; }
        //Let the EffectiveEndDate nullable
        public DateTime? EffectiveEndDate { get; set; }
    }
}
