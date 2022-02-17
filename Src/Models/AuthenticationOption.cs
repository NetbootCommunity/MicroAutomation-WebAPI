using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroAutomation.WebAPI.Models;

public class AuthenticationOption
{
    public string Authority { get; set; }
    public bool RequireHttpsMetadata { get; set; } = true;
}