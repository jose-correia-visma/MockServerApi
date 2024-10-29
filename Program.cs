using System.Text.Json.Serialization;
using MockServerApi.MockServer;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
});
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    InitializeMockServer(app.Services);
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

static void InitializeMockServer(IServiceProvider services)
{
    var configuration = services.GetRequiredService<IConfiguration>();
    var mockServerUrl = configuration.GetValue<string>("MockServer:Url");
    var mockServerEnvironment = configuration.GetValue<string>("MockServer:Environment");
    MockServerHelper.Initialize(mockServerUrl, mockServerEnvironment);
}