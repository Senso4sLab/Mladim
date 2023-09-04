using Azure.Core;
using Mladim.Application.Contracts.File;
using Mladim.Domain.Models;

namespace Mladim.WebAPI.Services;

public class FileApiService : IFileApiService
{
    private IWebHostEnvironment Env { get; set; }
    public FileApiService(IWebHostEnvironment env) => Env = env;    
       
    public async Task<string> AddFileAsync(byte[] file, string folder, string fileName)
    {
        //Console.WriteLine("Start point AddFileAsync");

        //if (Env != null)
        //    Console.WriteLine($"{Env.WebRootPath}");


        string filePath = Path.Combine(Env.WebRootPath, "Files", folder);
        
        string trustedFileName = $"{Path.GetRandomFileName()}.{Path.GetExtension(fileName)}";
        string trustedPath = Path.Combine(filePath, trustedFileName);


        //Console.WriteLine("Start stream");

        //try
        {
            using (var stream = File.OpenWrite(trustedPath))
            {
                stream.Seek(0, SeekOrigin.Begin);
                await stream.WriteAsync(file, 0, file.Length);
            }
        }
        //catch(Exception ex) 
        //{
        //    Console.WriteLine($"Error: {ex.Message}");
        //}

        //Console.WriteLine("End stream");


        return trustedFileName;        
    }


    public bool DeleteFile(string fileUrl)
    {
        string filePath = Path.Combine(Env.WebRootPath, fileUrl);     

        if (File.Exists(filePath))
        {
            File.Delete(filePath);
            return true;
        }
        return false;
    }

    //public bool DeleteFile(string trustedFileName, string folder)
    //{
    //    string filePath = Path.Combine(Env.WebRootPath, "Files", folder);
    //    string trustedPath = Path.Combine(filePath, trustedFileName);

    //    if (File.Exists(trustedPath))
    //    {
    //        File.Delete(trustedPath);
    //        return true;
    //    }
    //    return false;
    //}
}
