var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

// Establece la dirección base para el cliente HTTP
builder.Services.AddHttpClient("CreditCardAPI", client =>
{
    //La uri podría ser https://localhost:7067  ó http://localhost:5008 dependiendo de 
    //como se tenga configurado en la dirección URL de la aplicación CreditCardAPI
    client.BaseAddress = new Uri("https://localhost:7067/");
});

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
