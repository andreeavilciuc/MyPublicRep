﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace IssueTracker.Models
{
    public class AdminViewModel
    {
        public class RoleViewModel
        {
            public string Id { get; set; }
            [Required]
            [Display(Name = "RoleName")]
            public string Name { get; set; }
        }
    }
}