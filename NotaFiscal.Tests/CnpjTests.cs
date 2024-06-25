namespace NotaFiscal.Tests;
using NotaFiscal.Api.Models;

public class CnpjTests
{
    [Fact]
    public void FormataCnpj_RecebeSemDigitos_RetornaComDigitos()
    {
        string cnpj = "00000000000000";

        cnpj = Cnpj.FormataCnpj(cnpj);

        string cnpjFormatado = "00.000.000/0000-00";

        Assert.Equal(cnpjFormatado, cnpj);
    }

    [Fact]
    public void RemoveDigitosCnpj_RecebeComDigitos_RetornaSemDigitos()
    {
        string cnpj = "33.014.556/1598-96";

        cnpj = Cnpj.RemoveDigitos(cnpj);

        string cnpjSemDigitos = "33014556159896";
        int size = cnpjSemDigitos.Length;

        Assert.Equal(cnpjSemDigitos, cnpj);
    }

    [Fact]
    public void VerificaCnpj_RecebeCnpjValido_RetornaTrue()
    {
        string cnpj = "33.014.556/1598-96";
        cnpj = Cnpj.RemoveDigitos(cnpj);

        bool resultado = Cnpj.VerificaCnpj(cnpj);

        Assert.True(resultado);
    }

    [Fact]
    public void VerificaCnpj_RecebeCnpjInvalido_RetornaFalso()
    {
        string cnpj = "12.345.678/9012-34";
        cnpj = Cnpj.RemoveDigitos(cnpj);

        bool resultado = Cnpj.VerificaCnpj(cnpj);

        Assert.False(resultado);
    }

    [Fact]
    public void VerificaEFormata_RecebeCnpjValido_RetornaTrueECnpjFormatado()
    {
        string cnpj = "33014556159896";
        string cnpjEsperado = "33.014.556/1598-96"; // Ajuste de acordo com o formato esperado
        bool resultado = Cnpj.VerificaEFormata(cnpj, out string cnpjFormatado);

        Assert.Equal(cnpjEsperado, cnpjFormatado);
        Assert.True(resultado);
    }
    
}