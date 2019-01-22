using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuthDemo.Models
{
    public class TimeSheet
    {
        public int TimeSheetId { get; set; }
        public DateTime MonthAndYear { get; set; } //Stores  yyyy-mm-01 (1st of any month or year)
        public Decimal RatePerHour { get; set; } //Copy over the rate per hour charge from the CustomerAccount
        public List<TimeSheetDetail> TimeSheetDetails { get; set; }

        //Having InstructorId and Instructor navigation property
        //here is due to the need of many to one relationship between
        //TimeSheet entity type and the Instructor entity type.
        //I still wondered from time to time whether there is a need to delete away the
        //CreatedBy and the CreatedById property because both usually share the same values.
        //Eventually, I keep both of them as "insurance".
        public int InstructorId { get; set; }
        public UserInfo Instructor { get; set; }

        public int CreatedById { get; set; }
        public UserInfo CreatedBy { get; set; }
        public DateTime CreatedAt { get; set; }

        //I felt like removing these 3 properties. After some thought that the admin
        //might step in to make changes on what the instructor has entered. I think I better
        //keep it.
        public int UpdatedById { get; set; }
        public UserInfo UpdatedBy { get; set; }
        public DateTime UpdatedAt { get; set; }

        public DateTime? VerifiedAndSubmittedAt { get; set; }
        public int CheckedById { get; set; }

        public UserInfo ApprovedBy { get; set; }
        public int? ApprovedById { get; set; }
        public DateTime? ApprovedAt { get; set; }

    }
}
