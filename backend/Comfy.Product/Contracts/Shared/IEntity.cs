using System;
using System.Collections.Generic;
using System.Text;

namespace Comfy.Product.Contracts.Shared
{
    public interface IEntity
    {
        int Id { get; set; }
        bool Deleted { get; set; }
    }
}
