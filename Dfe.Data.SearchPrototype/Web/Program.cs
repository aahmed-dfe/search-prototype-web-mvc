using Dfe.Data.Common.Infrastructure.CognitiveSearch;
using Dfe.Data.SearchPrototype.Common.Mappers;
using Dfe.Data.SearchPrototype.Infrastructure;
using Dfe.Data.SearchPrototype.SearchForEstablishments;
using Dfe.Data.SearchPrototype.SearchForEstablishments.ByKeyword.Usecase;
using Dfe.Data.SearchPrototype.SearchForEstablishments.Models;
using Dfe.Data.SearchPrototype.Web.Mappers;
using Dfe.Data.SearchPrototype.Web.Models.Factories;
using Dfe.Data.SearchPrototype.Web.Models.ViewModels;
using GovUk.Frontend.AspNetCore;
using Models = Dfe.Data.SearchPrototype.Web.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddGovUkFrontend();

// Start of IOC container registrations
//
//
builder.Services.AddDefaultSearchFilterServices(builder.Configuration);
builder.Services.AddDefaultCognitiveSearchServices(builder.Configuration);
builder.Services.AddCognitiveSearchAdaptorServices(builder.Configuration);
builder.Services.AddSearchForEstablishmentServices();
builder.Services.AddScoped<ISearchResultsFactory, SearchResultsFactory>();
builder.Services.AddSingleton<IMapper<EstablishmentResults?, List<Models.ViewModels.Establishment>?>, EstablishmentResultsToEstablishmentsViewModelMapper>();
builder.Services.AddSingleton<IMapper<FacetsAndSelectedFacets, List<Facet>?>, FacetsAndSelectedFacetsToFacetsViewModelMapper>();
builder.Services.AddSingleton<IMapper<Dictionary<string, List<string>>, IList<FilterRequest>>, SelectedFacetsToFilterRequestsMapper>();
//
//
// End of IOC container registrations

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

namespace Dfe.Data.SearchPrototype.Web
{
    public partial class Program { }
}