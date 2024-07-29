using ApplyJobLibrary.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace IJPMVCApp.Controllers
{
    [Authorize]
    public class ApplyJobController : Controller
    {
        
        static HttpClient svc = new HttpClient { BaseAddress = new Uri("http://localhost:5160/ApplyJobSvc/") };
        public async Task <ActionResult> Index()
        {
            string username = User.Identity.Name;
            string role = User.Claims.ToArray()[4].Value;
            string secretKey = "My name is Bond, James Bond the great";
            string token = await svc.GetStringAsync("http://localhost:5160/AuthSvc?userName=" + username + "&role=" + role + "&secretKey=" + secretKey);
            svc.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
            List<ApplyJob> applications = await svc.GetFromJsonAsync<List<ApplyJob>>("");
            return View(applications);
        }

        public async Task <ActionResult> Details(int postId,string empId)
        {
            ApplyJob application =await svc.GetFromJsonAsync<ApplyJob>($"{postId}/{empId}");
            return View(application);
        }

        public ActionResult Create()
        {
            ApplyJob app = new ApplyJob();
            app.ApplicationStatus = "Received";
            app.AppliedDate = DateTime.Now;
            return View(app);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(ApplyJob application)
        {          
            application.AppliedDate=Convert.ToDateTime(application.AppliedDate.ToLongDateString());
            application.ApplicationStatus = "Received";
            await svc.PostAsJsonAsync<ApplyJob>("", application);
            return RedirectToAction(nameof(Index));            
        }

        [Authorize(Roles = "Manager,Admin")]
        [Route("ApplyJob/Edit/{postId}/{empId}")]        
        public async Task <ActionResult> Edit(int postId,string empId)
        {
            ApplyJob application=await svc.GetFromJsonAsync<ApplyJob>($"{postId}/{empId}");
            return View(application);
        }

        [Route("ApplyJob/Edit/{postId}/{empId}")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task <ActionResult> Edit(int postId, string empId,ApplyJob application)
        {           
            await svc.PutAsJsonAsync<ApplyJob>($"{postId}/{empId}", application);
            return RedirectToAction(nameof(Index));           
        }

        [Authorize(Roles = "Manager,Admin")]
        [Route("ApplyJob/Delete/{postId}/{empId}")]
        public async Task<ActionResult> Delete(int postId,string empId)
        {
            ApplyJob application = await svc.GetFromJsonAsync<ApplyJob>($"{postId}/{empId}");
            return View(application);
        }

        [Route("ApplyJob/Delete/{postId}/{empId}")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(int postId,string empId,ApplyJob application)
        {            
            await svc.DeleteAsync($"{postId}/{empId}");
            return RedirectToAction(nameof(Index));            
        }

        public async Task <ActionResult> GetDetailsByEmpId (string empId)
        {
            List<ApplyJob> applications = await svc.GetFromJsonAsync<List<ApplyJob>>("" + "GetByEmpId/" + empId);
            return View(applications);
        }

        public async Task<ActionResult> GetDetailsByPostId(int postId)
        {
            List<ApplyJob> applications = await svc.GetFromJsonAsync<List<ApplyJob>>("" + "GetByPostId/" + postId);
            return View(applications);
        }

        public async Task<ActionResult> GetDetailsByAppliedDate(DateTime appliedDate)
        {
            List<ApplyJob> applications = await svc.GetFromJsonAsync<List<ApplyJob>>("" + "GetByAppliedDate/" + appliedDate.ToLongDateString());
            return View(applications);
        }

        public async Task<ActionResult> GetByStatus(string status)
        {
            List<ApplyJob> applications = await svc.GetFromJsonAsync<List<ApplyJob>>("" + "GetByStatus/" + status);
            return View(applications);
        }
    }
}
