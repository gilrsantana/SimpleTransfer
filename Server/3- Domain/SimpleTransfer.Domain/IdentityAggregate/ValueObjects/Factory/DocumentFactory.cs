using System.Text.RegularExpressions;

namespace SimpleTransfer.Domain.IdentityAggregate.ValueObjects.Factory;

public static class DocumentFactory
{
    public static Document? Create(string number)
    {
        var digits = Regex.Replace(number, @"\D", "");
        return digits.Length switch
        {
            11 => Cpf.Create(number),
            14 => Cnpj.Create(number),
            _ => null
        };
    }
}