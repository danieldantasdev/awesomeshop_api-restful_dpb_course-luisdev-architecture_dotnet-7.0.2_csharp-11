using Behavioral.Application.Models;
using Behavioral.Infrastructure.Payments.Models;

namespace Behavioral.Infrastructure.Payments;

public class PaymentSlipService : IPaymentService
{
    public object Process(OrderInputModel model)
    {
        // Recebe os dados de Boleto de uma API Externa, por exemplo

        var paymentSlip = new PaymentSlipModel(
            "12312.23214521-1.232152131", "12324124", DateTime.Now, DateTime.Now.AddDays(3), 1234.0m,
            new Payer("Pagador", "123.567.899-10", "Rua do Pagador"),
            new Receiver("Recebedor", "987.654.321-01", "Rua do Recebedor")
        );

        var builder = new PaymentSlipBuilder();

        var paymentSlipWithBuilder = builder
            .Start()
            .WithPayer(new Payer("Pagador", "123.567.899-10", "Rua do Pagador"))
            .WithReceiver(new Receiver("Recebedor", "987.654.321-01", "Rua do Recebedor"))
            .WithPaymentDocument("12312.23214521-1.232152131", "12324124", 1234.0m)
            .WithDates(DateTime.Now, DateTime.Now.AddDays(3))
            .Build();

        return "Dados do Boleto";
    }
}