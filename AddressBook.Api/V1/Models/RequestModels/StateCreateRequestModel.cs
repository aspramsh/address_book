﻿namespace AddressBook.Api.V1.Models.RequestModels
{
    public class StateCreateRequestModel
    {
        public string Name { get; set; }

        public string Code { get; set; }

        public int CountryId { get; set; }
    }
}
