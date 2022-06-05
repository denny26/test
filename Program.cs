using first_asp_dot_net_project.Controllers;
using first_asp_dot_net_project.Data;
using first_asp_dot_net_project.Models;

var builder = WebApplication.CreateBuilder(args);

// Register of the necessary services
builder.Services.Configure<DatabaseSettings>(builder.Configuration.GetSection("DatabaseSettings"));
builder.Services.AddSingleton<MoviesController>();

var app = builder.Build();

// Routes
app.MapGet("/", () => "Movies API!");

app.MapGet("/api/movies", async (MoviesController moviesController) =>
{
    List<Movie> movies = await moviesController.Get();
    return Results.Ok(movies);
});

app.MapGet("/api/movies/{id}", async (MoviesController movieController, string id) =>
{
    Movie movie = await movieController.Get(id);
    
    if (movie == null)
    { 
        return Results.NotFound(new { message = $"Movie with id {id} not found!"});
    }

    return Results.Ok(movie);
});

app.MapPost("/api/movies", async (MoviesController movieController, Movie movie) =>
{
    await movieController.Create(movie); 
    return Results.Ok(movie);
});

app.MapPut("/api/movies/{id}", async (MoviesController movieController, string id, Movie updatedMovie) =>
{
    Movie movie = await movieController.Get(id);

    if (movie == null)
    {
        return Results.NotFound(new { message = $"Movie with id {id} not found!" });
    }

    updatedMovie.Id = movie.Id;
    await movieController.Update(id, updatedMovie);

    return Results.Ok(updatedMovie);
});

app.MapDelete("/api/movies/{id}", async (MoviesController moviesController, string id) =>
{
    Movie movie = await moviesController.Get(id);

    if (movie == null)
    {
        return Results.NotFound(new { message = $"Movie with id {id} not found!" });
    }

    await moviesController.Delete(id);
    return Results.Ok(new { message = "The movie has been deleted!" });
});

app.Run();
