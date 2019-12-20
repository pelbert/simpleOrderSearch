﻿using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using tuya.Models;

public class DataGenerator
{

    public static void Initialize(System.IServiceProvider serviceProvider)
    {
        using (var context = new OrderContext(
            serviceProvider.GetRequiredService<DbContextOptions<OrderContext>>()))
        {
            context.Orders.AddRange(
                new Order
                {
                    OrderID = 36,
                    ShipperID = 4,
                    DriverID = 35,
                    CompletionDate = "2018-01-12T05:10:00",
                    Status = 10,
                    Code = "R4C877FF",
                    MSA = 1,
                    Duration = "92.00",
                    OfferType = 1

                },
                new Order
                {
                    OrderID = 37,
                    ShipperID = 4,
                    DriverID = 243,
                    CompletionDate = "2018-02-15T05:10:00",
                    Status = 10,
                    Code = "R47077FF",
                    MSA = 1,
                    Duration = "43.00",
                    OfferType = 1
                },
                new Order
                {
                    OrderID = 38,
                    ShipperID = 4,
                    DriverID = 35,
                    CompletionDate = "2018-01-31T05:10:00",
                    Status = 10,
                    Code = "R6453FF",
                    MSA = 2,
                    Duration = "120.00",
                    OfferType = 1
                },
                new Order
                {
                    OrderID = 39,
                    ShipperID = 4,
                    DriverID = 35,
                    CompletionDate = "2018-01-31T05:10:00",
                    Status = 10,
                    Code = "R4C877DS",
                    MSA = 4,
                    Duration = "15.00",
                    OfferType = 1
                },
                new Order
                {
                    OrderID = 40,
                    ShipperID = 4,
                    DriverID = 35,
                    CompletionDate = "2018-01-31T05:10:00",
                    Status = 10,
                    Code = "R4C9999F",
                    MSA = 1,
                    Duration = "111.00",
                    OfferType = 1
                },
                new Order
                {
                    OrderID = 41,
                    ShipperID = 67,
                    DriverID = 35,
                    CompletionDate = "2018-01-31T05:10:00",
                    Status = 10,
                    Code = "R4C87S32",
                    MSA = 1,
                    Duration = "54.00",
                    OfferType = 1
                },
                new Order
                {
                    OrderID = 42,
                    ShipperID = 4,
                    DriverID = 35,
                    CompletionDate = "2018-01-31T05:10:00",
                    Status = 10,
                    Code = "R4C87123",
                    MSA = 1,
                    Duration = "92.00",
                    OfferType = 1
                },
                new Order
                {
                    OrderID = 43,
                    ShipperID = 4,
                    DriverID = 35,
                    CompletionDate = "2018-01-31T05:10:00",
                    Status = 10,
                    Code = "R42G77FF",
                    MSA = 1,
                    Duration = "40.00",
                    OfferType = 1
                },
                new Order
                {
                    OrderID = 44,
                    ShipperID = 4,
                    DriverID = 35,
                    CompletionDate = "2018-01-31T05:10:00",
                    Status = 10,
                    Code = "R4002WFF",
                    MSA = 1,
                    Duration = "92.00",
                    OfferType = 2
                },
                new Order
                {
                    OrderID = 45,
                    ShipperID = 4,
                    DriverID = 35,
                    CompletionDate = "2018-01-31T05:10:00",
                    Status = 20,
                    Code = "R400KHFF",
                    MSA = 3,
                    Duration = "23.00",
                    OfferType = 1
                },
                new Order
                {
                    OrderID = 46,
                    ShipperID = 24,
                    DriverID = 35,
                    CompletionDate = "2018-01-31T05:10:00",
                    Status = 61,
                    Code = "R4C437FF",
                    MSA = 1,
                    Duration = "92.00",
                    OfferType = 1
                },
                new Order
                {
                    OrderID = 47,
                    ShipperID = 121,
                    DriverID = 35,
                    CompletionDate = "2018-03-1T05:10:00",
                    Status = 10,
                    Code = "R422AQF",
                    MSA = 1,
                    Duration = "66.00",
                    OfferType = 2
                }

            );

            context.SaveChanges();
        }
    }
}