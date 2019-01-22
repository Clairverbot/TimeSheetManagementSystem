using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuthDemo.Models
{
    public class TimeSheetDetailSignature
    {
        public int TimeSheetDetailSignatureId { get; set; }//primary key, auto-number field property
        public byte[] Signature { get; set; }//property for binary signature image
        public int TimeSheetIDetailId { get; set; }//foreign key property
        public TimeSheetDetail TimeSheetDetail { get; set; }//navigation property
    }
}
