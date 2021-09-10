using App.MVC.ViewModels;
using System;
using System.Net.Http;
using System.Net.Http.Headers;

namespace App.MVC.Util
{
    public class UtilAgenda
    {
        public static string ResgatarDiaSemana(DayOfWeek dayOfWeek)
        {
            switch (dayOfWeek)
            {
                default:
                    return "Domingo";
                case DayOfWeek.Monday:
                    return "Segunda";
                case DayOfWeek.Tuesday:
                    return "Terça";
                case DayOfWeek.Wednesday:
                    return "Quarta";
                case DayOfWeek.Thursday:
                    return "Quinta";
                case DayOfWeek.Friday:
                    return "Sexta";
                case DayOfWeek.Saturday:
                    return "Sábado";
            }
        }

        public static int CalcularHoraFinal(int pHoraInicial, int pHoraDuracao, int pMinutoDuracao)
        {
            var horaInical = Convert.ToInt32(pHoraInicial.ToString("0000").Substring(0, 2));
            var minInicial = Convert.ToInt32(pHoraInicial.ToString("0000").Substring(2, 2));


            TimeSpan horaIni = new TimeSpan(horaInical, minInicial, 0);
            TimeSpan horaFim = new TimeSpan(pHoraDuracao, pMinutoDuracao, 0);

            return Convert.ToInt32(horaFim.Add(horaIni).ToString().Replace(":", "").Substring(0, 4));
        }

        public static bool EnviarSMS(string msg, string celularCliente, string data, string time)
        {
            /*
             &jobdate=24/12/2013&jobtime=16:00
             */
            var jobdate = string.IsNullOrEmpty(data) ? string.Empty : string.Format("&jobdate={0}", data);
            var jobtime = string.IsNullOrEmpty(time) ? string.Empty : string.Format("&jobtime={0}", time);
            var celular = celularCliente.Replace("(", "").Replace(")", "").Replace("-", "").Trim();
            var url = string.Format("http://54.173.24.177/painel/api.ashx?action=sendsms&lgn=11953292131&pwd=784106&msg={0}&numbers={1}{2}{3}", msg, celular, jobdate, jobtime);
            HttpContent content = new StringContent("");
            content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            var client = new HttpClient { BaseAddress = new Uri("http://localhost:1066/api/") };
            var response = client.PostAsync(url, content).Result;

            if (response.IsSuccessStatusCode)
            {
                return true;
            }

            return false;
        }
    }
}
