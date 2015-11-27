﻿using System.Data.Common;
using Microsoft.AspNet.Identity.EntityFramework;

namespace GWHolidayBookingWeb.DataAccess.Identity
{
    public class IdentityContext : IdentityDbContext<IdentityEmployee>, IIdentityContext
    {
        public IdentityContext()
            : base("ConnStringDb1")
        {
            Configuration.LazyLoadingEnabled = true;
        }

        public IdentityContext(DbConnection connection)
            : base(connection, true)
        {
        }
    }
}