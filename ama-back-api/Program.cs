using ama_back_api.Database;
using ama_back_api.DBModels;
using ama_back_api.Global;
using log4net;

var builder = WebApplication.CreateBuilder(args);

builder.Logging.ClearProviders();
builder.Logging.AddLog4Net();

ILog log = LogManager.GetLogger(typeof(Program));

builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
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

using (var scope = app.Services.CreateScope())
{
    var _dbContext = scope.ServiceProvider.GetRequiredService<AmaDBContext>();
    List<AmaCategory> categories = [.._dbContext.Categories.ToList()];

    log.Info("Checking if status exist");
    if (!_dbContext.Status.Any(c=>c.Name!.Equals("Created"))){
        _dbContext.Status.Add(new AmaStatus { Name = "Created", Description = "Entity has been created"});
        log.Info("Created status added");
    }

    if (!_dbContext.Status.Any(c=>c.Name!.Equals("OnGoing"))){
        _dbContext.Status.Add(new AmaStatus { Name = "OnGoing", Description = "Entity is opened"});
        log.Info("OnGoing status added");
    }

    if (!_dbContext.Status.Any(c=>c.Name!.Equals("Closed"))){
        _dbContext.Status.Add(new AmaStatus { Name = "Closed", Description = "Entity is closed"});
        log.Info("Closed status added");
    }

    if (!_dbContext.Status.Any(c=>c.Name!.Equals("Archived"))){
        _dbContext.Status.Add(new AmaStatus { Name = "Archived", Description = "Entity is archived"});
        log.Info("Archived status added");
    }
    _dbContext.SaveChanges();
    log.Info("Statuses up to date");
}


log.Info("Amadeus API started");
app.Run();
