using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartHall.Application.Authentication
{
	public interface IJwtTokenGenerator
	{
		public string GenerateToken(IdentityUser user);
	}
}
