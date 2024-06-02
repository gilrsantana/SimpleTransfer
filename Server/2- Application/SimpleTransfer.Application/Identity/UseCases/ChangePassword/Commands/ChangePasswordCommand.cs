using MediatR;

namespace SimpleTransfer.Application.Identity.UseCases.ChangePassword.Commands;

public record ChangePasswordCommand(string Id, string OldPassword, string NewPassword) 
    : IRequest<object>;