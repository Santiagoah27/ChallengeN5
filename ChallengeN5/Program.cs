using ChallengeN5.CQRS.Commands;
using ChallengeN5.CQRS.Queries;
using ChallengeN5.Data;
using ChallengeN5.Repositories;
using Confluent.Kafka;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Nest;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddDbContext<PermissionDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));

var settings = new ConnectionSettings(new Uri("http://localhost:9200")).DefaultIndex("permissions");

builder.Services.AddSingleton<IElasticClient>(new ElasticClient(settings));

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(Program).Assembly));

var producerConfig = new ProducerConfig { BootstrapServers = "localhost:9092" };

builder.Services.AddSingleton<IProducer<Null, string>>(new ProducerBuilder<Null, string>(producerConfig).Build());

builder.Services.AddCors(options =>
{
    options.AddPolicy("Policy",
    builder =>
    {
        builder.WithOrigins("http://localhost:3000")
               .AllowAnyHeader()
               .AllowAnyMethod();
    });
});


// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.UseCors("Policy");

app.MapControllers();

app.Run();
