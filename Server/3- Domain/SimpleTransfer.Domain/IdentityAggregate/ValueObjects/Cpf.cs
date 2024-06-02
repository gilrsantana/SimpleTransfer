using System.Text.RegularExpressions;
using Flunt.Br;
using Flunt.Br.Extensions;
using SimpleTransfer.Domain.IdentityAggregate.Enums;
using SimpleTransfer.Domain.IdentityAggregate.ValueObjects.Factory;

namespace SimpleTransfer.Domain.IdentityAggregate.ValueObjects;

public class Cpf : Document
{
    private Cpf(string number) : base(number, EDocumentType.CPF)
    {
        Validate();
    }
    
    public static Cpf Create(string number)
    {
        var numberDigits = Regex.Replace(number, @"\D", "");
        return new Cpf(numberDigits);
    }

    public string GetToString()
        => Convert.ToUInt64(Number)
            .ToString(@"000\.000\.000\-00");

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
            .IsCpf(Number, nameof(Number), "CPF inválido"));
    }
}