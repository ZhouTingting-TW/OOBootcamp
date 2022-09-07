namespace OOBootcamp;

public class GraduateParkingBoy
{
    private readonly List<ParkingLot> _parkingLots;
    private int _count;

    public GraduateParkingBoy(List<ParkingLot> parkingLots)
    {
        _parkingLots = parkingLots;
        _count = -1;
    }

    // Write your logic here
    public string Park(Vehicle vehicle)
    {
        MoveToNext();
        _parkingLots[_count % 2].ParkVehicle(vehicle);
        return GetCurrentParkingLot();
    }

    private string GetCurrentParkingLot()
    {
        return _parkingLots[(_count) % 2].Name;
    }

    private void MoveToNext()
    {
        _count++;
    }
}