using ReplenishmentService.APITransaction;
using ReplenishmentService.APITransaction.Model;
using ReplenishmentService.APITransaction.Controller;
using ReplenishmentService.Ordering;
using ReplenishmentService.Ordering.DbContext;
using System;
using System.Threading.Tasks;
using Topshelf;

namespace ReplenishmentService
{
    class Program
    {
        static void Main(string[] args)
        {
            //DeliverySender sender = new DeliverySender();
            //sender.sendDelivery();

            //DeliveriesSender delivery = new DeliveriesSender();
            //delivery.sendDelivery();

          
            //Console.ReadKey();


            var exitCode = HostFactory.Run(hostConfig =>
            {
                hostConfig.Service<MainService>(serviceConfig =>
                {
                    serviceConfig.ConstructUsing(() => new MainService());
                    serviceConfig.WhenStarted(s => s.Start());
                    serviceConfig.WhenStopped(s => s.Stop());
                });
                hostConfig.RunAsLocalSystem();
                hostConfig.SetServiceName("ReplenishmentResvice");
                hostConfig.SetDisplayName("IPE ReplenishmentResvice");
                hostConfig.SetDescription("Seino-IPE Fuel Replenishment Service");
            });
        }
    }
}
