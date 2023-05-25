using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mladim.Domain.Models.Result;

public class Result
{
    public string Message { get; set; }
    public bool IsSucceed { get; set; }

    public Result()
    {

    }
    public Result(bool isSucceed, string message = "Uspešno izvedeno")
    {
        Message = message;
        IsSucceed = isSucceed;
    }
}

public class Result<T> : Result
{
    public T? Value { get; set; }

    public Result()
    {

    }

    public Result(T? value, bool isSucceed, string message = "Uspešno izvedeno") : base(isSucceed, message)
    {
        this.Value = value;
    }
    public Result(string errorMessage) : base(false, errorMessage)
    {
        
    }
       
}