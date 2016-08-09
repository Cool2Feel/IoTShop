using IoTShop.Common.Logic.Context;
using IoTShop.Common.Logic.Helpers;
using IoTShop.Common.Logic.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.IO;
using System.Linq;

namespace IoTShop.Common.Logic.Migrations
{
    internal sealed class Configuration : DbMigrationsConfiguration<IoTShop.Common.Logic.Context.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(ApplicationDbContext context)
        {
            ReadLinesFromFiles(context.OS, context, @"D:\Documenten\Howest\Semester 4\03 - Server Side Advanced\03 - Design patterns en IoC\Deel 1\UploadContent\OS.txt");
            ReadLinesFromFiles(context.Frameworks, context, @"D:\Documenten\Howest\Semester 4\03 - Server Side Advanced\03 - Design patterns en IoC\Deel 1\UploadContent\ProgrammingFramework.txt");
            ReadLinesFromFiles(context.Devices, context, @"D:\Documenten\Howest\Semester 4\03 - Server Side Advanced\03 - Design patterns en IoC\Deel 1\UploadContent\Devices.txt");
            CreateRole(ApplicationRole.Adminestrator, context);
            CreateRole(ApplicationRole.NormalUser, context);
            CreateUser(new ApplicationUser()
            {
                FirstName = "Hein",
                LastName = "Pauwelyn",
                Email = "hein_pauwelyn@hotmail.com",
                UserName = "hein_pauwelyn@hotmail.com",
                Address = "",
                City = "Menen",
                ZipCode = "8930",
                Country = "Belgium"
            }, context);
        }

        private void CreateUser(ApplicationUser applicationUser, ApplicationDbContext context)
        {
            if (!context.Users.Any(u => u.Email.Equals(applicationUser.Email)))
            {
                UserStore<ApplicationUser> store = new UserStore<ApplicationUser>(context);
                UserManager<ApplicationUser> manager = new UserManager<ApplicationUser>(store);

                manager.Create(applicationUser, "P@ssw0rd");
                manager.AddToRole(applicationUser.Id, ApplicationRole.Adminestrator);
            }
        }

        private void CreateRole(string role, ApplicationDbContext context)
        {
            IdentityResult roleResult;
            RoleManager<IdentityRole> roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));

            if (!roleManager.RoleExists(role))
            {
                roleResult = roleManager.Create(new IdentityRole(role));
            }
        }

        private void ReadLinesFromFiles(DbSet<Device> devices, ApplicationDbContext context, string filename)
        {
            string[] lines = File.ReadAllLines(filename);

            foreach (string line in lines)
            {
                string[] variables = line.Split(';');
                string[] ossen = variables[6].Split('-');
                List<OS> ossenList = new List<OS>();

                foreach (string osid in ossen)
                {
                    int id = Convert.ToInt32(osid);

                    OS theOs = (from os in context.OS
                                where os.ID == id
                                select os).FirstOrDefault<OS>();

                    ossenList.Add(theOs);
                    //context.Entry<OS>(theOs).State = EntityState.Unchanged;
                    context.OS.AddOrUpdate(o => o.Name, theOs);

                    context.SaveChanges();
                }

                int frameworkId = Convert.ToInt32(variables[7]);

                Framework framework = (from f in context.Frameworks
                                       where f.ID == frameworkId
                                       select f).SingleOrDefault<Framework>();

                //context.Entry<Framework>(framework).State = EntityState.Unchanged;
                context.Frameworks.AddOrUpdate(f => f.Name, framework);
                context.SaveChanges();


                devices.AddOrUpdate(device => device.Name, new Device()
                {
                    //ID = Convert.ToInt32(variables[0]),
                    Name = variables[1],
                    Price = Convert.ToDecimal(variables[2]),
                    RentPrice = Convert.ToDecimal(variables[3]),
                    Stock = Convert.ToInt32(variables[4]),
                    Picture = variables[5],
                    OS = ossenList,
                    Framework = new List<Framework>() { framework },
                    Description = variables[8]
                });

                context.SaveChanges();
            }
        }

        private void ReadLinesFromFiles(DbSet<Framework> frameworks, ApplicationDbContext context, string filename)
        {
            string[] lines = File.ReadAllLines(filename);

            foreach (string line in lines)
            {
                string[] variables = line.Split(';');

                frameworks.AddOrUpdate(framework => framework.Name, new Framework()
                {
                    //ID = Convert.ToInt32(variables[0]),
                    Name = variables[1]
                });

                context.SaveChanges();
            }

        }

        private void ReadLinesFromFiles(DbSet<OS> oss, ApplicationDbContext context, string filename)
        {
            string[] lines = File.ReadAllLines(filename);

            foreach (string line in lines)
            {
                string[] variables = line.Split(';');

                oss.AddOrUpdate(os => os.Name, new OS()
                {
                    //ID = Convert.ToInt32(variables[0]),
                    Name = variables[1]
                });

                context.SaveChanges();
            }
        }

    }
}
