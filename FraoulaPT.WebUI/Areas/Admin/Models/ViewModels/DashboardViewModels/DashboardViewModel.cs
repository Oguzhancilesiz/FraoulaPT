using FraoulaPT.DTOs.DashboardDTOs;

namespace FraoulaPT.WebUI.Areas.Admin.Models.ViewModels.DashboardViewModels
{
    public class DashboardViewModel
    {
        public int ActivePackageCount { get; set; }
        public int ActiveStudentCount { get; set; }
        public int PendingQuestionCount { get; set; }
        public decimal MonthlyRevenue { get; set; }
        public int ExpiringPackagesCount { get; set; }
        public int UnapprovedPaymentsCount { get; set; }
        public int ActiveChatsCount { get; set; }
        public string AvgResponseTime { get; set; }

        public List<TopUserDTO> TopCoaches { get; set; } = new();
        public List<TopUserDTO> TopStudents { get; set; } = new();
        public List<ExpiringPackageDTO> ExpiringPackages { get; set; } = new();
        public List<LowQuotaUserDTO> LowMessageQuotaUsers { get; set; } = new();
        public List<UserActivityDTO> RecentActivities { get; set; } = new();

        public List<string> SalesChartLabels { get; set; } = new();
        public List<int> SalesChartData { get; set; } = new();

        public List<string> IncomeChartLabels { get; set; } = new();
        public List<decimal> IncomeChartData { get; set; } = new();

        public List<string> PackageChartLabels { get; set; } = new();
        public List<int> PackageChartData { get; set; } = new();

        public List<string> RevenueChartLabels { get; set; } = new();
        public List<decimal> RevenueChartData { get; set; } = new();
    }
}
