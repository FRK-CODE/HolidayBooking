using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using HolidayBookingWeb.DataAccess;
using HolidayBookingWeb.DataAccess.Identity;
using HolidayBookingWeb.DataAccess.ViewModels;
using HolidayBookingWeb.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace HolidayBookingWeb.Migrations
{
    internal sealed class ConfigIdentityInitial : DbMigrationsConfiguration<IdentityContext>
    {
        public ConfigIdentityInitial()
        {
            AutomaticMigrationsEnabled = true;
            MigrationsDirectory = @"Migrations\Identity";
            ContextKey = "GWHolidayBookingWeb.DataAccess.Identity.AuthContext";
        }

        protected override void Seed(IdentityContext context)
        {
            var userManager = new UserManager<IdentityEmployee>(new UserStore<IdentityEmployee>(context));
            var rolemanager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(new IdentityContext()));

            if (!rolemanager.RoleExists("admin"))
            {
                var adminRole = new IdentityRole("admin");
                rolemanager.Create(adminRole);
            }
            if (!rolemanager.RoleExists("user"))
            {
                var userRole = new IdentityRole("user");
                rolemanager.Create(userRole);
            }
            if (userManager.FindByName("d.c@gradweb.co.uk") == null)
            {
                var test = new RegisterUserAndEmployeeViewModel();
                test.StaffId = Guid.NewGuid();

                var user = new IdentityEmployee
                {
                    StaffId = test.StaffId,
                    UserName = "d.c@gradweb.co.uk"
                };
                var team = new Team
                {
                    TeamId = Guid.NewGuid(),
                    TeamName = "Test"
                };
                var result = userManager.Create(user, "123123");
                userManager.AddToRole(user.Id, "admin");
                var employee = new Employee
                {
                    FirstName = "Dominic",
                    LastName = "Cooke",
                    HolidayAllowance = 25,
                    RemainingAllowance = 25,
                    HolidayBookings = new List<EmployeeHolidayBooking>(),
                    StaffId = test.StaffId,
                    TeamId = team.TeamId
                };
                var employeeContext = new EmployeeContext();
                employeeContext.Employees.Add(employee);
                employeeContext.Teams.Add(team);
                employeeContext.SaveChanges();
            }
        }
    }
}