using MongoDBPOCCsharp.Models;
using MongoDBPOCCsharp.Service;

var builder = WebApplication.CreateBuilder(args);

builder.Services.Configure<ProjectManagementStoreSetting>(
    builder.Configuration.GetSection("ProjectManagementStore"));



builder.Services.AddSingleton<EmployeeService>();

builder.Services.AddSingleton<DepartmentService>();
builder.Services.AddSingleton<ProjectService>();

builder.Services.AddGraphQLServer()
                .AddQueryType<Query>();
var app = builder.Build();
app.UseRouting().UseEndpoints(endpoints => endpoints.MapGraphQL());

app.MapGet("/", () => "Hello World!");

app.Run();
