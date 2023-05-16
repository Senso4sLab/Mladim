using Microsoft.Extensions.DependencyInjection;
using Mladim.Application.Contract;
using Mladim.Application.Contracts;
using Mladim.Infrastracture.Persistance;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Mladim.Infrastracture.Repositories;

public class UnitOfWork : IUnitOfWork
{    
    private ApplicationDbContext Context { get; }
    private Hashtable Repository { get; }

    public UnitOfWork(ApplicationDbContext context)
    {
        this.Context = context; 
        this.Repository = new Hashtable();
    }
    public IGenericRepository<T> GetRepository<T>() where T : class
    {
        var type = typeof(T).Name;       

        if (!this.Repository.ContainsKey(type))
        {
            //var repositoryType = typeof(GenericRepository<>);
            //var repositoryInstance = Activator.CreateInstance(repositoryType.MakeGenericType(typeof(T)), this.Context);

            var typeName = $"{type}Repository";            

            var repositoryInstance = Activator.CreateInstance(Assembly.GetExecutingAssembly().FullName!, typeName);
            
            this.Repository.Add(type, repositoryInstance);
        }

        return (IGenericRepository<T>) this.Repository[type]!;

    }

    public async Task<int> SaveChangesAsync()
    {
       return await this.Context.SaveChangesAsync(); 
    }

    public void Dispose()
    {
       this.Context.Dispose();  
    }
}