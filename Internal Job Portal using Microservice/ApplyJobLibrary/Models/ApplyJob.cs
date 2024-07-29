using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ApplyJobLibrary.Models;

public partial class ApplyJob
{
    [DisplayName("Post ID")]
    public int PostId { get; set; }

    [DisplayName("Employee ID")]
    public string EmpId { get; set; } = null!;

    [DisplayName("Applied Date")]
    [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
    public DateTime AppliedDate { get; set; }

    [DisplayName("Application Status")]
    public string? ApplicationStatus { get; set; }

    public virtual Employee? Emp { get; set; } 

    public virtual JobPost? Post { get; set; } 
}
