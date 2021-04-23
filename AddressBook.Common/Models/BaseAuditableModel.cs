using AddressBook.Common.Models.Interfaces;
using System;
using System.ComponentModel.DataAnnotations;

namespace AddressBook.Common.Models
{
    public class BaseAuditableModel<T> : BaseModel<T>, IBaseAuditableModel
        where T : new()
    {
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd hh:mm:ss}", ApplyFormatInEditMode = true)]
        [ScaffoldColumn(false)]
        [Required]
        public DateTime CreatedDate { get; set; }

        [ScaffoldColumn(false)]
        public DateTime? UpdatedDate { get; set; }
    }
}
