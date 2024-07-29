using EmpSkillLibrary.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmpSkillLibrary.Repos
{
    public class EmpSkillRepo : IEmpSkillRepo
    {
        EmpSkillDBContext ctx = new EmpSkillDBContext();

        public async Task DeleteSkill(string SkillId, string EmpId)
        {
            try
            {
                EmpSkill edel = await GetOneSkill(SkillId, EmpId);
                ctx.EmpSkills.Remove(edel);
                await ctx.SaveChangesAsync();
            }
            catch
            {
                throw new EmpSkillException("No such skill is present for given Employee");
            }
        }

        public async Task<Employee> EmpDetails(string EmpId)
        {
            try
            {
                Employee emp = await (from e in ctx.Employees where e.EmpId == EmpId select e).FirstAsync();
                return emp;
            }
            catch(Exception ex) 
            {
                throw new Exception(ex.Message);
            }
        }

       public async Task<Skill> SkillDetails(string skillId)
        {
            try
            {
                Skill skills = await (from s in ctx.Skills where s.SkillId == skillId select s).FirstAsync();
                return skills;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<List<Employee>> GetAllEmployees()
        {
            List<Employee> employees = await ctx.Employees.ToListAsync();
            if (employees.Count > 0)
            {
                return employees;
            }
            else
            {
                throw new EmpSkillException("No Employees List are available");
            }
        }

        public async Task<List<EmpSkill>> GetAllEmpSkills()
        {
            List<EmpSkill> empSkills = await ctx.EmpSkills.ToListAsync();
            if (empSkills.Count > 0)
            {
                return empSkills;
            }
            else
            {
                throw new EmpSkillException("No Employee skills are available");
            }
        }

        public async Task<List<Skill>> GetAllSkills()
        {
            List<Skill>skills=await ctx.Skills.ToListAsync();
            if (skills.Count > 0)
            {
                return skills;
            }
            else
            {
                throw new EmpSkillException("No Skills are available");
            }
        }

        public async Task<List<EmpSkill>> GetByEmpId(string EmpId)
        {
            List<EmpSkill> eskill = await (from es in ctx.EmpSkills where es.EmpId == EmpId select es).ToListAsync();
            if (eskill.Count > 0)
            {  
                return eskill;
            }               
            else
            {
                throw new EmpSkillException("No Skill available for given Employee ID");
            }                
        }

        public async Task<List<EmpSkill>> GetBySkillId(string SkillId)
        {
            List<EmpSkill> eskill = await (from es in ctx.EmpSkills where es.SkillId == SkillId select es).ToListAsync();
            if (eskill.Count > 0)
            {
                return eskill;
            }
            else
            {
                throw new EmpSkillException("No Employees are available for given Skill Id");
            }
        }

        public async Task<EmpSkill> GetOneSkill(string SkillId, string EmpId)
        {
            try
            {
                EmpSkill eskill = await (from es in ctx.EmpSkills where es.SkillId == SkillId && es.EmpId == EmpId select es).FirstAsync();
                return eskill;
            }
            catch (Exception e)
            {
                throw new EmpSkillException("No skills are available for given Employee ID and Skill ID");
            }

        }

        public async Task InsertSkill(EmpSkill empskill)
        {
            await ctx.EmpSkills.AddAsync(empskill);
            await ctx.SaveChangesAsync();
        }

       
        public async Task UpdateSkill(string SkillId, string EmpId, EmpSkill empskill)
        {
            try
            {
                EmpSkill eskill = await GetOneSkill(SkillId, EmpId);
                eskill.SkillExperience = empskill.SkillExperience;
                await ctx.SaveChangesAsync();
            }
            catch 
            {
                throw new EmpSkillException("Invalid Skill Id or Employee Id to update");
            }
        }
    }
}
