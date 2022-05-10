using DeskBooker.Core.Domain;
using DeskBooker.DataAccess.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace DeskBooker.DataAccess.Tests.Repositories
{
    public class DeskBookingRepositoryTests
    {
        [Fact]
        public void ShouldSaveTheDeskBooking()
        {
            //Arrange
            var options = new DbContextOptionsBuilder<DeskBookerContext>()
                .UseInMemoryDatabase(databaseName: "ShouldSaveTheDeskBooking")
                .Options;
            var deskBooking = new DeskBooking
            {
                FirstName = "Thomas",
                LastName = "Huber",
                Date = new DateTime(2020, 1, 25),
                Email = "thomas@thomasclaudiushuber.com",
                DeskId = 1
            };



            //Act
            using (var context = new DeskBookerContext(options))
            {
                var repository = new DeskBookingRepository(context);
                repository.Save(deskBooking);
            }

            // Assert
            using (var context = new DeskBookerContext(options))
            {
                var bookings = context.DeskBooking.ToList();
                int cnt = bookings.Count;
                Assert.Equal(1, cnt);
                var storedDeskBooking = bookings[0];

                Assert.Equal(deskBooking.FirstName, storedDeskBooking.FirstName);
                Assert.Equal(deskBooking.LastName, storedDeskBooking.LastName);
                Assert.Equal(deskBooking.Email, storedDeskBooking.Email);
                Assert.Equal(deskBooking.DeskId, storedDeskBooking.DeskId);
                Assert.Equal(deskBooking.Date, storedDeskBooking.Date);
            }
        }


        [Fact]
        public void ShouldGetAllOrderedByDate()
        {
            //Arrange
            var options = new DbContextOptionsBuilder<DeskBookerContext>()
                .UseInMemoryDatabase(databaseName: "ShouldGetAllOrderedByDate")
                .Options;
            var storedList = new List<DeskBooking>
            {
                CreateDeskBooking(1,new DateTime(2020,1,27)),
                CreateDeskBooking(2,new DateTime(2020, 1, 25)),
                CreateDeskBooking(3,new DateTime(2020, 1, 29))

            };
            var expectedList = storedList.OrderBy(x => x.Date).ToList();

            using (var context = new DeskBookerContext(options))
            {
                foreach (var deskBooking in storedList)
                {
                    context.Add(deskBooking);
                    context.SaveChanges();
                }
            }
            // Act
            List<DeskBooking> actualList;
            using (var context = new DeskBookerContext(options))
            {
                var repository = new DeskBookingRepository(context);
                actualList = repository.GetAll().ToList();
            }

            // Assert
            var bb = new DeskBookingEqualityComparer();
            //Assert.Equal(expectedList, actualList, new DeskBookingEqualityComparer());
            //CollectionAssert.AreEqual(expectedList, actualList, new DeskBookingEqualityComparer());
            Assert.Equal(expectedList, actualList);


        }
        private class DeskBookingEqualityComparer : IComparer
        {
            public int Compare(object x, object y)
            {
                var deskBooking1 = (DeskBooking)x;
                var deskBooking2 = (DeskBooking)y;
                if (deskBooking1.Id > deskBooking2.Id)
                    return 1;
                if (deskBooking1.Id < deskBooking2.Id)
                    return -1;
                else
                    return 0;
            }
        }
        private DeskBooking CreateDeskBooking(int id, DateTime dateTime)
        {
            return new DeskBooking
            {
                Id = id,
                FirstName = "Thomas",
                LastName = "Huber",
                Date = dateTime,
                Email = "thomas@thomasclaudiushuber.com",
                DeskId = 1
            };
        }
    }
}
