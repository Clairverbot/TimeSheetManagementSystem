using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuthDemo.Models
{
    public class AccountDetail
    {
        public int AccountDetailId { get; set; }
        //Note that, 1 is Sunday, 2 is Monday, 3 is Tuesday and 7 is Saturday.
        public int DayOfWeekNumber { get; set; }
        public int StartTimeInMinutes { get; set; }//No choice but to use DateTime due to the fact that you deal with time zone
        public int EndTimeInMinutes { get; set; }//http://stackoverflow.com/questions/538739/best-way-to-store-time-hhmm-in-a-database
        //When the instructor needs to create his timesheet for a particular month, the system
        //need to fetch to correct AccountDetail entity object for a particular month to create the correct list of dates
        //for the user to "select" to create the timesheet data. The server side logic will heavily rely on EffectiveStartDate
        //and EffectiveEndDate
        public DateTime EffectiveStartDate { get; set; }
        public DateTime? EffectiveEndDate { get; set; }
        //There may be special situations that the instructor need to set an AccountDetail record as "not visible" (false)
        //. By setting IsVisible property to false, the record will not be used by the Instructor to see any dates
        //which can be generated from the respective AccountDetail record.
        public bool IsVisible { get; set; } 
        public int CustomerAccountId { get; set; }
        public CustomerAccount CustomerAccount { get; set; }
        public List<TimeSheetDetail> TimeSheetDetails { get; set; }


    }
}
