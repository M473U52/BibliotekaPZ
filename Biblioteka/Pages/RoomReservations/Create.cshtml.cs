using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Biblioteka.Models;
using Biblioteka.Repositories.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Biblioteka.Areas.Identity.Data;
using System.ComponentModel.DataAnnotations;
using Xunit.Sdk;
using Microsoft.VisualBasic;

namespace Biblioteka.Pages.RoomReservations
{
    [Authorize(Roles = "Reader")]
    public class CreateModel : PageModel
    {
        private readonly UserManager<BibUser> _userManager;
        private IRoomReservationRepository _roomReservationRepository;
        private IRoomRepository _roomRepository;
        private IEmployeeRepository _employeeRepository;
        private IReaderRepository _readerRepository;
        private IRoomTypeRepository _roomTypeRepository;

        public CreateModel( UserManager<BibUser> userManager, IReaderRepository readerRepository, IRoomReservationRepository roomReservationRepository, IRoomRepository roomRepository, IEmployeeRepository employeeRepository, IRoomTypeRepository roomTypeRepository)
        {
            _userManager = userManager;
            _roomReservationRepository = roomReservationRepository;
            _roomRepository = roomRepository;
            _employeeRepository = employeeRepository;
            _readerRepository = readerRepository;
            _roomTypeRepository = roomTypeRepository;
        }

        public List<SelectListItem>? Rooms { get; set; }

        [BindProperty]
        public RoomReservationDto RoomReservation { get; set; } = default!;

        public RoomType RoomTypeForReservation { get; set; } = default!;


        [BindProperty]
        public string RoomId { get; set; } = default!;


        [BindProperty]
        public TimeSpan startingTime { get; set; } = default!;
        [BindProperty]
        public TimeSpan endingTime { get; set; } = default!;

        [BindProperty]
        public string AgreementCheckbox { get; set; }
        public IActionResult OnGet(int roomTypeId)
        {

            var roomType = _roomTypeRepository.getOne(roomTypeId);

            if (roomType == null)
            {
                return NotFound();
            }
            else
            {
                RoomTypeForReservation = roomType;
            }

            Rooms = _roomRepository.getAll().Where(r => r.roomTypeId == roomTypeId).Select(r => new SelectListItem { Value = r.roomNumber.ToString(), Text = r.FullData }).ToList();
            //Employees = _bibContext.Employee.Select(r => new SelectListItem { Value = r.employeeId.ToString(), Text = r.FullName }).ToList();
            
            return Page();
        }

        public IActionResult OnPost(int roomTypeId)
        {
            bool isAgreementChecked = AgreementCheckbox == "on";
            if (!isAgreementChecked)
            {
                ModelState.AddModelError("AgreementCheckbox", "Musisz zaakceptować regulamin.");
                return Page();
            }

            var startOfReservation = DateTime.Parse(Request.Form["startOfReservation"]);
            var endOfReservation = DateTime.Parse(Request.Form["endOfReservation"]);

            RoomReservation.begginingOfReservationDate = startOfReservation.Add(RoomReservation.startingTime);
            RoomReservation.endOfReservationDate = endOfReservation.Add(RoomReservation.endingTime);
            
            var roomreservation = RoomReservation;
            RoomReservation newRoomReservation = new RoomReservation()
            {
                readerId = RoomReservation.readerId,
                roomId = RoomReservation.roomId,
                begginingOfReservationDate = RoomReservation.begginingOfReservationDate,
                endOfReservationDate = RoomReservation.endOfReservationDate,
                startingTime = RoomReservation.startingTime,
                endingTime = RoomReservation.endingTime,
                Confirmation = RoomReservation.Confirmation,
            };

            Reader? foundReader = _readerRepository.GetByMail(User.Identity.Name);
            if (foundReader != null && newRoomReservation != null)
                newRoomReservation.reader = foundReader;
            else
                ModelState.AddModelError("", "Wymagana jest osoba składająca rezerwację");


            Room? foundRoom = _roomRepository.getAll().FirstOrDefault(r => r.roomNumber.ToString().Equals(RoomId.ToString()));

            if (foundRoom != null && newRoomReservation != null)
            {
                newRoomReservation.room = foundRoom;
            }


            RoomReservation.Confirmation = false;

            if (IsTerminWrong(newRoomReservation))
                ModelState.AddModelError(" ", "Data początku rezerwacji nie może być później niż data końca rezerwacji lub daty nie mogą być takie same.");

            if (IsReserved(newRoomReservation))
                ModelState.AddModelError("RoomId", "Już ktoś zarezerwował salę w obrębie tego terminu.");

            DateTime now = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, DateTime.Now.Hour, DateTime.Now.Minute, 0);
            if (newRoomReservation != null && DateTime.Compare(newRoomReservation.begginingOfReservationDate, now) < 0)
                ModelState.AddModelError(" ", "Nie można zarezerwować daty początku rezerwacji na dzień, który już był.");

            if (newRoomReservation != null && DateTime.Compare(newRoomReservation.endOfReservationDate, now) < 0)
                ModelState.AddModelError(" ", "Nie można zarezerwować daty końca rezerwacji na dzień, który już był.");

            if (!ModelState.IsValid || newRoomReservation == null || foundReader == null)
            {
               var roomType = _roomTypeRepository.getOne(roomTypeId);

                if (roomType == null)
                {
                    return NotFound();
                }
                else
                {
                    RoomTypeForReservation = roomType;
                }

                Rooms = _roomRepository.getAll().Where(r => r.roomTypeId == roomTypeId).Select(r => new SelectListItem { Value = r.roomNumber.ToString(), Text = r.FullData }).ToList();


                if (foundRoom == null)
                    ModelState.AddModelError("", "Należy wybrać salę.");

                return Page();
            }
            
            _roomReservationRepository.Add(newRoomReservation);

            return RedirectToPage("./IndexReader");
        }

        private bool IsTerminWrong(RoomReservation reservation)
        {
            if (reservation != null)
                return (DateTime.Compare(reservation.begginingOfReservationDate, reservation.endOfReservationDate) >= 0);

            return false;
        }

        private bool IsReserved(RoomReservation toSaveReservation)
        {
            if (toSaveReservation != null)
            {
                foreach (RoomReservation savedReservation in _roomReservationRepository.getAll())
                {

                    if (savedReservation != null && savedReservation.room != null &&
                        savedReservation.room.roomNumber == toSaveReservation.room.roomNumber &&
                        IsTerminIncluded(toSaveReservation, savedReservation.begginingOfReservationDate, savedReservation.endOfReservationDate)
                       )
                    {
                        return true;
                    }
                }

                return false;
            }
            return false;
        }

        private bool IsTerminIncluded(RoomReservation toSaveReservation, DateTime beginningDateRange, DateTime endDateRange)
        {
            if (toSaveReservation != null)
            {
                DateTime startingDate = toSaveReservation.begginingOfReservationDate;
                DateTime endDate = toSaveReservation.endOfReservationDate;

                if (DateTime.Compare(startingDate, beginningDateRange) >= 0 && DateTime.Compare(startingDate, endDateRange) <= 0)
                    return true;
                else if (DateTime.Compare(endDate, beginningDateRange) >= 0 && DateTime.Compare(endDate, endDateRange) <= 0)
                    return true;
                else if (DateTime.Compare(startingDate, beginningDateRange) < 0 && DateTime.Compare(endDate, endDateRange) > 0)
                    return true;
                else
                    return false;
            }

            return false;
        }

    }
}