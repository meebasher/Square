using Square.Api.Middleware;
using Square.Api.Options;
using Square.Infra.Data.Context;
using Square.IoC;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContextGeneric<SquareDbContext>(nameof(ConnectionStrings.SquareContext));
builder.Services.AddControllers();
builder.Services.Register();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

//Auto migrations, so no DB handjob
await app.Services.MigrateDbContextGenericAsync<SquareDbContext>();
// Configure the HTTP request pipeline.

// Exception handling middleware
app.UseMiddleware<ExceptionHandlingMiddleware>();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
