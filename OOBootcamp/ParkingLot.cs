using System.Collections.Concurrent;

namespace OOBootcamp;

public class ParkingLot
{
    public string? Name { get; }
    public double HourlyRate { get; }
    public int AvailableCount => MaxCapacity - _parkedVehicles.Count;
    public int MaxCapacity { get; }
    private readonly ConcurrentDictionary<Vehicle, DateTime> _parkedVehicles;

    public ParkingLot(int maxCapacity, double hourlyRate, string? name)
    {
        MaxCapacity = maxCapacity;
        Name = name;
        HourlyRate = hourlyRate;
        _parkedVehicles = new ConcurrentDictionary<Vehicle, DateTime>();
    }
    
    public bool ParkVehicle(Vehicle vehicle)
    {
        return AvailableCount > 0 && _parkedVehicles.TryAdd(vehicle, DateTime.UtcNow);
    }

    public bool HasVehicle(Vehicle vehicle)
    {
        return _parkedVehicles.ContainsKey(vehicle);
    }
    
    /// <param name="vehicle"></param>
    /// <returns>Parking charge</returns>
    /// <exception cref="VehicleNotFoundException"></exception>
    public double RetrieveVehicle(Vehicle vehicle)
    {
        if (_parkedVehicles.ContainsKey(vehicle) && _parkedVehicles.TryRemove(vehicle, out var parkedDateTime))
        {
            return Math.Ceiling((DateTime.UtcNow - parkedDateTime).TotalMilliseconds / 3600.0) * HourlyRate;
        }

        throw new VehicleNotFoundException(vehicle);
    }
}