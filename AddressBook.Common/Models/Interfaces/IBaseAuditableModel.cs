using System;

namespace AddressBook.Common.Models.Interfaces
{
    public interface IBaseAuditableModel
    {
        DateTime CreatedDate { get; set; }

        DateTime? UpdatedDate { get; set; }
    }
}
