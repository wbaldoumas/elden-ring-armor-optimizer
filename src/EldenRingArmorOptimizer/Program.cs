using EldenRingArmorOptimizer;
using EldenRingArmorOptimizer.Engine.Configuration;
using EldenRingArmorOptimizer.Engine.DataTransfer;
using EldenRingArmorOptimizer.Engine.Mappers;
using EldenRingArmorOptimizer.Engine.Records;
using EldenRingArmorOptimizer.Engine.Repositories;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

var builder = WebAssemblyHostBuilder.CreateDefault(args);

builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.Configure<RepositoryConfiguration>(
    options => builder.Configuration.GetSection(RepositoryConfiguration.Key).Bind(options)
);

builder.Services.AddScoped(_ => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
builder.Services.AddScoped<IMapper<ArmorPieceDto, ArmorPiece>, ArmorPieceMapper>();
builder.Services.AddScoped<IMapper<TalismanDto, Talisman>, TalismanMapper>();
builder.Services.AddScoped<IArmorPieceRepository, ArmorPieceRepository>();
builder.Services.AddScoped<ITalismanRepository, TalismanRepository>();
builder.Services.AddScoped<IWeaponRepository, WeaponRepository>();

await builder.Build().RunAsync();
