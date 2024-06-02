using System.Text.RegularExpressions;
using MediatR;
using Microsoft.AspNetCore.Identity;
using SimpleTransfer.Application.Identity.Token.Interface;
using SimpleTransfer.Application.Identity.UseCases.RegisterUser.Commands;
using SimpleTransfer.Application.Identity.UseCases.RegisterUser.Notifications;
using SimpleTransfer.Application.Notifications;
using SimpleTransfer.Domain.IdentityAggregate.Entities;
using SimpleTransfer.Domain.IdentityAggregate.ValueObjects.Factory;

namespace SimpleTransfer.Application.Identity.UseCases.RegisterUser.Handlers;

public class RegisterUserCommandHandler(
    UserManager<User> userManager, 
    IMediator mediator, 
    ITokenService tokenService) 
    : IRequestHandler<RegisterUserCommand, object>
{
    public async Task<object> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
    {
        var newUserError = ValidateNewUser(request);
        if (!string.IsNullOrEmpty(newUserError))
            return new ErrorNotification([newUserError]);
        
        var user = User.Create(request.Username, 
            DocumentFactory.Create(request.Document)!, 
            request.Email, 
            request.Password);
        
        if (!user.IsValid)
            return new ErrorNotification(user.Notifications
                .Select(x => x.Message).AsEnumerable());
        
        var result = await userManager.CreateAsync(user, request.Password);
        if (!result.Succeeded)
            return new ErrorNotification(result.Errors
                .Select(x => x.Description).AsEnumerable());
        var token = tokenService.GenerateToken(user);
        var notification = new RegisteredUserNotification
            (user.Id, user.Document.Number, user.Email ?? "", user.UserName ?? "", token);
        await mediator.Publish(notification, cancellationToken);
        
        return notification;
    }
    
    private string ValidateNewUser(RegisterUserCommand request)
    {
        if (GetUserByUserName(request.Username.Trim().ToUpper()) is not null)
            return "Nome de usuário já existe";
        
        if (GetUserByEmail(request.Email.Trim().ToLower()) is not null)
            return "Email já cadastrado";
        
        if (GetUserByDocument(request.Document) is not null)
            return "Documento já cadastrado";
        
        var document = DocumentFactory.Create(request.Document);
        return document is null ? "Documento Inválido" : "";
    }
    
    
    private User? GetUserByUserName(string userName) => userManager.Users
        .FirstOrDefault(x => x.UserName == userName);
    
    private User? GetUserByEmail(string email) => userManager.Users
        .FirstOrDefault(x => x.Email == email);
    
    private User? GetUserByDocument(string documentNumber)
    {
        var number = Regex.Replace(documentNumber, "[^0-9]", "");
        return userManager.Users
            .FirstOrDefault(x => x.Document.Number == number);
    }
}