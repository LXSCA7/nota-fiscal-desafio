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
            return Convert.ToUInt64(cnpj).ToString(@"00\.000\.000\/0000\-00");
        }
        public static bool VerificaCnpj(string cnpj)
        {
            if (cnpj.Length != 14)
                return false;

            int[] digitos = new int[14];
            for (int i = 0; i < cnpj.Length; i++)
            {
                digitos[i] = int.Parse(cnpj[i].ToString());
            }

            // soma do primeiro digito verificador (X) xx.xxx.xxx/xxxx-Xx
            int soma = 0;
            int peso = 5;
            for (int i = 0; i < 12; i++)
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
            for (int i = 0; i < 13; i++)
            {
                soma += digitos[i] * peso;
                peso--;
                if (peso < 2)
                    peso = 9;
            }

            resto = soma % 11;
            int dv2 = resto < 2 ? 0 : 11 - resto;

            return digitos[12] == dv1 && digitos[13] == dv2;
        }

        public static bool VerificaEFormata(string cnpj, out string cnpjFormatado)
        {
            cnpjFormatado = "";
            cnpj = RemoveDigitos(cnpj);

            if (cnpj.Length != 14 || !VerificaCnpj(cnpj))
                return false;

            cnpjFormatado = FormataCnpj(cnpj);
            return true;
        }
    }
}