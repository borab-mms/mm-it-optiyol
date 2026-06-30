using MM.IT.Common.Constants;
using MM.IT.Core.Api.Extensions;
using MM.IT.Core.Extensions;

ThreadPool.SetMinThreads(100, 100);

var builder = WebApplication.CreateBuilder(args);

builder.Services.UseDefaultApiServiceCollection(builder.Configuration, builder.Environment);

var app = builder.Build();

app.UseDefaultApiApplicationBuilder(builder.Configuration, builder.Environment);

app.Run();
