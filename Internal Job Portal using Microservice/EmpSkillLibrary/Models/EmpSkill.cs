using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;


namespace EmpSkillLibrary.Models;

public partial class EmpSkill
{

    [DisplayName("Employee ID")]
    public string EmpId { get; set; } = null!;

    [DisplayName("Skill ID")]    
    public string SkillId { get; set; } = null!;

    [DisplayName("Skill Experience")]
    [Range(1, 100, ErrorMessage = "Skill Experience more than 0")]
    public decimal SkillExperience { get; set; }

    public virtual Employee? Emp { get; set; }

    public virtual Skill? Skill { get; set; } 

}
