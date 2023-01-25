using MobilpayEncryptDecrypt;

namespace Netopia.Logic
{
    public interface IPaymentProcessor
    {
        MobilpayEncrypt CreatePaymentForNetopia();
    }
}