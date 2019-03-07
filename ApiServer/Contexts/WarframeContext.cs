using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Warframe.Data;

namespace ApiServer.Contexts
{
    public class WarframeContext : DbContext
    {

        public DbSet<WeaponData> Weapons { get; set; }

        public WarframeContext(DbContextOptions<WarframeContext> options) :
            base(options)
        {

        }
    }
}
