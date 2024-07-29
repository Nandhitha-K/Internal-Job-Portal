using ApplyJobLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplyJobLibrary.Repos
{
    public interface IApplyJob
    {
        Task<List<ApplyJob>> GetAllApplications();
        Task<ApplyJob> GetApplication(int postId, string empId);
        Task<List<ApplyJob>> GetByPostId(int postId);
        Task<List<ApplyJob>> GetByEmpId(string empId);
        Task<List<ApplyJob>> GetByStatus(string status);
        Task<List<ApplyJob>> GetByAppliedDate(DateTime appliedDate);

        Task InsertApplyJob(ApplyJob application);
        Task UpdateApplyJob(int postId, string empId, ApplyJob application);
        Task DeleteApplyJob(int postId, string empId);
        Task InsertJobPost(JobPost posts);
        Task<List<JobPost>> GetAllPost();
        Task<List<Employee>> GetAllEmpId();

    }
}
