using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using EmpSkillLibrary.Models;
using EmpSkillLibrary.Repos;
namespace EmpSkillWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmpSkillController : ControllerBase
    {
        IEmpSkillRepo repo;
        public EmpSkillController(IEmpSkillRepo empSkillsRepo)
        {
            repo = empSkillsRepo;
        }

        [HttpGet]
        public async Task<ActionResult> GetAllEmpSkill()
        {
            List<EmpSkill> emps = await repo.GetAllEmpSkills();
            return Ok(emps);
        }

        [HttpGet("{SkillId}/{EmpId}")]
        public async Task<ActionResult> GetOne(string SkillId, String EmpId)
        {
            try
            {
                EmpSkill eskill = await repo.GetOneSkill(SkillId, EmpId);
                return Ok(eskill);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("GetBySkillId/{SkillId}")]
        public async Task<ActionResult> GetBySkillId(string SkillId)
        {
            try
            {
                List<EmpSkill> eskills = await repo.GetBySkillId(SkillId);
                return Ok(eskills);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("GetByEmpId/{EmpId}")]
        public async Task<ActionResult> GetByEmpId(string EmpId)
        {
            try
            {
                List<EmpSkill> eskills = await repo.GetByEmpId(EmpId);
                return Ok(eskills);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public async Task<ActionResult> Insert(EmpSkill empskill)
        {
            try
            {
                await repo.InsertSkill(empskill);
                return Created($"api/EmpSkill/{empskill.SkillId}/{empskill.EmpId}", empskill);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{SkillId}/{EmpId}")]
        public async Task<ActionResult> Update(string SkillId, String EmpId, EmpSkill empSkill)
        {
            try
            {
                await repo.UpdateSkill(SkillId, EmpId, empSkill);
                return Ok(empSkill);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{SkillId}/{EmpId}")]
        public async Task<ActionResult> Delete(string SkillId, String EmpId)
        {
            try
            {
                await repo.DeleteSkill(SkillId, EmpId);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("EmpDetails/{empid}")]
        public async Task<ActionResult> Empdetails(string EmpId)
        {
            try
            {
                Employee emp =await repo.EmpDetails(EmpId);
                return Ok(emp);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("SkillDetails/{skillId}")]
        public async Task<ActionResult> Skilldetails(string SkillId)
        {
            try
            {
                Skill sk =await repo.SkillDetails(SkillId);
                return Ok(sk);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
