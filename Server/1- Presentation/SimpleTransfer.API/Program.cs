using SimpleTransfer.API.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.ConfigureApi(builder.Configuration);
builder.Services.ConfigureData(builder.Configuration);
builder.Services.ConfigurePersistence();
builder.Services.LoadApi(builder.Build());