using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ApplyJobLibrary.Models;
using Microsoft.EntityFrameworkCore;

namespace ApplyJobLibrary.Repos
{
    public class ApplyJobRepo:IApplyJob
    {
        ApplyJobDBContext ctx = new ApplyJobDBContext();
        public async Task DeleteApplyJob(int postId, string empId)
        {
            try
            {
                ApplyJob a = await GetApplication(postId, empId);
                ctx.ApplyJobs.Remove(a);
                await ctx.SaveChangesAsync();
            }
            catch 
            {
                throw new ApplyJobException("No Application is available for given Employee");
            }
        }

        public async Task<List<ApplyJob>> GetAllApplications()
        {
            List<ApplyJob> applyJobs = await ctx.ApplyJobs.ToListAsync();
            if (applyJobs.Count > 0)
            {
                return applyJobs;
            }
            else
            {
                throw new ApplyJobException("No Applications are available");
            }
        }

        public async Task<ApplyJob> GetApplication(int postId, string empId)
        {
            try
            {
                ApplyJob applyJob = await (from s in ctx.ApplyJobs where s.PostId == postId && s.EmpId == empId select s).FirstAsync();
                return applyJob;
            }
            catch 
            {
                throw new ApplyJobException("No Application is available for given Job");
            }
        }

        public async Task<List<ApplyJob>> GetByEmpId(string empId)
        {
            List<ApplyJob> applyJobs = await (from s in ctx.ApplyJobs where s.EmpId == empId select s).ToListAsync();
            if (applyJobs.Count > 0)
            {
                return applyJobs;
            }
            else
            {
                throw new ApplyJobException("No Applications are available for given Employee");
            }
        }

        public async Task<List<ApplyJob>> GetByPostId(int postId)
        {
            List<ApplyJob> applyJobs = await (from s in ctx.ApplyJobs where s.PostId == postId select s).ToListAsync();
            if (applyJobs.Count > 0)
            {
                return applyJobs;
            }
            else
            {
                throw new ApplyJobException("No Post is available for given job");
            }
        }

        public async Task<List<ApplyJob>> GetByStatus(string status)
        {
            List<ApplyJob> applyJobs = await (from s in ctx.ApplyJobs where s.ApplicationStatus == status select s).ToListAsync();
            if (applyJobs.Count > 0)
            {
                return applyJobs;
            }
            else
            {
                throw new ApplyJobException("No Application is available for given status");
            }
        }

        public async Task<List<ApplyJob>> GetByAppliedDate(DateTime appliedDate)
        {
            List<ApplyJob> applyJobs = await (from s in ctx.ApplyJobs where s.AppliedDate == appliedDate select s).ToListAsync();
            if (applyJobs.Count > 0)
            {
                return applyJobs;
            }
            else
            {
                throw new ApplyJobException("No Application is available for given Date");
            }
        }

        public async Task InsertApplyJob(ApplyJob application)
        {
            await ctx.ApplyJobs.AddAsync(application);
            await ctx.SaveChangesAsync();
        }

        public async Task InsertJobPost(JobPost posts)
        {
            await ctx.JobPosts.AddAsync(posts);
            await ctx.SaveChangesAsync();
        }

        public async Task UpdateApplyJob(int postId, string empId, ApplyJob application)
        {
            try
            {
                ApplyJob applyJob = await GetApplication(postId, empId);
                applyJob.ApplicationStatus = application.ApplicationStatus;
                await ctx.SaveChangesAsync();
            }
            catch 
            {
                throw new ApplyJobException("No post is available to update");
            }
        }

        public async Task<List<JobPost>> GetAllPost()
        {
            List<JobPost> jobPosts = await ctx.JobPosts.ToListAsync();
            if (jobPosts.Count > 0)
            {
                return jobPosts;
            }
            else
            {
                throw new ApplyJobException("No Posts are available");
            }
        }

        public async Task<List<Employee>> GetAllEmpId()
        {
            List<Employee> employees = await ctx.Employees.ToListAsync();
            if (employees.Count > 0)
            {
                return employees;
            }
            else
            {
                throw new ApplyJobException("No Employees are available");
            }
        }
    }
}
