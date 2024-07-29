using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using JobPostLibrary;
using JobPostLibrary.Models;
using Microsoft.AspNetCore.Authorization;

namespace IJPMVCApp.Controllers
{
    [Authorize]
    public class JobPostController : Controller
    {
        static HttpClient svc = new HttpClient { BaseAddress = new Uri("http://localhost:5160/JobPostSvc/") };
        public async Task<ActionResult> Index()
        {
            string username = User.Identity.Name;
            string role = User.Claims.ToArray()[4].Value;
            string secretKey = "My name is Bond, James Bond the great";
            string token = await svc.GetStringAsync("http://localhost:5160/AuthSvc?userName=" + username + "&role=" + role + "&secretKey=" + secretKey);
            svc.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
            List<JobPost> jpost = await svc.GetFromJsonAsync<List<JobPost>>("");
            return View(jpost);
        }

        public async Task<ActionResult> Details(int pid)
        {
            JobPost post = await svc.GetFromJsonAsync<JobPost>("" + "ByPostId/" + pid);
            return View(post);
        }

        [Authorize(Roles = "Manager,Admin")]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(JobPost jobPost)
        {
            jobPost.PostDate = Convert.ToDateTime(jobPost.PostDate.ToLongDateString());
            jobPost.LastDate = Convert.ToDateTime(jobPost.LastDate.ToLongDateString());
            await svc.PostAsJsonAsync<JobPost>("", jobPost);
            return RedirectToAction(nameof(Index));           
        }

        [Authorize(Roles = "Manager,Admin")]
        [Route("JobPost/Edit/{pid}")]
        public async Task<ActionResult> Edit(int pid)
        {
            JobPost jpost = await svc.GetFromJsonAsync<JobPost>("" + "ByPostId/" + pid);
            return View(jpost);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("JobPost/Edit/{pid}")]
        public async Task<ActionResult> Edit(int pid, JobPost post)
        {           
            await svc.PutAsJsonAsync<JobPost>("" + pid, post);
            return RedirectToAction(nameof(Index));        
        }

        [Authorize(Roles = "Manager,Admin")]
        [Route("JobPost/Delete/{pid}")]
        public async Task<ActionResult> Delete(int pid)
        {
            JobPost post = await svc.GetFromJsonAsync<JobPost>("" + "ByPostId/" + pid);
            return View(post);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("JobPost/Delete/{pid}")]
        public async Task<ActionResult> Delete(int pid, JobPost post)
        {           
            await svc.DeleteAsync("" + pid);
            return RedirectToAction(nameof(Index));           
        }

        public async Task<ActionResult> GetByPostDate(DateTime pdate)
        {
            List<JobPost> post = await svc.GetFromJsonAsync<List<JobPost>>("" + "ByPostDate/" + pdate.ToLongDateString());
            return View(post);
        }           

        public async Task<ActionResult> GetByLastDate(DateTime ldate)
        {           
            List<JobPost> post = await svc.GetFromJsonAsync<List<JobPost>>("" + "ByLastDate/" + ldate.ToLongDateString());
            return View(post);           
        }

        public async Task<ActionResult> GetByJobId(string jid)
        {            
            List<JobPost> post = await svc.GetFromJsonAsync<List<JobPost>>("" + "ByJobId/" + jid);
            return View(post);            
        }

        public async Task<ActionResult> JobDetails(string jid)
        {
            Job job = await svc.GetFromJsonAsync<Job>(""+ "ByJobDetail/" + jid);
            return View(job);
        }
    }
}
