﻿using Microsoft.AspNetCore.Mvc.RazorPages;
using Biblioteka.Models;
using Biblioteka.Repositories.Interfaces;
using Microsoft.AspNetCore.Identity;
using Biblioteka.Areas.Identity.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Biblioteka.Pages.RoomReservations
{
    
    public class IndexReaderModel : PageModel
    {
        private readonly UserManager<BibUser> _userManager;
        private IRoomReservationRepository _roomReservationRepository;

        //
        public IList<RoomReservation> RoomReservation { get; set; } = default!;

        public IndexReaderModel( UserManager<BibUser> userManager, IRoomReservationRepository roomReservationRepository)
        {
            _userManager = userManager;
            _roomReservationRepository = roomReservationRepository;

            RoomReservation = new List<RoomReservation>();
        }

        

        public async Task OnGet()
        {

            if (User.IsInRole("Reader"))
            {
               var email = _userManager.GetUserName(User);
                RoomReservation = _roomReservationRepository.getAllOfUser(email);
            }
        }

        public IActionResult OnPostDeleteBorrowing(int reservationId)
        {
            var reservation = _roomReservationRepository.getOne(reservationId);

            if (reservation != null)
            {

                TempData["Message"] = $"Success/Pomyślnie usunięto wynajęcie sali: \"{reservation.roomId}\" " +
                                $"z dnia {reservation.begginingOfReservationDate.ToString("dd.MM.yyyy")}.";

                _roomReservationRepository.Delete(reservationId);
            }
            else
            {
                TempData["Message"] = $"Error/Brak sali o takim ID.";
            }
            return RedirectToPage("./IndexReader");
        }
    }
}
