using FraoulaPT.DTOs.DashboardDTOs;
using FraoulaPT.DTOs.UserPackageDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FraoulaPT.Services.Abstracts
{

    public interface IUserPackageService
    {
        Task<bool> CreateAsync(UserPackageCreateDTO dto);
        Task<bool> HasActivePackageAsync(Guid userId);
        Task <List<UserPackageDetailDTO>> GetPackagesByUserAsync(Guid userId);

        Task<UserPackageStatusDTO?> GetActivePackageStatusAsync(Guid userId);
        Task<bool> IncrementUsedMessageAsync(Guid userId);
        Task<bool> IncrementUsedQuestionAsync(Guid userId);
        Task<int?> GetRemainingDaysAsync(Guid userId);

        //dashboard için
        Task<int> GetActivePackageCountAsync();
        Task<decimal> GetMonthlyRevenueAsync();
        Task<int> GetExpiringPackagesCountAsync();
        Task<List<ExpiringPackageDTO>> GetExpiringPackagesAsync(int limit);
        Task<List<LowQuotaUserDTO>> GetLowMessageQuotaUsersAsync(int limit);
        Task<int> GetUnapprovedPaymentsCountAsync();

    }

}
