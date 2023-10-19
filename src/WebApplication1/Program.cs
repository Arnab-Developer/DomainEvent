using MediatR;
using WebApplication1.Application;
using WebApplication1.Application.Behaviors;
using WebApplication1.Application.Commands;
using WebApplication1.Domain.SeedWork;
using WebApplication1.Infra;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddMediatR(cfg =>
{
    cfg.RegisterServicesFromAssemblyContaining(typeof(Ping));

    cfg.AddOpenBehavior(typeof(EmptyHelloValidation<,>));
    cfg.AddOpenBehavior(typeof(AdminNameValidation<,>));
    cfg.AddOpenBehavior(typeof(Tran<,>));
});
builder.Services.AddTransient<IParentRepo, ParentRepo>();
builder.Services.AddTransient<IAnotherParentRepo, AnotherParentRepo>();
builder.Services.AddTransient<ITranManagement, TranManagement>();

var constr = builder.Configuration.GetConnectionString("constr");
builder.Services.AddSqlServer<EfContext>(constr);

var app = builder.Build();

app.MapGet("/hello", async (string name, IMediator mediator, ILogger<Program> logger) =>
{
    var helloCommand = new HelloCommand() { Name = name };
    var reply = await mediator.Send(helloCommand);
    return reply;
});

app.Run();