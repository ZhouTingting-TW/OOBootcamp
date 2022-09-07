using System.Collections.Generic;
using NUnit.Framework;

namespace OOBootcamp.Test;

public class GraduateParkingBoyTest
{
    private GraduateParkingBoy _graduateParkingBoy;

    [SetUp]
    public void SetUp()
    {
        ParkingLot parkingLotA = new ParkingLot(20, 1, "A");
        ParkingLot parkingLotB = new ParkingLot(20, 1, "B");
        _graduateParkingBoy = new GraduateParkingBoy(new List<ParkingLot>
        {
            parkingLotA, parkingLotB
        });
    }

    [Test]
    public void should_park_to_parking_lot_A_given_first_time_park_the_car()
    {
        Vehicle vehicle = new Vehicle("v1");

        var lotName = _graduateParkingBoy.Park(vehicle);

        Assert.AreEqual("A", lotName);
    }
    
    [Test]
    public void should_park_to_parking_lot_B_given_parked_to_parking_lot_A_last_time()
    {
        Vehicle vehicle = new Vehicle("v1");
        Vehicle vehicle2 = new Vehicle("v2");

        _graduateParkingBoy.Park(vehicle);
        var lotName = _graduateParkingBoy.Park(vehicle2);

        Assert.AreEqual("B", lotName);
    }
    
    
}