using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace SoolfOrb_OrderCheckout
{
    public class Program
    {
        public static void Main(string[] args)
        {
            string[] data = new string[]{
              
                "dell-streak-7.json",
                "dell-venue.json",
                "droid-2-global-by-motorola.json",
                "droid-pro-by-motorola.json",
                "lg-axis.json",
                "motorola-atrix-4g.json",
                "motorola-bravo-with-motoblur.json",
                "motorola-charm-with-motoblur.json",
                "motorola-defy-with-motoblur.json",
                "motorola-xoom-with-wi-fi.json",
                "motorola-xoom.json",
                "nexus-s.json",
                "phones.json",
                "samsung-galaxy-tab.json",
                "samsung-gem.json",
                "samsung-mesmerize-a-galaxy-s-phone.json",
                "samsung-showcase-a-galaxy-s-phone.json",
                "samsung-transform.json",
                "sanyo-zio.json",
                "t-mobile-g2.json",
                "t-mobile-mytouch-4g.json"
            };
            using (var db = new ProductionDbContext())
            {
                if (db.Providers.Count() == 0)
                {                
                    db.Providers.Add(new Provider() { Name = "ÌÒÑ" });
                    db.Providers.Add(new Provider() { Name = "ÒÅËÅ2" });
                    db.Providers.Add(new Provider() { Name = "ÁÈËÀÉÍ" });
                    db.Providers.Add(new Provider() { Name=  "Ìåãàôîí" });
                    db.SaveChanges();
                }
                var random = new Random();
                Provider[] arr=  db.Providers.ToArray();
                foreach (string file in data)
                {
                    int pid = arr[(int)Math.Floor(random.NextDouble() * arr.Length)].ID;
                    string Name = file.Substring(0, file.LastIndexOf(".")).Replace("-", " ").Replace("-", " ").Replace("-", " ").Replace("-", " ").Replace("-", " ");
                    string json = System.IO.File.ReadAllText(System.IO.Directory.GetCurrentDirectory() + @"\wwwroot\phones\"+file);
                    db.OrderItems.Add(new OrderItem() { 
                        Name = Name,
                        ImageUri = $"/img/{file.Substring(0, file.LastIndexOf("."))}.1.jpg",
                        Quantity = ((float)random.NextDouble()*1000 + 500),
                        PropertiesJson = json,
                        Unit = "$",
                        ProviderID = pid
                    });
                }
                db.SaveChanges();
            }


            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
