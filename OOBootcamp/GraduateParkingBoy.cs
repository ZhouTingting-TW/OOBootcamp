using System.Collections.Concurrent;

namespace OOBootcamp;

public class GraduateParkingBoy
{
    private readonly List<ParkingLot> _parkingLots;
    private int _count;
    private ConcurrentDictionary<Vehicle, ParkingLot> _parkingInfo;

    public GraduateParkingBoy(List<ParkingLot> parkingLots)
    {
        _parkingLots = parkingLots;
        _count = 0;
        _parkingInfo = new ConcurrentDictionary<Vehicle, ParkingLot>();
    }
    
    public void Park(Vehicle vehicle)
    {
        int moveToNext = 0;
        while (!_parkingLots[_count % _parkingLots.Count].ParkVehicle(vehicle))
        {
            moveToNext++;
            if (moveToNext >= _parkingLots.Count)
            {
                throw new AllParkingPlotsAreFullException();
            }
            _count += moveToNext;
        }

        ParkingLot currentParkingLot = _parkingLots[_count % _parkingLots.Count];
        if (_parkingInfo.TryAdd(vehicle, currentParkingLot))
        {
            _count++;
        }
    }

    public string? GetCurrentParkingLotName(string licensePlate)
    {
        var vehicle = new Vehicle(licensePlate);
        if (_parkingInfo.TryGetValue(vehicle, out var parkingLot))
        {
            return parkingLot.Name;
        }

        return null;
    }

    public void RetrieveCars(Vehicle vehicle)
    {
        if (_parkingInfo.TryGetValue(vehicle, out var parkingLotToBeReleased))
        {
            parkingLotToBeReleased.RetrieveVehicle(vehicle);
            _parkingInfo.TryRemove(vehicle, out var parkingLot2);
            return;
        }
        throw new VehicleNotFoundException(vehicle);
        
    }
    
}