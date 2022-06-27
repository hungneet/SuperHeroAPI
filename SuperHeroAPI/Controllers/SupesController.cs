using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace SuperHeroAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SupesController : ControllerBase
    {
        
        private readonly DataContext context;

        public SupesController(DataContext context)
        {
            this.context = context;
        }

        [HttpGet]
        public async Task<ActionResult<List<Supes>>> Get()
        {   if (await this.context.Supes.ToListAsync() == null) return BadRequest("No Supes has been added");
            return Ok(await this.context.Supes.ToListAsync());
        }
        [HttpGet("Search")]
        public async Task<ActionResult<Supes>> Get(int id)
        {   var hero = await this.context.Supes.FindAsync(id);
            if (hero == null) return BadRequest("Hero not found");
            return Ok(hero);
        }
        [HttpPost]
        public async Task<ActionResult<List<Supes>>> AddSupe(Supes supe)
        {   
           this.context.Supes.Add(supe);
            await this.context.SaveChangesAsync();
            return Ok(await this.context.Supes.ToListAsync());
        }
        [HttpPut]
        public async Task<ActionResult<List<Supes>>> UpdateSupe(Supes request)
        {
            var supedb = await this.context.Supes.FindAsync( request.Id);
            if (supedb == null) return BadRequest("Hero not found");

            supedb.Name = request.Name;
            supedb.Superpower = request.Superpower;

            await this.context.SaveChangesAsync();
            return Ok(await this.context.Supes.ToListAsync());
            
         
  
        }

        [HttpDelete]
        public async Task<ActionResult<List<Supes>>> Delete(int id)
        {
            var supedb = await this.context.Supes.FindAsync(id);
            if (supedb == null) return BadRequest("Hero not found");

            this.context.Supes.Remove(supedb);
            await this.context.SaveChangesAsync();
            return Ok(await this.context.Supes.ToListAsync());
        }

    }
}