using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JobPostLibrary.Models;
using JobPostLibrary.Repo;
using Microsoft.EntityFrameworkCore;

namespace JobPostLibrary.Repo
{
    public class JobPostRepo:IJobPostRepo
    {
        JobPostDBContext cxt = new JobPostDBContext();
        public async Task DeleteJobPost(int postid)
        {
            try
            {
                JobPost post = await GetBypostId(postid);
                cxt.Remove(post);
                await cxt.SaveChangesAsync();
            }
            catch
            {
                throw new JobPostException("No Post is available for given Post Id");
            }
        }

        public async Task<List<Job>> GetAllJob()
        {
            List<Job> jobs = await cxt.Jobs.ToListAsync();
            if (jobs.Count > 0)
            {
                return jobs;
            }
            else
            {
                throw new JobPostException("No Jobs are available");
            }
        }

        public async Task<List<JobPost>> GetAllJobPost()
        {
            List<JobPost> jpost = await cxt.JobPosts.ToListAsync();
            if (jpost.Count > 0)
            {
                return jpost;
            }
            else
            {
                throw new JobPostException("No Job Posts are available");
            }
        }

        public async Task<List<JobPost>> GetByJobId(string jobid)
        {
            List<JobPost> jpost = await (from jp in cxt.JobPosts where jp.JobId == jobid select jp).ToListAsync();
            if (jpost.Count > 0)
            {
                return jpost;
            }
            else
            {
                throw new JobPostException("No Job Post are available for given Job Id");
            }
        }

        public async Task<List<JobPost>> GetByLastDate(DateTime ldate)
        {
            List<JobPost> jpost = await (from jp in cxt.JobPosts where jp.LastDate == ldate select jp).ToListAsync();
            if (jpost.Count > 0)
            {
                return jpost;
            }
            else
            {
                throw new JobPostException("There is no Post available for given last date");
            }
        }

        public async Task<List<JobPost>> GetByPostDate(DateTime pdate)
        {
            List<JobPost> jpost = await (from jp in cxt.JobPosts where jp.PostDate == pdate select jp).ToListAsync();
            if (jpost.Count > 0)
            {
                return jpost;
            }
            else
            {
                throw new JobPostException("There is no Post available for given Post date");
            }
        }

        public async Task<JobPost> GetBypostId(int pid)
        {
            try
            {
                JobPost post = await (from jp in cxt.JobPosts where jp.PostId == pid select jp).FirstAsync();
                return post;
            }
            catch
            {
                throw new JobPostException("No Post is available for given Post Id");
            }
        }

        public async Task<Job> GetJobDetails(string jobid)
        {
            try
            {
                Job job = await (from j in cxt.Jobs where j.JobId == jobid select j).FirstOrDefaultAsync();
                return job;
            }
            catch 
            {
                throw new JobPostException("No Job is available for given Job Id");
            }
        }

        public async Task InsertJobPost(JobPost jobPost)
        {
            await cxt.JobPosts.AddAsync(jobPost);
            await cxt.SaveChangesAsync();
        }

        public async Task UpdateJobPost(int postid, JobPost post)
        {
            try
            {
                JobPost jpost = await GetBypostId(postid);
                jpost.PostDate = post.PostDate;
                jpost.LastDate = post.LastDate;
                jpost.Vacancies = post.Vacancies;
                await cxt.SaveChangesAsync();
            }
            catch
            {
                throw new JobPostException("Invalid Post Id to update");
            }
        }
    }
}
