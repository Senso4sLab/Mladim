using Mladim.Domain.Models;
using Mladim.Infrastracture.Persistance;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mladim.Infrastracture.Repositories;

public class ProjectRepository : GenericRepository<Project>
{
	public ProjectRepository(ApplicationDbContext context):base(context)
	{

	}
}
