namespace OOBootcamp;

public class SmartParkingBoy
{
    private readonly List<ParkingLot> _parkingLots;
    private readonly Dictionary<Vehicle, ParkingLot> _vehicleLocation;
    
    public SmartParkingBoy(List<ParkingLot> parkingLots)
    {
        _parkingLots = parkingLots;
        _vehicleLocation = new Dictionary<Vehicle, ParkingLot>(50);
    }

    public void ParkVehicle(Vehicle vehicle)
    {
        var maxParkingLots = new List<ParkingLot>(_parkingLots.Count);
        var currentMaxCount = 0;
        foreach (var parkingLot in _parkingLots)
        {
            if (parkingLot.AvailableCount > currentMaxCount)
            {
                maxParkingLots.Clear();
                maxParkingLots.Add(parkingLot);
                currentMaxCount = parkingLot.AvailableCount;
            }
            else if (parkingLot.AvailableCount == currentMaxCount)
            {
                maxParkingLots.Add(parkingLot);
            }
        }

        if (currentMaxCount == 0)
        {
            throw new NoParkingSlotAvailableException();
        }

        var targetParkingLot = maxParkingLots.MaxBy(x => x.AvailableCount * 100.0 / x.MaxCapacity)!;
        if(!targetParkingLot.ParkVehicle(vehicle)) throw new NoParkingSlotAvailableException();
        _vehicleLocation.Add(vehicle, targetParkingLot);
    }

    public double RetrieveVehicle(string licensePlate)
    {
        var vehicle = new Vehicle(licensePlate);
        if (_vehicleLocation.ContainsKey(vehicle))
        {
            return _vehicleLocation[vehicle].RetrieveVehicle(vehicle);
        }

        throw new VehicleNotFoundException(vehicle);
    }
}