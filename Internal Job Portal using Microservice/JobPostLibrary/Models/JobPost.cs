using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace JobPostLibrary.Models;

public partial class JobPost
{
    [DisplayName("Post ID")]
    public int PostId { get; set; }

    [DisplayName("Job ID")]
    public string? JobId { get; set; }

    [DisplayName("Post Date")]
    [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
    public DateTime PostDate { get; set; }

    [DisplayName("Last Date")]
    [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
    public DateTime LastDate { get; set; }

    [Range(1, 100, ErrorMessage = "Vacancy should be More than 0")]
    public int? Vacancies { get; set; }
    public virtual Job? Job { get; set; }
}
