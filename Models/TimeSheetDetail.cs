using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuthDemo.Models
{
    public class TimeSheetDetail
    {
        public int TimeSheetDetailId { get; set; }

        public TimeSheetDetailSignature TimeSheetDetailSignature { get; set; }

        public int TimeSheetId { get; set; }
        public TimeSheet TimeSheet { get; set; }

        public int AccountDetailId { get; set; }
        public AccountDetail AccountDetail { get; set; }

        //I have removed the foreign key and navigation property relationship
        //between the TimeSheetDetail and the SessionSynopsis.
        public string SessionSynopsisNames { get; set; }

        public int TimeInInMinutes { get; set; }
        public int TimeOutInMinutes { get; set; }

        public DateTime DateOfLesson { get; set; }

        public bool IsReplacementInstructor { get; set; }

        //The following Properties are mapped to 3 columns to exist to facilitate cleaner SQL queries.
        //The values should be copied from other tables by using code.
        public decimal WageRatePerHour { get; set; }  //The value is copied from InstructorAccount when calculations are done.
        public int OfficialTimeInMinutes { get; set; } //This value is copied from AccountDetail
        public int OfficialTimeOutMinutes { get; set; } //This value is copied from AccountDetail
    }
}
