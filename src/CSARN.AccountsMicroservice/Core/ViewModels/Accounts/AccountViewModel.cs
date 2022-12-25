using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.ViewModels.Accounts
{
    public class AccountViewModel
    {
        public Guid Id { get; set; }
        public Guid PassportId { get; set; }

        public bool IsDeleted { get; set; }
        public bool IsBlocked { get; set; }

        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
