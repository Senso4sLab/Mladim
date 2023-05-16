using Microsoft.EntityFrameworkCore;
using Mladim.Application.Contract;
using Mladim.Domain.Models;
using Mladim.Infrastracture.Persistance;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mladim.Infrastracture.Repositories;

public class OrganizationRepository : GenericRepository<Organization>
{
	public OrganizationRepository(ApplicationDbContext context) : base(context)
	{

	}

   
}
