using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Advertisements_Mvc.Scripts
{
    interface Validator
    {
        bool IsValid();
        string Message { get; }
    }
}
