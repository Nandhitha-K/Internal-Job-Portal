using System;
using System.Collections.Generic;

namespace EmpSkillLibrary.Models;

public partial class Employee
{
    public string EmpId { get; set; } = null!;
    public string? EmpName { get; set; }
    public virtual ICollection<EmpSkill> EmpSkills { get; set; } = new List<EmpSkill>();
}
