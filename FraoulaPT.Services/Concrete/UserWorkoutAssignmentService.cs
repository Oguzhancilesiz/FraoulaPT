using FraoulaPT.Core.Abstracts;
using FraoulaPT.DTOs.UserWorkoutAssignmentDTOs;
using FraoulaPT.Entity;
using FraoulaPT.Services.Abstracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FraoulaPT.Services.Concrete
{
    public class UserWorkoutAssignmentService : BaseService<
    UserWorkoutAssignment,
    UserWorkoutAssignmentListDTO,
    UserWorkoutAssignmentDetailDTO,
    UserWorkoutAssignmentCreateDTO,
    UserWorkoutAssignmentUpdateDTO>, IUserWorkoutAssignmentService
    {
        public UserWorkoutAssignmentService(IUnitOfWork unitOfWork) : base(unitOfWork) { }
    }

}
