using Flunt.Br;
using Flunt.Br.Extensions;
using SimpleTransfer.Domain.IdentityAggregate.Enums;
using SimpleTransfer.Domain.IdentityAggregate.ValueObjects.Factory;

namespace SimpleTransfer.Domain.IdentityAggregate.ValueObjects;

public class Cnpj : Document
{
    private Cnpj(string number) : base(number, EDocumentType.CNPJ)
    {
        Validate();
    }
    
    internal static Cnpj Create(string number)
    {
        return new Cnpj(number);
    }

    public string GetToString()
        => Convert.ToUInt64(Number)
            .ToString(@"00\.000\.000\/0000\-00");

    public void Update(string number)
    {
        var originalNumber = Number;
        Number = number;
        Validate();
        if (IsValid)
            return;
        Number = originalNumber;
    }
    
    

    public sealed override void Validate()
    {
        AddNotifications(new Contract()
            .IsCnpj(Number, nameof(Number), "CNPJ inválido"));
    }
}