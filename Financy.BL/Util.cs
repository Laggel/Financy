using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Financy.Data;
using Financy.DataAccess;

namespace Financy.BL
{
    public static class Util
    {
        public static string GetDescripcion(Tipo tipo, int day)
        { 
            var str = "";
            switch(tipo.Descripcion)       
            {         
            case "Mensual":
                    str = "Cada " +day+ " del mes.";
                break;                  
            case "Diario":            
                str = "Cada día.";
                break;           
            case "Semanal":
                str = "Cada " + System.Globalization.CultureInfo.CurrentCulture.DateTimeFormat.DayNames[day] + ".";
                break;
            case "De Lunes a Viernes":            
                str = "Cada día laborable.";
                break;          
            }

             return str;
        }

        public static int GetDaysInMonth(DateTime date)
        {
            return DateTime.DaysInMonth(date.Year, date.Month);
        }

        public static int GetWeekDaysInMonth(DateTime date)
        {
            var counter = 0;
            for (int i = 0; i < DateTime.DaysInMonth(date.Year, date.Month); i++)
            {
                var fecha = new DateTime(date.Year, date.Month, i);

                if (fecha.DayOfWeek != DayOfWeek.Saturday && fecha.DayOfWeek != DayOfWeek.Sunday)
                {
                    counter++;
                }
            }

            return counter;
        }

        public static int GetWeeks(DateTime date)
        {
            var counter = 0;
            var askDate = date;
            date = new DateTime(date.Year, date.Month, 1);
            
            while (askDate.Month == date.Month)
            {
                counter++;
                date = Proyectar.GetNextWeekday(date, (int)DayOfWeek.Monday);
            }
        
            return counter;
        }

    }
}
