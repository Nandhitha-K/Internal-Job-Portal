using System;
using System.Collections.Generic;

namespace EmpSkillLibrary.Models;

public partial class Skill
{
    public string SkillId { get; set; } = null!;
    public string SkillName { get; set; } = null!;
    public virtual ICollection<EmpSkill> EmpSkills { get; set; } = new List<EmpSkill>();
}
