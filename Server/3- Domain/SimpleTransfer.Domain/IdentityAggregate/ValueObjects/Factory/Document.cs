using SimpleTransfer.Common.ValueObjects;
using SimpleTransfer.Domain.IdentityAggregate.Enums;

namespace SimpleTransfer.Domain.IdentityAggregate.ValueObjects.Factory;

public class Document : BaseValueObject
{
    public string Number { get; protected set; }
    public EDocumentType Type { get; private set; }

    protected Document(string number, EDocumentType type)
    {
        Number = number;
        Type = type;
    }
    
    // public abstract string GetToString();
    //
    // public abstract void Update(string number);
    public override void Validate()
    {
        throw new NotImplementedException();
    }
}