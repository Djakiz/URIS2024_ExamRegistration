using System.Reflection;
using URIS2024_ExamRegistration.Data;
using URIS2024_ExamRegistration.Profiles;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(setupAction =>
{
    setupAction.SwaggerDoc("ExamRegistrationOpenApiSpecification",
        new Microsoft.OpenApi.Models.OpenApiInfo()
        {
            Title = "Exam Registration API",
            Version = "1",
            Description = "API koji moze da vrsi prijavu ispita, modifikaciju prijava i pregled kreiranih prijava.",
            Contact = new Microsoft.OpenApi.Models.OpenApiContact()
            {
                Name = "Nikola Djakovic",
                Email = "nekimejl@gmail.com",
                Url = new Uri("http://ftn.uns.ac.rs")
            },
            License = new Microsoft.OpenApi.Models.OpenApiLicense()
            {
                Name = "FTN licence",
                Url = new Uri("http://ftn.uns.ac.rs")

            },
            TermsOfService = new Uri("http://ftn.uns.ac.rs")
        });

    // Refleksija - kako da indirektnim putem dodjemo do naziva naseg projekta
    var xmlCommentsFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";

    var xmlCommentsFullPath = Path.Combine(AppContext.BaseDirectory, xmlCommentsFile);

    setupAction.IncludeXmlComments(xmlCommentsFullPath);

});

// Navodimo da se svaki put prilikom koriscnja IExamRegistrationRepository interfejsa koristi jedna instanca ExamRegistrationRepository klase
// Ovo je neophodno kako bi se izbeglo pravljenje novih instanci klase prilikom svakog poziva
// Kada se budemo povezali sa bazom podataka, AddSingleton metodu zamenjujemo sa AddScoped
builder.Services.AddSingleton<IExamRegistrationRepository, ExamRegistrationRepository>();

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());



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
