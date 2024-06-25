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
        string cnpj = "00.000.000/0000-00";

        cnpj = Cnpj.RemoveDigitos(cnpj);

        string cnpjSemDigitos = "00000000000000";
        int size = cnpjSemDigitos.Length;

        Assert.Equal(cnpjSemDigitos, cnpj);
    }

    [Fact]
    public void VerificaCnpj_RecebeCnpjValido_RetornaTrue()
    {
        string cnpj = "44.159.188/0001-90";
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
}