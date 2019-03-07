using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ApiServer.Contexts;
using Warframe.Data;
using Microsoft.AspNetCore.SignalR;
using ApiServer.Hubs;

namespace ApiServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WeaponDataController : ControllerBase
    {
        private readonly WarframeContext _context;
        private readonly IHubContext<LogHub> _loggingContext;

        public WeaponDataController(WarframeContext context, IHubContext<LogHub> loggingContext)
        {
            _context = context;
            _loggingContext = loggingContext;
        }

        // GET: api/WeaponData
        [HttpGet]
        public async Task<ActionResult<IEnumerable<WeaponData>>> GetWeapons()
        {
            return await _context.Weapons.OrderBy(x=>x.Title).ToListAsync();
        }

        // GET: api/WeaponData/5
        [HttpGet("{id}")]
        public async Task<ActionResult<WeaponData>> GetWeaponData(Guid id)
        {
            var weaponData = await _context.Weapons.FindAsync(id);

            if (weaponData == null)
            {
                return NotFound();
            }

            return weaponData;
        }

        // PUT: api/WeaponData/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutWeaponData(Guid id, WeaponData weaponData)
        {
            if (id != weaponData.Id)
            {
                return BadRequest();
            }

            _context.Entry(weaponData).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!WeaponDataExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/WeaponData
        [HttpPost]
        public async Task<ActionResult<WeaponData>> PostWeaponData(WeaponData weaponData)
        {
            _context.Weapons.Add(weaponData);
            await _context.SaveChangesAsync();
            await _loggingContext.Clients.All.SendAsync("ItemLoaded", weaponData);
            return CreatedAtAction("GetWeaponData", new { id = weaponData.Id }, weaponData);
        }

        // DELETE: api/WeaponData/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<WeaponData>> DeleteWeaponData(Guid id)
        {
            var weaponData = await _context.Weapons.FindAsync(id);
            if (weaponData == null)
            {
                return NotFound();
            }

            _context.Weapons.Remove(weaponData);
            await _context.SaveChangesAsync();

            return weaponData;
        }

        private bool WeaponDataExists(Guid id)
        {
            return _context.Weapons.Any(e => e.Id == id);
        }
    }
}
