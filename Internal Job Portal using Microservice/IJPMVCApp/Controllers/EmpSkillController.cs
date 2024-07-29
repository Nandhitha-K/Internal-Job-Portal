using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using EmpSkillLibrary.Repos;
using EmpSkillLibrary.Models;
using Microsoft.AspNetCore.Authorization;
using JobPostLibrary.Models;
using System.Security.Cryptography;
namespace IJPMVCApp.Controllers
{
    [Authorize]
    public class EmpSkillController : Controller
    {
        IEmpSkillRepo empSkillRepo;
        static HttpClient svc = new HttpClient { BaseAddress = new Uri("http://localhost:5160/EmpSkillSvc/") };
        public async Task<ActionResult> Index()
        {
            string username = User.Identity.Name;
            string role = User.Claims.ToArray()[4].Value;
            string secretKey = "My name is Bond, James Bond the great";
            string token = await svc.GetStringAsync("http://localhost:5160/AuthSvc?userName=" + username + "&role=" + role + "&secretKey=" + secretKey);
            svc.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
            List<EmpSkill> empsskills = await svc.GetFromJsonAsync<List<EmpSkill>>("");
            return View(empsskills);
        }

        public async Task<ActionResult> Details(string SkillId, string EmpId)
        {
            EmpSkill empskill = await svc.GetFromJsonAsync<EmpSkill>($"{SkillId}/{EmpId}");
            return View(empskill);
        }
        

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(EmpSkill empSkill)
        {
            try
            {
                await svc.PostAsJsonAsync<EmpSkill>("", empSkill);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        [Route("EmpSkill/Edite/{SkillId}/{EmpId}")]
        public async Task<ActionResult> Edit(string SkillId, string EmpId)
        {
            EmpSkill empskill = await svc.GetFromJsonAsync<EmpSkill>($"{SkillId}/{EmpId}");
            return View(empskill);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("EmpSkill/Edite/{SkillId}/{EmpId}")]
        public async Task<ActionResult> Edit(string SkillId, string EmpId, EmpSkill empSkill)
        {
            try
            {
                await svc.PutAsJsonAsync<EmpSkill>($"{SkillId}/{EmpId}", empSkill);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        [Route ("EmpSkill/Delete/{SkillId}/{EmpId}")]
        public async Task<ActionResult> Delete(string SkillId, string EmpId)
        {
            EmpSkill empskill = await svc.GetFromJsonAsync<EmpSkill>($"{SkillId}/{EmpId}");
            return View(empskill);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("EmpSkill/Delete/{SkillId}/{EmpId}")]
        public async Task< ActionResult> Delete(string SkillId, string EmpId, IFormCollection collection)
        {
            try
            {
                await svc.DeleteAsync($"{SkillId}/{EmpId}");
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        public async Task<ActionResult>GetBySkillId(string SkillId)
        {
            try
            {
                List<EmpSkill> empSkills = await svc.GetFromJsonAsync<List<EmpSkill>>(""+ "GetBySkillId/" + SkillId);
                return View(empSkills);
            }
            catch
            {
                return View();
            }            
        }

        public async Task<ActionResult> GetByEmpId(string EmpId)
        {
            try
            {
                List<EmpSkill> empSkills = await svc.GetFromJsonAsync<List<EmpSkill>>(""+ "GetByEmpId/" + EmpId);
                return View(empSkills);
            }
            catch
            {
                return View();
            }
        }

        public async Task<ActionResult> Empdetails(string eid)
        {
            try
            {
                Employee emp = await svc.GetFromJsonAsync<Employee>("" + "EmpDetails/" + eid);
                return View(emp);
            }
            catch
            {
                return View();
            }
        }

        public async Task<ActionResult> Skilldetails(string sid)
        {
            try
            {
                Skill sk = await svc.GetFromJsonAsync<Skill>("" + "SkillDetails/" + sid);
                return View(sk);
            }
            catch
            {
                return View();
            }
        }
    }
}
