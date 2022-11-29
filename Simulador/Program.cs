using Microsoft.EntityFrameworkCore;
using Simulador;

internal class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddDbContext<CopaContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("ServerConnection")));


        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        var app = builder.Build();

        app.UseSwagger();
        app.UseSwaggerUI();


        app.UseHttpsRedirection();


        app.MapGet("/teams", async (CopaContext context) =>
        {
            var teams = await context.Teams.ToListAsync();

            Results.Ok(teams);
        });

        app.MapPost("/teams", async (CopaContext context, Team team) =>
        {
            await context.Teams.AddAsync(team);
            await context.SaveChangesAsync();

            Results.Ok(team);
        });

        app.MapPut("/teams/{id}", async (CopaContext context, Team team) =>
        {
            var dbTeam = await context.Teams.FirstAsync(tea m.Id);
            if (dbTeam == null)
                Results.NotFound();

            dbTeam.Name = team.Name;
            dbTeam.Img = team.Img;

            context.Teams.Update(dbTeam);
            await context.SaveChangesAsync();

            Results.Ok(dbTeam);

        });

        app.MapDelete("/teams", async (CopaContext context) =>
        {

        });

        app.Run();
    }
}

public class Team
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Img { get; set; }
}

