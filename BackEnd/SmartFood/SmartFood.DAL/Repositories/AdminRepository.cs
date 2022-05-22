using Microsoft.EntityFrameworkCore;
using SmartFood.DAL.DataContext;
using SmartFood.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore.SqlServer;
using System.IO;
using System.Threading.Tasks;
using SmartFood.DAL.Interfaces;
using System.Data.Common;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace SmartFood.DAL.Repositories
{
    public class AdminRepository : AdminInterface
    {
        private DatabaseContext context;

        public AdminRepository(DatabaseContext context)
        {
            this.context = context;
        }

        public Admin Get()
        {
            Admin admin = Admin.admin;
            return admin;
        }
        public DatabaseFacade ContextConnection()
        {
            return context.Database;
        }
    }
}
