﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Resolve.Models
{
    public class Approver
    {

        public int CaseID { get; set; }
        public Case Case { get; set; }

        // Case to be approved by
        [Display(Name = "Approver")]
        public string LocalUserID { get; set; }
        public LocalUser LocalUser { get; set; }
        public string LocalGroupID { get; set; }
        public LocalGroup LocalGroup { get; set; }
        // Default values: 0, -1 is Reject, 1 is Approved
        public int Approved { get; set; }
        public int Order { get; set; }

    }
    
}
