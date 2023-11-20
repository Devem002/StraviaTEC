using ApiMongo.Configuration;
using ApiMongo.DAL;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.


// This method gets called by the runtime. Use this method to add services to the container.

builder.Services.Configure<CommentsDatabaseSettings>(builder.Configuration.GetSection("CommentsDatabase"));
builder.Services.AddSingleton<ICommentsDatabaseSettings>(sp =>
      sp.GetRequiredService<IOptions<CommentsDatabaseSettings>>().Value);
builder.Services.AddSingleton<CommentsDAL>();

builder.Services.AddControllers();
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

app.MapControllers();

app.Run();
