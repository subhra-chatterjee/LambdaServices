using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DataModel;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddAWSLambdaHosting(LambdaEventSource.HttpApi);

// Add DynamoDB configuration
builder.Services.AddSingleton<IAmazonDynamoDB, AmazonDynamoDBClient>();
builder.Services.AddSingleton<IDynamoDBContext, DynamoDBContext>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment() || true)
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// Map endpoints using the Endpoint class
SimpleApiLambda.Endpoint.MapEndpoints(app);

app.Run();
