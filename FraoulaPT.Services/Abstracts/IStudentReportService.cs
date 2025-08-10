using FraoulaPT.DTOs.StudentReportDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FraoulaPT.Services.Abstracts
{
    public interface IStudentReportService
    {
        Task<StudentReportDTO> BuildAsync(Guid userId);
    }
}
