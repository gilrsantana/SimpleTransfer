using Flunt.Notifications;
using Flunt.Validations;
using Microsoft.AspNetCore.Identity;
using SimpleTransfer.Domain.BankTransactionsAggregate.Entities;
using SimpleTransfer.Domain.IdentityAggregate.ValueObjects.Factory;

namespace SimpleTransfer.Domain.IdentityAggregate.Entities;

public sealed class User : IdentityUser
{
    public Document Document { get; private set; }
    public string AccountBankId { get; private set; }
    public AccountBank AccountBank { get; private set; }
    private IEnumerable<Notification> _notifications = new List<Notification>();
    public IEnumerable<Notification> Notifications
    {
        get => _notifications;
        private set => _notifications = value.AsEnumerable();
    }
    public bool IsValid => _notifications.Any();

    private User()
    {
    }
    
    private User(string name, Document document, string email)
    {
        UserName = name.Trim().ToUpper();
        Email = email.Trim().ToLower();
        Document = document;
        Validate();
    }
    
    public static User Create(string name, Document document, string email, string password)
    {
        var user = new User(name, document, email);
        if (!user.IsValid)
            return user;

        var passwordHasher = new PasswordHasher<User>();
        user.PasswordHash = passwordHasher.HashPassword(user, password);

        return user;
    }
    
    public void Update(string name, Document document, string email)
    {
        var oldName = UserName;
        var oldDocument = Document;
        var oldEmail = Email;
        UserName = name.Trim().ToUpper();
        Document = document;
        Email = email.Trim().ToLower();
        Validate();
        if (IsValid) return;
        UserName = oldName;
        Document = oldDocument;
        Email = oldEmail;
    }
    
    public void ChangePassword(string actualPassword, string newPassword)
    {
        if (!IsValidPassword(actualPassword))
        {
            AddNotifications(
                new List<Notification> { new ("Password", "Senha atual incorreta") });
            return;
        }
        
        SetPassword(newPassword);
    }
    
    private bool IsValidPassword(string password)
    {
        var passwordHasher = new PasswordHasher<User>();
        if (PasswordHash == null)
            return false;
        return passwordHasher
            .VerifyHashedPassword(this, PasswordHash, password) 
               == PasswordVerificationResult.Success;
    }

    private void SetPassword(string newPassword)
    {
        var passwordHasher = new PasswordHasher<User>();
        PasswordHash = passwordHasher.HashPassword(this, newPassword);
    }
    
    private void Validate()
    {
        var contract = new Contract<User>()
            .Requires()
            .IsEmail(Email, "Email", "E-mail inválido")
            .IsNotNullOrEmpty(UserName, "Name", "Nome inválido")    
            .IsTrue(Document.IsValid, "Document", "Invalid document");
        AddNotifications(contract.Notifications);
    }

    private void AddNotifications(IEnumerable<Notification> notifications)
    {
        Notifications = _notifications.Concat(notifications).ToList();
    }
}