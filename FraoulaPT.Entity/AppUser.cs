using FraoulaPT.Core.Abstracts;
using FraoulaPT.Core.Enums;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FraoulaPT.Entity
{
    public class AppUser : IdentityUser<Guid>, IEntity
    {
        public string FullName { get; set; }
        public Status Status { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public int AutoID { get; set; }
        public UserProfile Profile { get; set; }
        public ICollection<UserPackage> UserPackages { get; set; }
        public ICollection<ChatMessage> SentMessages { get; set; }
        public ICollection<ChatMessage> ReceivedMessages { get; set; }
        public ICollection<UserQuestion> AskedQuestions { get; set; }
        public ICollection<UserQuestion> AnsweredQuestions { get; set; }
        public ICollection<WorkoutProgram> WorkoutPrograms { get; set; } // Koçsa
        public virtual ICollection<UserWeeklyForm> UserWeeklyForms { get; set; } = new List<UserWeeklyForm>();
    }
}
