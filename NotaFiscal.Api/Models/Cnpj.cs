using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotaFiscal.Api.Models
{
    public class Cnpj
    {
        public static string RemoveDigitos(string cnpj)
        {
            StringBuilder newCnpj = new();
            foreach (char c in cnpj)
            {
                if (char.IsDigit(c))
                    newCnpj.Append(c);
            }
            return newCnpj.ToString();
        }

        public static string FormataCnpj(string cnpj)
        {
            StringBuilder newCnpj = new();
            for (int i = 0; i < 14; i++)
            {
                if (i == 2 || i == 5)
                    newCnpj.Append('.');
                if (i == 8)
                    newCnpj.Append('/');
                if (i == 12)
                    newCnpj.Append('-');

                newCnpj.Append(cnpj[i]);
            }
            return newCnpj.ToString();
        }
        public static bool VerificaCnpj(string cnpj)
        {
            
            int[] digitos = new int[cnpj.Length];

            for (int i = 0; i < cnpj.Length; i++)
            {
                digitos[i] = int.Parse(cnpj[i].ToString());
            }

            // soma do primeiro digito verificador (X) xx.xxx.xxx/xxxx-Xx
            int soma = 0;
            int peso = 5;
            for (int i = 0; i < cnpj.Length - 2; i++)
            {
                soma += digitos[i] * peso;
                peso--;
                if (peso < 2)
                    peso = 9;
            }
            int resto = soma % 11;
            int dv1 = resto < 2 ? 0 : 11 - resto;

            // soma do primeiro digito verificador (X) xx.xxx.xxx/xxxx-xX
            soma = 0;
            peso = 6;
            for (int i = 0; i < cnpj.Length - 2; i++)
            {
                soma += digitos[i] * peso;
                peso--;
                if (peso < 2)
                    peso = 9;
            }

            resto = soma % 11;
            int dv2 = resto > 2 ? 0 : 11 - resto;

            return digitos[12] == dv1 && digitos[13] == dv2;
        }
    }

}