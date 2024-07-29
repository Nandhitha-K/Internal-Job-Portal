using Microsoft.AspNetCore.Connections;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;
using JobPostLibrary;
using JobPostLibrary.Repo;
using JobPostLibrary.Models;
using RabbitMQ.Client;

namespace JobPostWebApi.Controllers
{

    [Route("api/[controller]")]
    [ApiController]

    public class JobPostController : ControllerBase
    {
        IJobPostRepo repo;        
        public JobPostController(IJobPostRepo jobPostRepo)
        {
            repo = jobPostRepo;
        }

        [HttpGet]
        public async Task<ActionResult> GetAllJobPost()
        {
            try
            {
                List<JobPost> post = await repo.GetAllJobPost();
                return Ok(post);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("ByJobId/{jid}")]
        public async Task<ActionResult> GetByJobId(string jid)
        {
            try
            {
                List<JobPost> post = await repo.GetByJobId(jid);
                return Ok(post);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("ByPostId/{pid}")]
        public async Task<ActionResult> GetByPostId(int pid)
        {
            try
            {
                JobPost post = await repo.GetBypostId(pid);
                return Ok(post);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("ByPostDate/{pdate}")]
        public async Task<ActionResult> GetByPostDate(DateTime pdate)
        {
            try
            {
                List<JobPost> post = await repo.GetByPostDate(pdate);
                return Ok(post);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("ByJobDetail/{jid}")]
        public async Task<ActionResult> JobDetails(string jid)
        {
            try
            {
                Job jtitle = await repo.GetJobDetails(jid);
                return Ok(jtitle);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [HttpGet("ByLastDate/{ldate}")]
        public async Task<ActionResult> GetByLastDate(DateTime ldate)
        {
            try
            {
                List<JobPost> post = await repo.GetByLastDate(ldate);
                return Ok(post);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        private void PublishToMessageQueue(string integrationEvent, string eventData)
        {
            var factory = new ConnectionFactory();
            var connection = factory.CreateConnection();
            var channel = connection.CreateModel();
            var body = Encoding.UTF8.GetBytes(eventData);
            channel.BasicPublish(exchange: "JobPostExchange", routingKey: integrationEvent, basicProperties: null, body: body);
        }

        [HttpPost]
        public async Task<ActionResult> InsertJobPost(JobPost post)
        {
            try
            {
                await repo.InsertJobPost(post);
                var integrationEventData = JsonConvert.SerializeObject(new { PostId = post.PostId });
                PublishToMessageQueue("JobPost.Add", integrationEventData);
                return Created($"api/JobPost/{post.PostId}", post);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{postid}")]
        public async Task<ActionResult> UpdatePost(int postid, JobPost post)
        {
            try
            {
                await repo.UpdateJobPost(postid, post);
                return Ok(post);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{pid}")]
        public async Task<ActionResult> DeletePost(int pid)
        {
            try
            {
                await repo.DeleteJobPost(pid);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
