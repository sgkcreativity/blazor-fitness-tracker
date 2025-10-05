using FitTrack.Core.Models;

namespace FitTrack.Client.Services;

public interface IWorkoutStore
{
    Task<IReadOnlyList<Workout>> GetAllAsync();
    
    Task AddAsync(Workout w);

    Task RemoveAsync(Guid id);
    Task ClearAsync();
}