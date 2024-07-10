using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.EntityFrameworkCore;
using Reservation.Models;
using Reservation.Services;
using Reservation.Web.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddDefaultIdentity<AppUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddEntityFrameworkStores<ApplicationDbContext>();


builder.Services.AddAuthentication()
   .AddMicrosoftAccount(microsoftOptions =>
   {
       microsoftOptions.ClientId = builder.Configuration["GraphSettings:ClientId"];
       microsoftOptions.ClientSecret = builder.Configuration["GraphSettings:ClientSecret"];
   });

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("AdminPolicy", policy => policy.RequireRole("Admin"));
});

builder.Services.AddRazorPages(options =>
{
    //options.Conventions.AuthorizeFolder("/Admin", "AdminPolicy");
});

builder.Services.AddServerSideBlazor();

#region E-Mail
//builder.Services.Configure<MailSettings>(builder.Configuration.GetSection("MailSettings"));
builder.Services.Configure<GraphSettings>(builder.Configuration.GetSection("GraphSettings"));
builder.Services.AddTransient<IEmailSender, GraphEmailSender>();
#endregion E-Mail


builder.Services.Configure<GraphSettings>(
    builder.Configuration.GetSection(GraphSettings.SettingsName));

builder.Services.AddSingleton<GraphService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();
app.MapBlazorHub();

app.Run();
