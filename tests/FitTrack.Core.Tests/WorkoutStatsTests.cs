using FitTrack.Core.Models;
using FitTrack.Core.Services;
using Xunit;

public class WorkoutStatsTests
{
    [Fact]
    public void Pace_Computes_MinPerKm()
    {
        var w = new Workout { DurationMin = 50, DistanceKm = 10, Type = WorkoutType.Run };
        var pace = WorkoutStats.PaceMinPerKm(w);
        Assert.NotNull(pace);
        Assert.InRange(pace!.Value, 4.9, 5.1); // ~5:00 / km
    }

    [Fact]
    public void Totals_Work()
    {
        var list = new []
        {
            new Workout { DurationMin = 30, DistanceKm = 5,  Type = WorkoutType.Run },
            new Workout { DurationMin = 45, DistanceKm = 8,  Type = WorkoutType.Run },
            new Workout { DurationMin = 20, DistanceKm = null, Type = WorkoutType.Strength }
        };

        Assert.Equal(13, (int)WorkoutStats.TotalDistanceKm(list)); // 5 + 8
        Assert.Equal(95, WorkoutStats.TotalDurationMin(list));     // 30 + 45 + 20
    }
}