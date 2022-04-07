using EldenRingArmorOptimizer;
using EldenRingOptimizer.Engine.Mappers;
using EldenRingOptimizer.Engine.Records;
using EldenRingOptimizer.Engine.Repositories;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

var builder = WebAssemblyHostBuilder.CreateDefault(args);

builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(_ => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
builder.Services.AddScoped<IMapper<JsonArmorPiece, ArmorPiece>, ArmorPieceMapper>();
builder.Services.AddScoped<IArmorPieceRepository, ArmorPieceRepository>();

await builder.Build().RunAsync();
