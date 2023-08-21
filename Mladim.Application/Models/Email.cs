using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mladim.Application.Models;

public record Email(string Subject, string HtmlContent, string Receipent, string Sender);

