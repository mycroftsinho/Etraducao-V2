namespace etraducao.Data.Dto
{
    public class PagamentoDto
    {
        public string _object { get; set; }
        public string id { get; set; }
        public string dateCreated { get; set; }
        public string customer { get; set; }
        public string dueDate { get; set; }
        public string value { get; set; }
        public string netValue { get; set; }
        public string billingType { get; set; }
        public string status { get; set; }
        public string description { get; set; }
        public string externalReference { get; set; }
        public string originalValue { get; set; }
        public string interestValue { get; set; }
        public string originalDueDate { get; set; }
        public string paymentDate { get; set; }
        public string clientPaymentDate { get; set; }
        public string invoiceUrl { get; set; }
        public string bankSlipUrl { get; set; }
        public string invoiceNumber { get; set; }
        public Discount discount { get; set; }
        public Fine fine { get; set; }
        public Interest interest { get; set; }
        public bool deleted { get; set; }
        public bool postalService { get; set; }
        public bool anticipated { get; set; }
    }

    public class Discount
    {
        public string value { get; set; }
        public string dueDateLimitDays { get; set; }
    }

    public class Fine
    {
        public string value { get; set; }
    }

    public class Interest
    {
        public string value { get; set; }
    }

}
