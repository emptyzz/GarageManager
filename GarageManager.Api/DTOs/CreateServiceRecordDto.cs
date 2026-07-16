using System.ComponentModel.DataAnnotations;

namespace GarageManager.Api.DTOs;

public record CreateServiceRecordDto(
    DateTime Date,
    [Required, StringLength(100)] string Description,
    [Range(0, double.MaxValue)] decimal Cost,
    [Range(0, int.MaxValue)] int MileageKm
    );