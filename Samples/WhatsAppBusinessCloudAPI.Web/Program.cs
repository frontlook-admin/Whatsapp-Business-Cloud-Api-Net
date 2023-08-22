using Newtonsoft.Json;
using WhatsappBusiness.CloudApi.Configurations;
using WhatsappBusiness.CloudApi.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews().AddNewtonsoftJson(options =>
{
    options.SerializerSettings.Formatting = Formatting.Indented;
    options.SerializerSettings.NullValueHandling = NullValueHandling.Ignore;
});

builder.Services.Configure<WhatsAppBusinessCloudApiConfig>(options =>
{
    builder.Configuration.GetSection("WhatsAppBusinessCloudApiConfiguration").Bind(options);
});

#pragma warning disable CS8601 // Possible null reference assignment.
WhatsAppBusinessCloudApiConfig whatsAppConfig = new WhatsAppBusinessCloudApiConfig
{
    WhatsAppBusinessPhoneNumberId = builder.Configuration.GetSection("WhatsAppBusinessCloudApiConfiguration")["WhatsAppBusinessPhoneNumberId"],
    WhatsAppBusinessAccountId = builder.Configuration.GetSection("WhatsAppBusinessCloudApiConfiguration")["WhatsAppBusinessAccountId"],
    WhatsAppBusinessId = builder.Configuration.GetSection("WhatsAppBusinessCloudApiConfiguration")["WhatsAppBusinessId"],
    AccessToken = builder.Configuration.GetSection("WhatsAppBusinessCloudApiConfiguration")["AccessToken"],
    AppName = builder.Configuration.GetSection("WhatsAppBusinessCloudApiConfiguration")["AppName"],
    Version = builder.Configuration.GetSection("WhatsAppBusinessCloudApiConfiguration")["Version"]
};
#pragma warning restore CS8601 // Possible null reference assignment.

builder.Services.AddWhatsAppBusinessCloudApiService(whatsAppConfig);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
