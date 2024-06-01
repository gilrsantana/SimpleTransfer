using SimpleTransfer.API.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.ConfigureApi();
builder.Services.ConfigureData(builder.Configuration);
builder.Services.LoadApi(builder.Build());