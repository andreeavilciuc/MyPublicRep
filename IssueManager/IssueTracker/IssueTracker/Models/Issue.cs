using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace IssueTracker.Models
{
    public enum IssueStatus
    {
        open, 
        changed,
        closed
    }
public enum IssuePriority
{
    low,
    medium, 
    high
}
    public class Issue
    {
        public int ID { get; set; }
        private DateTime _date = DateTime.Now;

        [DataType(DataType.Date), DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}")]
        public DateTime SubmitDate { get { return _date; }  set { _date = value; } }

        public IssuePriority Priority { get; set; }
        public IssueStatus Status { get; set; }

        [StringLength(50, MinimumLength = 3)]
        public string Subject { get; set; }

        [StringLength(300, MinimumLength = 3)]
        public string Content { get; set; }
        public virtual ApplicationUser Submitter { get; set; }
        public virtual ApplicationUser Agent { get; set; }

        private ICollection<Comment> comments;
        public virtual ICollection<Comment> Comments
        {
            get
            {
                return comments ?? (comments = new List<Comment>());
            }
            set
            {
                comments = value;
            }
        }
    }
}