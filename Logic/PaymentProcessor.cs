using Microsoft.AspNetCore.Hosting;
using MobilpayEncryptDecrypt;
using Netopia.Logic.Test;
using Microsoft.Extensions.Options;

namespace Netopia.Logic
{
    public class PaymentProcessor : IPaymentProcessor
    {
        private readonly PaymentConfiguration _paymentConfiguration;
        private readonly IWebHostEnvironment _hostingEnvironment;

        public PaymentProcessor(IOptions<PaymentConfiguration> paymentConfigurationOption,
            IWebHostEnvironment hostingEnvironment)
        {
            _paymentConfiguration = paymentConfigurationOption.Value;
            _hostingEnvironment = hostingEnvironment;
        }

        public MobilpayEncrypt CreatePaymentForNetopia()
        {
            MobilpayEncrypt encrypt = new MobilpayEncrypt();

            Mobilpay_Payment_Request_Card card = new Mobilpay_Payment_Request_Card();
            Mobilpay_Payment_Invoice invoice = new Mobilpay_Payment_Invoice();
            Mobilpay_Payment_Address billing = new Mobilpay_Payment_Address();
            Mobilpay_Payment_Address shipping = new Mobilpay_Payment_Address();
            Mobilpay_Payment_Invoice_Item itmm = new Mobilpay_Payment_Invoice_Item();
            Mobilpay_Payment_Invoice_Item itmm1 = new Mobilpay_Payment_Invoice_Item();
            Mobilpay_Payment_ItemCollection itmColl = new Mobilpay_Payment_ItemCollection();
            Mobilpay_Payment_Exchange_RateCollection exColl = new Mobilpay_Payment_Exchange_RateCollection();
            Mobilpay_Payment_Exchange_Rate ex = new Mobilpay_Payment_Exchange_Rate();
            Mobilpay_Payment_Request_Contact_Info ctinfo = new Mobilpay_Payment_Request_Contact_Info();
            Mobilpay_Payment_Confirm conf = new Mobilpay_Payment_Confirm();
            Mobilpay_Payment_Request_Url url = new Mobilpay_Payment_Request_Url();

            MobilpayEncryptDecrypt.MobilpayEncryptDecrypt encdecr = new MobilpayEncryptDecrypt.MobilpayEncryptDecrypt();
            card.OrderId = new Random().Next().ToString();
            card.Type = "card";
            card.Signature = _paymentConfiguration.Signature;
            url.ConfirmUrl = _paymentConfiguration.ConfirmUrl;
            url.ReturnUrl = _paymentConfiguration.ReturnUrl;
            //card.Service = service;
            card.Url = url;
            card.TimeStamp = DateTime.Now.ToString("yyyyMMddhhmmss");
            invoice.Amount = 12;
            invoice.Currency = "RON";

            invoice.Details = "ceva";
            invoice.Items = new Mobilpay_Payment_ItemCollection();
            billing.FirstName = "ceva";
            billing.LastName = "ceva";
            billing.IdentityNumber = "ceva";
            billing.FiscalNumber = "ceva";
            billing.MobilPhone = "ceva";
            billing.Type = "person";
            billing.ZipCode = "ceva";
            billing.Iban = "ceva";
            billing.Address = "ceva";
            billing.Bank = "ceva";
            billing.City = "ceva";
            billing.Country = "ceva";
            billing.County = "ceva";
            billing.Email = "ceva@gmail.com";

            shipping.Sameasbilling = "1";


            ctinfo.Billing = billing;
            ctinfo.Shipping = shipping;


            invoice.ContactInfo = ctinfo;


            card.Invoice = invoice;
            //if (String.IsNullOrWhiteSpace(m_UserToken) == false)
            //{
            //    card.Invoice.TokenId = m_UserToken;
            //}

            encrypt.Data = encdecr.GetXmlText(card);
            encrypt.X509CertificateFilePath = GetPathToCertificate();
            //m_Logger.LogInformation($"X509CertificateFilePath= [{encrypt.X509CertificateFilePath}]");
            //encdecr.Encrypt(encrypt);
            //Apelul urmator nu arunca exceptie
            encdecr.EncryptWithCng(encrypt);


            return encrypt;
        }

        private string GetPathToCertificate()
        {
            var contentRootPath = _hostingEnvironment.ContentRootPath.AddBackslash();
            var pathToCertificate = Path.GetFullPath(Path.Combine(contentRootPath, _paymentConfiguration.PathToCertificate));
            return pathToCertificate;
        }
    }
}
