using ControleDeSalao.Domain.Entities;
using System.Linq;

namespace ControleDeSalao.Infra.Cross.Security
{
    public class Acesso
    {
        public static Funcionalidade RetornaAcesso(string direitos)
        {
            var func = new Funcionalidade();
            var tipo = func.GetType();

            var dir = direitos.Split('|');

            for (int i = 0; i < dir.Count(); i++)
            {
                foreach (var info in tipo.GetProperties())
                {
                    if (info.Name == dir[i])
                    {
                        info.SetValue(func, true, null);
                    }
                }
            }

            return func;
        }

        public static string PreencherDireitos(Funcionalidade funcionalidade)
        {
            var direitos = string.Empty;
            const string separador = "|";
            var tipo = funcionalidade.GetType();

            foreach (var info in tipo.GetProperties())
            {
                var Name = info.Name;
                var check = info.GetValue(funcionalidade, null);
                if ((bool)check)
                    direitos += Name + separador;
            }

            return direitos;
        }
    }
}
