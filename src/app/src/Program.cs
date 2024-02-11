var builder = WebApplication.CreateBuilder(args);

builder.Services.AddMySql(builder.Configuration)
                .AddRepositories()
                .AddServices()
                .AddEndpointsApiExplorer()
                .AddSwagger()
                .AddCors()
                .AddEndpointsApiExplorer();

builder.Services.AddControllers();
var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();
app.UseCors(x => x.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
app.MapControllers();
app.UseHttpsRedirection();
app.Run();

public partial class Program
{
    protected Program()
    {
    }
}
