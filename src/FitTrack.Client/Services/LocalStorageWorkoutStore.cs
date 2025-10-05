using System.Text.Json;
using FitTrack.Core.Models;
using Microsoft.JSInterop;

namespace FitTrack.Client.Services;

public class LocalStorageWorkoutStore : IWorkoutStore
{
    private const string Key = "fittrack.workouts.v1";
    private readonly IJSRuntime _js;
    private readonly JsonSerializerOptions _json = new() { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };

    public LocalStorageWorkoutStore(IJSRuntime js) => _js = js;

    public async Task<IReadOnlyList<Workout>> GetAllAsync()
    {
        var json = await _js.InvokeAsync<string?>("localStorage.getItem", Key);
        if (string.IsNullOrWhiteSpace(json))
        {
            // Seed a couple example entries on first run
            var seed = new List<Workout>
            {
                new() { Date = DateTime.Today.AddDays(-1), Type = WorkoutType.Run, DurationMin = 40, DistanceKm = 8.0, Notes = "Easy run" },
                new() { Date = DateTime.Today.AddDays(-2), Type = WorkoutType.Strength, DurationMin = 30, Notes = "Full body" }
            };
            await SaveAsync(seed);
            return seed;
        }

        return JsonSerializer.Deserialize<List<Workout>>(json, _json) ?? new List<Workout>();
    }

    public async Task AddAsync(Workout w)
    {
        var list = (await GetAllAsync()).ToList();
        list.Add(w);
        await SaveAsync(list);
    }

    public async Task RemoveAsync(Guid id)
    {
        var list = (await GetAllAsync()).Where(x => x.Id != id).ToList();
        await SaveAsync(list);
    }

    public async Task ClearAsync()
    {
        await _js.InvokeVoidAsync("localStorage.removeItem", Key);
    }

    private async Task SaveAsync(List<Workout> list)
    {
        var json = JsonSerializer.Serialize(list, _json);
        await _js.InvokeVoidAsync("localStorage.setItem", Key, json);
    }
}