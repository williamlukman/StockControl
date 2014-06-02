using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.DomainModel
{
    public partial class CashBank
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }

        public bool IsBank { get; set; }
        public bool IsDeleted { get; set; }
        public decimal Amount { get; set; }

        public DateTime CreatedAt { get; set; }
        public Nullable<DateTime> ModifiedAt { get; set; }
        public Nullable<DateTime> DeletedAt { get; set; }

        public Dictionary<String, String> Errors { get; set; }
    }
}
