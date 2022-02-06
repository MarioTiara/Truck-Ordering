using ReplenishmentService.Ordering.DbAcess;
using ReplenishmentService.Ordering.Entity;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReplenishmentService.Ordering.Data
{
    class Ticket
    {
        private DateTime today = DateTime.Now;
        private EntityTicketbook ticketbook = new EntityTicketbook();
        private string CustomerCode = ConfigurationManager.AppSettings.Get("CustomerCode");
        private string UnitType = "TANKER5";
        private DbLms dblms = new DbLms();
        private int ticketSize { get; set; }
        public Ticket(EntityTicketbook ticketbook){
            this.ticketbook = ticketbook;
        }
        public int bookNewTicket()
        {
            bool bookingSuccess = false;
            int ticketId = 0;
            EntityTicket newticket = new EntityTicket();
            while (!bookingSuccess)
            {
                try
                {
                    newticket = makeNewTicket();
                    if (ticketAvailable(newticket.Number))
                    {
                        ticketId = dblms.insertNewTicket(newticket);
                        bookingSuccess = true;
                    }
                }
                catch (Exception err)
                {
                    Log.log(err.Message, "Ordering.Data.Ticket.bookNewTicket");
                }
              
            }
            if (bookingSuccess)
            {
                Console.Write(ticketId);
                Console.Write(" ");
                Console.Write(newticket.Number);
                Console.Write(" ");
                Console.Write(newticket.Descryption);
                Console.Write(" ");
                Console.WriteLine();
                logTicket(newticket);
                return ticketId;
            }
            return 0;
        }
        
        private EntityTicket makeNewTicket()
        {            
            EntityTicket newticket = new EntityTicket()
            {
                Number = generateNewTicketNumber(),
                Date = string.Join("-", today.Year, today.Month.ToString("00"), today.Day.ToString("00")),
                Tipe = "Order",
                Category = "Cargo",
                Status = "NW",
                Descryption = generateDescryption(),
                NoUnit = null,
                bConfirm = 0,
                bValid = 0,
                CreateDate = today,
                CreateBy = ConfigurationManager.AppSettings.Get("CustomerAdminName"),
                UpdateDate = today,
                UpdateBy = ConfigurationManager.AppSettings.Get("CustomerAdminName"),
            };

            return newticket;
        }

        private string generateNewTicketNumber()
        {
            //DateTime today = DateTime.Now;
            string lastticket = dblms.selectLastTicket();
            string newticket = null;
            if (lastticket != null)
            {
                string UniqueKey = lastticket.Substring(lastticket.Length - 5);
                int NewUniqueKey = Int32.Parse(UniqueKey) + 1;
                newticket = string.Concat("TSIL", today.Year, today.Month.ToString("00"), today.Day.ToString("00"), NewUniqueKey.ToString("00000"));
            }
            else
            {
                int UniqueKeyFisrt = 1;
                newticket = string.Concat("TSIL", today.Year, today.Month.ToString("00"), today.Day.ToString("00"), UniqueKeyFisrt.ToString("00000"));
            }
            return newticket;
        }

        private string generateDescryption()
        {
            return string.Join("-", this.CustomerCode,ticketbook.originCode , ticketbook.destinationCode, ticketbook.totalSlot.ToString("00"), UnitType, "1");
        }
        
        private bool ticketAvailable(string ticketnumber)
        {
            DataTable ticketSelected = dblms.selectbySingleTicketNumber(ticketnumber);
            if (ticketSelected.Rows.Count > 0)
                return false;
            return true;

        }
        private void logTicket(EntityTicket newticket)
        {
            EntityLmsTicketLog ticketlog = new EntityLmsTicketLog();
            ticketlog.Number = newticket.Number;
            ticketlog.Status = newticket.Status;
            ticketlog.bConfirm = newticket.bConfirm;
            ticketlog.CreateBy = newticket.CreateBy;
            ticketlog.CreateDate = newticket.CreateDate;
            dblms.insertLog(ticketlog);
        }
    }
}
