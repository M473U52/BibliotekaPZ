using Microsoft.AspNetCore.Mvc.RazorPages;
using Biblioteka.Models;
using Biblioteka.Repositories.Interfaces;
using Microsoft.AspNetCore.Identity;
using Biblioteka.Areas.Identity.Data;
using Microsoft.AspNetCore.Authorization;
using Biblioteka.Context;
using Microsoft.AspNetCore.Mvc;

namespace Biblioteka.Pages.RoomTypes
{
    public class DetailModel : PageModel
    {
        private IRoomTypeRepository _roomTypes;
        private IRoomRepository _roomRepository;

        public RoomType? roomType { get; set; }
        private List<Room> rooms { get; set; }

        public int roomCount = 0;
        public int maxSeats = 0;

        public DetailModel(IRoomTypeRepository roomTypes, IRoomRepository roomRepository)
        {
            _roomTypes = roomTypes;
            _roomRepository = roomRepository;
        }



        public async Task OnGet(int id)
        {
            
            roomType = _roomTypes.getOne(id);
            if(roomType == null) {
                roomType = _roomTypes.getOne(1);
            }

            rooms = _roomRepository.getAll();
            foreach(Room room in rooms)
            {
                if (room.roomTypeId == id)
                {
                    roomCount++;
                    maxSeats += room.seatCount;
                }
            }
        }
    }
}
