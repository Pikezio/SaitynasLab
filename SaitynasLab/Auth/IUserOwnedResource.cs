using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SaitynasLab.Auth
{
    public interface IUserOwnedResource
    {
        string UserId { get; }
    }
}
