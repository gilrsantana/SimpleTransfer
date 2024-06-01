using SimpleTransfer.Domain.IdentityAggregate.Enums;

namespace SimpleTransfer.Domain.IdentityAggregate.ValueObjects.Factory;

public static class DocumentFactory
{
    public static Document? Create(string number, EDocumentType type)
    {
        return type switch
        {
            EDocumentType.CPF => Cpf.Create(number),
            EDocumentType.CNPJ => Cnpj.Create(number),
            _ => null
        };
    }
}