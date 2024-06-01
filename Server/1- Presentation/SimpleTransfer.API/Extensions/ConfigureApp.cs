namespace SimpleTransfer.API.Extensions;

public static class ConfigureApp
{
    public static void LoadApi(this IServiceCollection services, WebApplication app)
    {
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseCors(x => x
            .AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin());
        app.UseHttpsRedirection();
        app.UseAuthentication();
        app.UseAuthorization();
        app.MapControllers();
        app.Run();
    }
}