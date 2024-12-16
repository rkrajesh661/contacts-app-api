using Contacts.Application.Contract;
using Contacts.Application.Middleware;
using Contacts.Application.Operations.Commands.Handlers;
using Contacts.Application.Validation;
using Contacts.Infrastructure.Data;
using FluentValidation;
using MediatR;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<IContactService, ContactService>();
builder.Services.AddMediatR(config =>
    config.RegisterServicesFromAssemblies(
        typeof(Program).Assembly, typeof(CreateContactCommandHandler).Assembly
        ));
builder.Services.AddValidatorsFromAssemblyContaining<CreateContactCommandValidator>();
builder.Services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy => {
        policy.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod();
    });
});

var app = builder.Build();

app.UseCors("AllowAll");
app.UseMiddleware<GlobalExceptionsHandlingMiddleware>();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
