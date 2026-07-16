using System.ComponentModel.DataAnnotations;

namespace GarageManager.Api.DTOs;

public record UpdateCarDto(
    [Required, StringLength(100)] string Name,
    [Range(0, int.MaxValue)] int MileageKm
    );