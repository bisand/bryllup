using Microsoft.Extensions.FileProviders;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

var config =
    new ConfigurationBuilder()
        .AddEnvironmentVariables()
        .Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(//options =>
    // {
    //     options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
    //     options.RoutePrefix = string.Empty;
    // }
    );
    var root = Directory.GetCurrentDirectory();
    var dotenv = Path.Combine(root, ".env");
    DotEnv.Load(dotenv);
}

app.UseFileServer(new FileServerOptions
{
    FileProvider = new PhysicalFileProvider(
         Path.Combine(Directory.GetCurrentDirectory(), "web")),
    RequestPath = "",
    EnableDefaultFiles = true
});

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
