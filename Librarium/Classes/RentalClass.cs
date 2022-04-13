using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Librarium.DataBase;

namespace Librarium.Classes
{
    class RentalClass
    {

        public static double Credit(int bookId, DateTime startdate, DateTime enddate)
        {

            List<Book> list = new List<Book>();
            list = AppData.Context.Book.ToList();


            double sum = 0;
            var cost = list.Where(i => i.ID == bookId).FirstOrDefault();


            if ((DateTime.Now.Date - startdate.Date).TotalDays > 30)
            {
                sum = (Convert.ToDouble(cost.Cost)*0.1) * ((enddate.Date - startdate.Date).TotalDays - 30);
            }


            return (double)sum;

        }


    }
}
