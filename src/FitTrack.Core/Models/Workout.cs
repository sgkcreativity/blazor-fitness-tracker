using System.ComponentModel.DataAnnotations;

namespace FitTrack.Core.Models;

public class Workout
{
    public Guid Id { get; set; } = Guid.NewGuid();

    [Required] public DateTime Date { get; set; } = DateTime.Today;
    [Required] public WorkoutType Type { get; set; } = WorkoutType.Run;

    // Duration in minutes
    [Range(1, 24*60)]
    public int DurationMin { get; set; }

    // Optional distance in kilometers
    [Range(0, 1000)]
    public double? DistanceKm { get; set; }

    // Optional calories
    [Range(0, 100000)]
    public int? Calories { get; set; }

    [MaxLength(280)]
    public string? Notes { get; set; }
}