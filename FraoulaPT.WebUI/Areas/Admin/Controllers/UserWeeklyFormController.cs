using FraoulaPT.Core.Abstracts;
using FraoulaPT.DTOs.UserWeeklyFormDTOs;
using FraoulaPT.Entity;
using FraoulaPT.Services.Abstracts;
using FraoulaPT.Services.Concrete;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace FraoulaPT.WebUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin,Coach")]
    public class UserWeeklyFormController : Controller
    {
        private readonly IUserWeeklyFormService _formService;
        private readonly IUnitOfWork _unitOfWork;
        private readonly UserManager<AppUser> _userManager;

        public UserWeeklyFormController(IUserWeeklyFormService formService,IUnitOfWork unitOfWork,UserManager<AppUser> userManager)
        {
            _formService = formService;
            _unitOfWork = unitOfWork;
            _userManager = userManager;
        }

        public async Task<IActionResult> Index()
        {
            var forms = await _formService.GetAllForAdminAsync();
            return View(forms); // @model List<UserWeeklyFormAdminListDTO>
        }
        [HttpGet]
        public async Task<IActionResult> UserForms(Guid userId)
        {
            var forms = await _formService.GetListByUserAsync(userId);
            return View(forms);
        }

        [HttpGet]
        public async Task<IActionResult> Detail(Guid id)
        {
            var form = await _formService.GetDetailWithPhotosByIdAsync(id);
            if (form == null)
                return NotFound();

            return View(form); // Areas/Admin/Views/UserWeeklyForm/Detail.cshtml
        }
        public async Task<IActionResult> GetLastFormPartial(Guid id)
        {
            var dto = await _formService.GetLastFormByUserAsync(id);
            return PartialView("_LastFormDetailsPartial", dto);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateCoachFeedback([FromBody] CoachFeedbackUpdateDTO dto)
        {
            if (dto == null || dto.Id == Guid.Empty || string.IsNullOrEmpty(dto.Feedback))
                return BadRequest("Geçersiz veri");

            var form = await _formService.GetByIdAsync(dto.Id);
            if (form == null)
                return NotFound();

            var coach = await _userManager.GetUserAsync(User);
            if (coach == null)
                return Unauthorized();

            var entity = await _unitOfWork.Repository<UserWeeklyForm>().GetById(dto.Id);
            entity.CoachFeedback = dto.Feedback;
            entity.UpdatedByUserId = coach.Id;
            entity.ModifiedDate = DateTime.UtcNow;
            entity.Status = Core.Enums.Status.Commit;

            await _unitOfWork.Repository<UserWeeklyForm>().Update(entity);
            await _unitOfWork.SaveChangesAsync();

            return Ok(new { success = true });
        }
    }
}
