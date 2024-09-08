using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartHall.Infrastructure
{
	public sealed class SmartHallDbContext : DbContext
	{
		public SmartHallDbContext(DbContextOptions<SmartHallDbContext> options) : base(options)
		{
		}
	}
}
