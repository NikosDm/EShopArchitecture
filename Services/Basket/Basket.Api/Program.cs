using Basket.Api.Data;
using Basket.Api.Models;
using BuildingBlocks.Behaviours;
using BuildingBlocks.Exceptions.handler;
using Carter;
using Marten;

var builder = WebApplication.CreateBuilder(args);

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddCarter();
builder.Services.AddMediatR(config => 
{
    config.RegisterServicesFromAssembly(typeof(Program).Assembly);
    
    // Here we basically add our custom behaviors as pipeline behaviors into mediator.
    config.AddOpenBehavior(typeof(ValidationBehaviour<,>));
    config.AddOpenBehavior(typeof(LoggingBehavior<,>));
});

builder.Services.AddMarten(opts => 
{
    opts.Connection(builder.Configuration.GetConnectionString("PostgresDB"));
    
    // We can use function expression to identify and set our identity field should be username.
    opts.Schema.For<ShoppingCart>().Identity(x => x.Username);
}).UseLightweightSessions();

builder.Services.AddScoped<IBasketRepository, BasketRepository>();

builder.Services.AddExceptionHandler<CustomExceptionHandler>();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.MapCarter();

app.UseExceptionHandler(options => {});

app.Run();
