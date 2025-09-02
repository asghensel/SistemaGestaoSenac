using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Senac.GestaoEscolar.Domain.Repositories.Alunos;
using Senac.GestaoEscolar.Domain.Repositories.Cursos;
using Senac.GestaoEscolar.Domain.Repositories.Matriculas;
using Senac.GestaoEscolar.Domain.Repositories.Professores;
using Senac.GestaoEscolar.Domain.Services.Alunos;
using Senac.GestaoEscolar.Domain.Services.Cursos;
using Senac.GestaoEscolar.Domain.Services.Matriculas;
using Senac.GestaoEscolar.Domain.Services.Professores;
using Senac.GestaoEscolar.Infra.Data.DataBaseConfigurations;
using Senac.GestaoEscolar.Infra.Data.Repositories;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

var nomeDaPoliticaDeCors = "PermitirFrontend";
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: nomeDaPoliticaDeCors,
        policy =>
        {
            policy.WithOrigins("http://localhost:8080", "http://127.0.0.1:5500")
                  .AllowAnyHeader()
                  .AllowAnyMethod();
        });
});

builder.Services.AddControllers();
builder.Services.AddScoped<IDbConnectionFactory>(x =>
    new DbConnectionFactory(builder.Configuration.GetConnectionString("DefaultConnection"))
);


builder.Services.AddScoped<IAlunoRepository, AlunoRepository>();
builder.Services.AddScoped<IAlunoService, AlunoService>();


builder.Services.AddScoped<IProfessorRepository, ProfessorRepository>();
builder.Services.AddScoped<IProfessorService, ProfessorService>();

builder.Services.AddScoped<ICursoRepository, CursoRepository>();
builder.Services.AddScoped<ICursoService, CursoService>();
builder.Services.AddScoped<IMatriculaRepository, MatriculaRepository>();
builder.Services.AddScoped<IMatriculaService, MatriculaService>();

builder.Services.AddEndpointsApiExplorer();


builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new() { Title = "Gerenciamento Escolar", Version = "v1" });

    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        Scheme = "bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "Insira 'Bearer ' seguido pelo seu token JWT. Exemplo: Bearer {seu_token}"
    });

    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference { Type = ReferenceType.SecurityScheme, Id = "Bearer" }
            },
            Array.Empty<string>()
        }
    });
});

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = builder.Configuration["Jwt:Issuer"],
            ValidAudience = builder.Configuration["Jwt:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
        };
    });

builder.Services.AddAuthorization();


var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();


app.UseCors(nomeDaPoliticaDeCors);
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();