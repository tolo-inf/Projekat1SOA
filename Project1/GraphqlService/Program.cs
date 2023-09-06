using GraphqlService.Repository;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<SensorDbContext>(options =>
{
    options.UseMySQL("server=127.0.0.1;user=root;password=root;database=iot1db;port=3306");
});

builder.Services.AddGraphQLServer().AddQueryType<Query>().AddProjections().AddFiltering().AddSorting();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.MapGraphQL("/graphql");

app.Run();
