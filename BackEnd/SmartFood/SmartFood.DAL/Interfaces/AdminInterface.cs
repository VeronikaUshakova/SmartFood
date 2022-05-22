using Microsoft.EntityFrameworkCore.Infrastructure;
using SmartFood.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Text;
using System.Threading.Tasks;

namespace SmartFood.DAL.Interfaces
{
    public interface AdminInterface
    {
        Admin Get();
        DatabaseFacade ContextConnection();
    }
}
