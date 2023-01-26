using MobilpayEncryptDecrypt;

namespace Netopia.Logic
{
    public interface IPaymentProcessor
    {
        PaymentResult ConfirmPayment(string textxml, string env_key);
        MobilpayEncrypt CreatePaymentForNetopia();
    }
}