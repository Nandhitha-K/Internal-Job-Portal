
using EmpSkillLibrary.Models;
using EmpSkillLibrary.Repos;
using JobPostLibrary.Repo;
using JobPostLibrary.Models;

using Microsoft.AspNetCore.Mvc.Rendering;
using ApplyJobLibrary.Repos;
using ApplyJobLibrary.Models;


namespace IJPMVCApp.Models
{
    public class ForeignKeyHelper
    {
        public static async Task<List<SelectListItem>> GetBySkillId()
        {
            List<SelectListItem> empSkills= new List<SelectListItem>();
            IEmpSkillRepo empSkillRepo = new EmpSkillRepo();
            List<Skill> empskills = await empSkillRepo.GetAllSkills();
            foreach(Skill empskill in empskills)
            {
                empSkills.Add(new SelectListItem { Text = empskill.SkillName+'-'+empskill.SkillId, Value = empskill.SkillId });
            }
            return empSkills;
        }

        public static async Task<List<SelectListItem>> GetByEmpId()
        {
            List<SelectListItem> empIds = new List<SelectListItem>();
            IEmpSkillRepo empIdRepo = new EmpSkillRepo();
            List<EmpSkillLibrary.Models.Employee> employees = await empIdRepo.GetAllEmployees();
            foreach (EmpSkillLibrary.Models.Employee empskill in employees)
            {
                empIds.Add(new SelectListItem { Text = empskill.EmpName+'-'+empskill.EmpId, Value = empskill.EmpId });
            }
            return empIds;
        }

        public static async Task<List<SelectListItem>> GetJobId()
        {
            List<SelectListItem> jobid = new List<SelectListItem>();
            IJobPostRepo jobrepo = new JobPostRepo();
            List<Job> jobs = await jobrepo.GetAllJob();
            foreach (Job jp in jobs)
            {
                jobid.Add(new SelectListItem { Text = jp.JobTitle, Value = jp.JobId });
            }

            return jobid;
        }

        public static async Task<List<SelectListItem>> GetAllPost()
        {
            List<SelectListItem> postid=new List<SelectListItem>();
            IApplyJob applyjob = new ApplyJobRepo();
            List<ApplyJobLibrary.Models.JobPost> posts = await applyjob.GetAllPost();
            foreach (ApplyJobLibrary.Models.JobPost post in posts)
            {
                postid.Add(new SelectListItem { Text = post.PostId.ToString(), Value = post.PostId.ToString() });
            }
            return postid;
        }

        public static async Task<List<SelectListItem>> GetAllEmpId()
        {
            List<SelectListItem> empid = new List<SelectListItem>();
            IApplyJob applyjob = new ApplyJobRepo();
            List<ApplyJobLibrary.Models.Employee> empids = await applyjob.GetAllEmpId();
            foreach (ApplyJobLibrary.Models.Employee emp in empids)
            {
                empid.Add(new SelectListItem { Text = emp.EmpName, Value = emp.EmpId });
            }
            return empid;
        }


    }
}
