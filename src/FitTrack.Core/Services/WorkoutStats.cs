using FitTrack.Core.Models;

namespace FitTrack.Core.Services;

public static class WorkoutStats
{
    // Pace (min/km). Returns null if distance is missing or zero.
    public static double? PaceMinPerKm(Workout w)
        => (w.DistanceKm is > 0) ? w.DurationMin / w.DistanceKm : null;

    public static double TotalDistanceKm(IEnumerable<Workout> items)
        => items.Where(i => i.DistanceKm.HasValue).Sum(i => i.DistanceKm!.Value);

    public static int TotalDurationMin(IEnumerable<Workout> items)
        => items.Sum(i => i.DurationMin);
}