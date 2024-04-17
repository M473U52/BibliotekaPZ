using Biblioteka.Areas.Identity.Data;
using Biblioteka.Models;
using Biblioteka.Pages.RoomTypes;
using Biblioteka.Repositories.DbImplementations;
using Biblioteka.Repositories;
using Biblioteka.Repositories.Interfaces;
using FakeItEasy;
using Microsoft.AspNetCore.Identity;
using Biblioteka.Pages.RoomReservations;


namespace Biblioteka.Tests.Sprint2.Library_Frontend
{
    public class ReservationsTests
    {
        [Fact]
        public void RoomTypesTest()
        {
            var roomTypeRepo = A.Fake<IRoomTypeRepository>();
            var bibUser = A.Fake<UserManager<BibUser>>();

            var roomType = new List<RoomType>
            {
                new RoomType { roomTypeId = 1, name = "1_", price = 220 },

                new RoomType { roomTypeId = 2, name = "2_", price = 520 },

                new RoomType { roomTypeId = 3, name = "3_", price = 320 },

                new RoomType { roomTypeId = 4, name = "4_", price = 450 },

                new RoomType { roomTypeId = 5, name = "5_", price = 666 },

            };


            A.CallTo(() => roomTypeRepo.getAll()).Returns(roomType);

            var selectingRoomTypes = new SelectingRoomTypes(bibUser, roomTypeRepo);

            var result = selectingRoomTypes.OnGet();

            Assert.Equal(5, selectingRoomTypes.RoomTypes.Count);
        }

        [Fact]
        public void CreateReservationGetRoomTypeTest()
        {
            var roomTypeRepo = A.Fake<IRoomTypeRepository>();
            var roomRepo = A.Fake<IRoomRepository>();
            var readerRepo = A.Fake<IReaderRepository>();
            var roomReservationRepository = A.Fake<IRoomReservationRepository>();
            var employeeRepository = A.Fake<IEmployeeRepository>();
            var bibUser = A.Fake<UserManager<BibUser>>();

            var roomType = new List<RoomType>
            {
                new RoomType { roomTypeId = 1, name = "1_", price = 220 },

                new RoomType { roomTypeId = 2, name = "2_", price = 520 },

                new RoomType { roomTypeId = 3, name = "3_", price = 320 },

                new RoomType { roomTypeId = 4, name = "4_", price = 450 },

                new RoomType { roomTypeId = 5, name = "5_", price = 666 },
            };

            var rooms = new List<Room>
            {
                new Room { roomNumber = 1, roomTypeId = 1, seatCount = 15 },
                new Room { roomNumber = 2, roomTypeId = 1, seatCount = 15 },
                new Room { roomNumber = 3, roomTypeId = 3, seatCount = 15 },
                new Room { roomNumber = 4, roomTypeId = 5, seatCount = 15 },
                new Room { roomNumber = 5, roomTypeId = 2, seatCount = 15 }

            };

            A.CallTo(() => roomTypeRepo.getAll()).Returns(roomType);
            A.CallTo(() => roomRepo.getAll()).Returns(rooms);

            var createModel = new CreateModel(bibUser, readerRepo, roomReservationRepository, roomRepo, employeeRepository, roomTypeRepo);
            var result = createModel.OnGet(1);

            Assert.Equal(2, createModel.Rooms.Count);
        }
    }
}
