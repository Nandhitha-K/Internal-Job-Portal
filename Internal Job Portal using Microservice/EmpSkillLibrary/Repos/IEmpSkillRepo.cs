using EmpSkillLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmpSkillLibrary.Repos
{
    public interface IEmpSkillRepo
    {
        Task<List<EmpSkill>> GetAllEmpSkills();
        Task<EmpSkill> GetOneSkill(string SkillId, String EmpId);
        Task<List<EmpSkill>> GetBySkillId(string SkillId);
        Task<List<EmpSkill>> GetByEmpId(string EmpId);
        Task<List<Employee>> GetAllEmployees();
        Task<List<Skill>> GetAllSkills();
        Task InsertSkill(EmpSkill empskill);
        Task UpdateSkill(string SkillId, string EmpId, EmpSkill empskill);
        Task DeleteSkill(string SkillId, string EmpId);
        Task<Employee> EmpDetails( string EmpId);
        Task<Skill> SkillDetails(string SkillId);
    }
}
