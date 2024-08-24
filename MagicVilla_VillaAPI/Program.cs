using MagicVilla_VillaAPI.Data;
using MagicVilla_VillaAPI.Logging;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

// if there is no return type then option is made

builder.Services.AddControllers(
       // options => options.ReturnHttpNotAcceptable = true  // get the Accept header key value
    ).AddNewtonsoftJson()
    .AddXmlDataContractSerializerFormatters(); // supports the xml format
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// application db context 

builder.Services.AddDbContext<AppDbContext>(
    options => options.UseSqlServer(
        builder.Configuration.GetConnectionString("sqlserver")
        ));

// adding the Interface implementation
//builder.Services.AddSingleton<> // use the instance single time through the application life cycle
// builder.Services.AddScoped<> // create the instance for each request in the application
//builder.Services.AddTransient<> // crate a instance for each request in the controller


builder.Services.AddSingleton<ILogging, Logging>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
