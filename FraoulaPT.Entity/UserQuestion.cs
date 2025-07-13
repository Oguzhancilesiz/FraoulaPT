using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FraoulaPT.Entity
{
    public class UserQuestion : BaseEntity
    {
        public Guid UserPackageId { get; set; }
        public UserPackage UserPackage { get; set; }
        public Guid AskedByUserId { get; set; }
        public AppUser AskedByUser { get; set; }
        public Guid? AnsweredByCoachId { get; set; }
        public AppUser AnsweredByCoach { get; set; }
        public string QuestionText { get; set; }
        public string AnswerText { get; set; }
        public DateTime AskedAt { get; set; }
        public DateTime? AnsweredAt { get; set; }
    }
}
