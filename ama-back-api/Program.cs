using ama_back_api.Database;
using ama_back_api.Global;
using log4net;

var builder = WebApplication.CreateBuilder(args);

builder.Logging.ClearProviders();
builder.Logging.AddLog4Net();

ILog log = LogManager.GetLogger(typeof(Program));

builder.Services.AddDbContext<AmaDBContext>();
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
//builder.Services.AddSwaggerGen();
builder.Services.AddControllersWithViews(
    o=>o.ModelBinderProviders.Insert(0, new FromJsonBinderProvider())
);


var app = builder.Build();

// Disable CORS
app.UseCors(x => x
            .AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader()
        );

// Configure the HTTP request pipeline.
/*
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}*/
app.MapControllers();

log.Info("Amadeus API started");
app.Run();
