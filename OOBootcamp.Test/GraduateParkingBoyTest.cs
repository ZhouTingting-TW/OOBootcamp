using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using NUnit.Framework;

namespace OOBootcamp.Test;

public class GraduateParkingBoyTest
{
    private GraduateParkingBoy _graduateParkingBoy = null!;

    [SetUp]
    public void SetUp()
    {
        ParkingLot parkingLotA = new ParkingLot(3, 1, "A");
        ParkingLot parkingLotB = new ParkingLot(1, 1, "B");
        _graduateParkingBoy = new GraduateParkingBoy(new List<ParkingLot>
        {
            parkingLotA, parkingLotB
        });
    }

    [Test]
    public void should_park_to_parkingLotA_given_first_time_park_the_car()
    {
        Vehicle vehicle = new Vehicle("v1");

        _graduateParkingBoy.Park(vehicle);

        Assert.AreEqual("A", _graduateParkingBoy.GetCurrentParkingLotName("v1"));
    }

    [Test]
    public void should_park_to_parkingLotB_given_parked_to_parkingLotA_last_time_and_parkingLotB_has_available_space()
    {
        var cars = new List<string> { "v1", "v2" };
        foreach (var car in cars)
        {
            _graduateParkingBoy.Park(new Vehicle(car));
        }

        Assert.AreEqual("A", _graduateParkingBoy.GetCurrentParkingLotName("v1"));
        Assert.AreEqual("B", _graduateParkingBoy.GetCurrentParkingLotName("v2"));
    }

    [Test]
    public void should_park_to_parkingLot_A_given_parked_to_parking_lot_A_last_time_but_parkingLotB_is_full()
    {
        var cars = new List<string> { "v1", "v2", "v3", "v4" };
        foreach (var car in cars)
        {
            _graduateParkingBoy.Park(new Vehicle(car));
        }

        Assert.AreEqual("A", _graduateParkingBoy.GetCurrentParkingLotName("v1"));
        Assert.AreEqual("B", _graduateParkingBoy.GetCurrentParkingLotName("v2"));
        Assert.AreEqual("A", _graduateParkingBoy.GetCurrentParkingLotName("v3"));
        Assert.AreEqual("A", _graduateParkingBoy.GetCurrentParkingLotName("v4"));
    }

    [Test]
    public void should_throw_exception_given_parkingLots_are_all_full()
    {
        var cars = new List<string> { "v1", "v2", "v3", "v4" };

        foreach (var car in cars)
        {
            _graduateParkingBoy.Park(new Vehicle(car));
        }
        
        Assert.Throws<AllParkingPlotsAreFullException>(() => _graduateParkingBoy.Park(new Vehicle("v5")));
    }
    
    [Test]
    public void should_retrieve_a_car_successfully_given_car_is_existed_in_parkingLots()
    {
        var cars = new List<string> { "v1", "v2", "v3", "v4" };

        foreach (var car in cars)
        {
            _graduateParkingBoy.Park(new Vehicle(car));
        }
        
        Assert.Throws<AllParkingPlotsAreFullException>(() => _graduateParkingBoy.Park(new Vehicle("v5")));
    }
    
    
}