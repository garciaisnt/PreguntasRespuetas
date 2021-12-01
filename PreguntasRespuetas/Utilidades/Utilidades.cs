using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PreguntasRespuetas.Utilidades
{
    public static class Utilidades
    {
        public static string TimeAgo(this DateTime dateTime)
        {
            string result = string.Empty;
            var timeSpan = DateTime.Now.Subtract(dateTime);

            if (timeSpan <= TimeSpan.FromSeconds(60))
            {
                result = string.Format("hace {0} segundos", timeSpan.Seconds);
            }
            else if (timeSpan <= TimeSpan.FromMinutes(60))
            {
                result = timeSpan.Minutes > 1 ?
                    String.Format("hace {0} minutos", timeSpan.Minutes) :
                    "alrededor de un minuto";
            }
            else if (timeSpan <= TimeSpan.FromHours(24))
            {
                result = timeSpan.Hours > 1 ?
                    String.Format("hace {0} horas", timeSpan.Hours) :
                    "alrededor de una hora";
            }
            else if (timeSpan <= TimeSpan.FromDays(30))
            {
                result = timeSpan.Days > 1 ?
                    String.Format("hace {0} dias", timeSpan.Days) :
                    "ayer";
            }
            else if (timeSpan <= TimeSpan.FromDays(365))
            {
                result = timeSpan.Days > 30 ?
                    String.Format("hace {0} meses", timeSpan.Days / 30) :
                    "alrededor de un mes";
            }
            else
            {
                result = timeSpan.Days > 365 ?
                    String.Format("hace {0} años", timeSpan.Days / 365) :
                    "alrededor de un año";
            }

            return result;
        }

    }
}