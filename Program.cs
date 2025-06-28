using BlazorServerTablerForNetExample.Data;
using Microsoft.AspNetCore.ResponseCompression;
using TablerForNet;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddSingleton<WeatherForecastService>();

builder.Services.AddTablerForNet();

// Configuration binding
builder.Services.Configure<ResponseCompressionOptions>(
    builder.Configuration.GetSection("ResponseCompression"));
//Enable is a security risk but website is much faster
//Risk is when using cookies
//https://en.wikipedia.org/wiki/CRIME
builder.Services.AddResponseCompression();


var app = builder.Build();

//must call at first
app.UseResponseCompression();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();
app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();
