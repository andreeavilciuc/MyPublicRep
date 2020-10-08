using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace IssueTracker.Models
{
    public class Comment
    {
        public int ID { get; set; }

        [ForeignKey ("Issue")]
        public int IssueID { get; set; }

        [StringLength(200)]
        public string Description { get; set; }

        [DataType(DataType.Date), DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}")]
        public DateTime Date { get; set; }
        public virtual ApplicationUser Submitter { get; set; }

        public virtual Issue Issue { get; set; }
    }
}