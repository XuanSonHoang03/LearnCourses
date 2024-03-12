using BusinessObject.Model;
using LearnCourses.Models;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using VNPAY_CS_ASPX;

namespace LearnCourses.Service
{
    public class VnPayService : IVnPayService
    {
        private IConfiguration _config;

        public VnPayService(IConfiguration config) { _config = config; }
        public string CreatePayment(HttpContext context, OrderInfo model)
        {
            var tick = DateTime.Now.Ticks;

            var vnpay = new VnPayLibrary();

            vnpay.AddRequestData("vnp_Version", _config["Vnpay:Version"]);
            vnpay.AddRequestData("vnp_Command", _config["Vnpay:Command"]);
            vnpay.AddRequestData("vnp_TmnCode", _config["Vnpay:Vnp_TmnCode"]);
            vnpay.AddRequestData("vnp_Amount", (model.Amount * 100).ToString()); //Số tiền thanh toán. Số tiền không mang các ký tự phân tách thập phân, phần nghìn, ký tự tiền tệ. Để gửi số tiền thanh toán là 100,000 VND (một trăm nghìn VNĐ) thì merchant cần nhân thêm 100 lần (khử phần thập phân), sau đó gửi sang VNPAY là: 10000000

            vnpay.AddRequestData("vnp_CreateDate", model.CreatedDate.ToString("yyyyMMddHHmmss"));
            vnpay.AddRequestData("vnp_CurrCode", _config["Vnpay:Currency"]);
            vnpay.AddRequestData("vnp_IpAddr", Utils.GetIpAddress(context));
            vnpay.AddRequestData("vnp_Locale", _config["Vnpay:Locales"]);

            vnpay.AddRequestData("vnp_OrderInfo", "Thanh toan don hang:" + model.OrderId);
            vnpay.AddRequestData("vnp_OrderType", "other"); //default value: other
            vnpay.AddRequestData("vnp_ReturnUrl", _config["Vnpay:Vnp_ReturnUrl"]);
            vnpay.AddRequestData("vnp_TxnRef", $"{model.OrderId}_{tick}"); // Mã tham chiếu của giao dịch tại hệ thống của merchant. Mã này là duy nhất dùng để phân biệt các đơn hàng gửi sang VNPAY. Không được trùng lặp trong ngày
            //Add Params of 2.1.0 Version
            var paymentUrl = vnpay.CreateRequestUrl(_config["Vnpay:Vnp_Url"], _config["Vnpay:Vnp_HashSecret"]);

            return paymentUrl;
        }

        public string GetPaymentStatus(string orderInfo)
        {
            throw new NotImplementedException();
        }

        public OrderInfo PaymentExecute(IQueryCollection collection)
        {
            var vnpay = new VnPayLibrary();
            foreach(var(key, value) in collection)
            {
               if(!string.IsNullOrEmpty(key) && key.StartsWith("vnp_"))
                                  {
                   vnpay.AddResponseData(key, value);
               }
            }
            var vnp_orderId = Convert.ToUInt64(vnpay.GetResponseData("vnp_TxnRef").Split('_')[0]);
            var vnp_TransactionNo = Convert.ToUInt64(vnpay.GetResponseData("vnp_TransactionNo"));
            var vnp_SecureHash = collection.FirstOrDefault(x => x.Key == "vnp_SecureHash").Value;
            var vnp_ResponseCode = collection.FirstOrDefault(x => x.Key == "vnp_ResponseCode").Value;
            var vnp_orderInfo = collection.FirstOrDefault(x => x.Key == "vnp_OrderInfo").Value;
            bool checkSignature = vnpay.ValidateSignature(vnp_SecureHash, _config["Vnpay:Vnp_HashSecret"]);

            if (checkSignature)
            {
                var order = new OrderInfo()
                {
                    OrderId = (long) vnp_orderId,
                    Amount = Convert.ToInt64(vnpay.GetResponseData("vnp_Amount")),
                    OrderDesc = vnp_orderInfo,
                    CreatedDate = DateTime.Now,
                    Status = vnp_ResponseCode,
                    PaymentTranId = (long) vnp_TransactionNo,
                    BankCode = vnpay.GetResponseData("vnp_BankCode"),
                    PayStatus = vnpay.GetResponseData("vnp_PayStatus")
                };
                return order;
            } else
            {
                return null;
            }
        }
    }
}
