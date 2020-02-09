namespace etraducao.Data.Dto
{

    public class ClienteDto
    {
        public string _object { get; set; }
        public string id { get; set; }
        public string dateCreated { get; set; }
        public string name { get; set; }
        public string email { get; set; }
        public object company { get; set; }
        public string phone { get; set; }
        public string mobilePhone { get; set; }
        public string address { get; set; }
        public string addressNumber { get; set; }
        public string complement { get; set; }
        public string province { get; set; }
        public string postalCode { get; set; }
        public string cpfCnpj { get; set; }
        public string personType { get; set; }
        public string deleted { get; set; }
        public string additionalEmails { get; set; }
        public string externalReference { get; set; }
        public string notificationDisabled { get; set; }
        public string city { get; set; }
        public string state { get; set; }
        public string country { get; set; }
        public string foreignCustomer { get; set; }
    }

}
