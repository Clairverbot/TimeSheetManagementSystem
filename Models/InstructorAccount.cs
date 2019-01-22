using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuthDemo.Models
{
    public class InstructorAccount
    {
        public int InstructorAccountId { get; set; }
        public int InstructorId { get; set; }
        public UserInfo Instructor { get; set; }
        public int CustomerAccountId { get; set; }
        public string Comments { get; set; }
        public CustomerAccount CustomerAccount { get; set; }

        public decimal WageRate { get; set; }
        //The system will assume that the EffectiveStartDate and EffectiveEndDate is
        //one month cycle. (e.g. 1st of June to 30th of June)
        //If there is NULL value in the EffectiveEndDate, the system will assume that
        //this wage information is going to last forever.
        public DateTime EffectiveStartDate { get; set; }
        public DateTime? EffectiveEndDate { get; set; }
        //When the system provides the functionality to user to
        //start generating monthly invoice, the system have to find the 
        //current InstructorAccount record to fetch wage rate info.
        public bool IsCurrent { get; set; } 
    }
}
