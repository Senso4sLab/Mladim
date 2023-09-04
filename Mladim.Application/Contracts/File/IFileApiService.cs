using Mladim.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mladim.Application.Contracts.File;

public interface IFileApiService
{
    //bool DeleteFile(string trustedFileName, string folder);
    Task<string> AddFileAsync(byte[] file, string folder, string fileName);
    bool DeleteFile(string fileUrl);
}
