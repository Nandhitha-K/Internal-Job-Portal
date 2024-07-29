using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ApplyJobLibrary.Models;
using ApplyJobLibrary.Repos;

namespace ApplyJobWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ApplyJobController : ControllerBase
    {
        IApplyJob repo;
        public ApplyJobController(IApplyJob arepo)
        {
            repo = arepo;
        }

        [HttpGet]
        public async Task<ActionResult> GetAll()
        {
            List<ApplyJob> applyJobs = await repo.GetAllApplications();
            return Ok(applyJobs);
        }

        [HttpGet("GetByEmpId/{empId}")]
        public async Task<ActionResult> GetByEmpId(string empId)
        {
            List<ApplyJob> applyJobs = await repo.GetByEmpId(empId);
            return Ok(applyJobs);
        }

        [HttpGet("GetByPostId/{postId}")]
        public async Task<ActionResult> GetByPostId(int postId)
        {
            List<ApplyJob> applyJobs = await repo.GetByPostId(postId);
            return Ok(applyJobs);
        }

        [HttpGet("GetByAppliedDate/{appliedDate}")]
        public async Task<ActionResult> GetByAppliedDate(DateTime appliedDate)
        {
            List<ApplyJob> applyJobs = await repo.GetByAppliedDate(appliedDate);
            return Ok(applyJobs);
        }

        [HttpGet("GetByStatus/{status}")]
        public async Task<ActionResult> GetByStatus(string status)
        {
            List<ApplyJob> applyJobs = await repo.GetByStatus(status);
            return Ok(applyJobs);
        }

        [HttpGet("{postId}/{empId}")]
        public async Task<ActionResult> GetApplyJob(int postId, string empId)
        {
            ApplyJob applyJob = await repo.GetApplication(postId, empId);
            return Ok(applyJob);
        }

        [HttpPut("{postId}/{empId}")]
        public async Task<ActionResult> Update(int postId, string empId, ApplyJob applyJob)
        {
            try
            {
                await repo.UpdateApplyJob(postId, empId, applyJob);
                return Ok(applyJob);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{postId}/{empId}")]
        public async Task<ActionResult> Delete(int postId, string empId)
        {
            try
            {
                await repo.DeleteApplyJob(postId, empId);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("JobPost")]
        public async Task<ActionResult> InsertPost(JobPost post)
        {
            try
            {
                await repo.InsertJobPost(post);
                return Created();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }



        [HttpPost]
        public async Task<ActionResult> Insert(ApplyJob applyJob)
        {
            try
            {
                await repo.InsertApplyJob(applyJob);
                return Created($"api/ApplyJob/{applyJob.PostId}/{applyJob.EmpId}", applyJob);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
