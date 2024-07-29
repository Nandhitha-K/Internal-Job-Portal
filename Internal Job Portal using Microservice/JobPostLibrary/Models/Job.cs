using System;
using System.Collections.Generic;

namespace JobPostLibrary.Models;

public partial class Job
{
    public string JobId { get; set; } = null!;
    public virtual ICollection<JobPost> JobPosts { get; set; } = new List<JobPost>();
    public string JobTitle { get; set; }
}
