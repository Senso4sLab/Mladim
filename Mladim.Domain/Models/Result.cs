using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mladim.Domain.Models;



public class Result<T>
{
    public T? Value { get; set; } = default!;

    public string Message { get; set; } = string.Empty;
    public bool Succeeded { get; set; }

    public Result() 
    {

    }

    private Result(string message, bool isSucceed) 
    {
        this.Message = message;
        this.Succeeded = isSucceed;       
    }

    private Result(T? value, string message) 
    {
        this.Value = value;
        this.Succeeded = true;  
    }  

    public static Result<T> Error(string message) =>
        new Result<T>(message, false);

    public static Result<T> Success(T value, string message = "Uspešno izvedeno") =>
        new Result<T>(value, message);

}