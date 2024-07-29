using JobPostLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobPostLibrary.Repo
{
    public interface IJobPostRepo
    {
        Task<List<JobPost>> GetAllJobPost();
        Task<List<JobPost>> GetByJobId(string jobid);
        Task<List<JobPost>> GetByPostDate(DateTime pdate);
        Task<List<JobPost>> GetByLastDate(DateTime ldate);
        Task<JobPost> GetBypostId(int pid);
        Task InsertJobPost(JobPost jobPost);
        Task UpdateJobPost(int postid, JobPost post);
        Task DeleteJobPost(int postid);
        Task<List<Job>> GetAllJob();
        Task<Job> GetJobDetails(string jobid);

    }
}
