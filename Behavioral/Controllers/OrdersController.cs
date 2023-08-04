using Behavioral.Application.ChainOfResponsibility;
using Behavioral.Application.Configurations;
using Behavioral.Application.Models;
using Behavioral.Application.TemplateMethods;
using Behavioral.Core.Entities.States;
using Behavioral.Infrastructure;
using Behavioral.Infrastructure.Payments;
using Behavioral.Infrastructure.Payments.Strategies;
using Behavioral.Infrastructure.Products;
using Microsoft.AspNetCore.Mvc;
using FraudCheckModel = Behavioral.Infrastructure.Payments.Models.FraudCheckModel;

namespace Behavioral.Controllers;

[ApiController]
[Route("api/orders")]
public class OrdersController : ControllerBase
{
    [HttpPost("singleton")]
    public IActionResult PostSingleton(
        OrderInputModel model
    )
    {
        return Ok(BusinessHours.GetInstance());
    }

    [HttpPost("not-using-chain")]
    public IActionResult Post(
        OrderInputModel model,
        [FromServices] IProductRepository productRepository,
        [FromServices] IPaymentFraudCheckService fraudCheckService,
        [FromServices] ICustomerRepository customerRepository)
    {
        var itemsDictionary = model.Items.ToDictionary(d => d.ProductId, d => d.Quantity);
        var hasStock = productRepository.HasStock(itemsDictionary);

        if (!hasStock)
            return BadRequest();

        var customer = customerRepository.GetCustomerById(model.Customer.Id);
        var customerAllowedToBuy = customer.IsAllowedToBuy();

        if (!customerAllowedToBuy)
            return BadRequest();

        var isFraud = fraudCheckService.IsFraud(model);

        if (isFraud)
            return BadRequest();

        return NoContent();
    }

    [HttpPost("using-chain")]
    public IActionResult PostWithChain(
        OrderInputModel model,
        [FromServices] IProductRepository productRepository,
        [FromServices] IPaymentFraudCheckService fraudCheckService,
        [FromServices] ICustomerRepository customerRepository)
    {
        var validateCustomerHandler = new ValidateCustomerHandler(customerRepository);
        var validateStockHandler = new ValidateStockHandler(productRepository);
        var checkForFraudHandler = new CheckForFraudHandler(fraudCheckService);

        validateCustomerHandler
            .SetNext(validateStockHandler)
            .SetNext(checkForFraudHandler);

        var success = validateCustomerHandler.Handle(model);

        if (!success)
            return BadRequest();

        return NoContent();
    }

    [HttpPost("payment-using-strategy")]
    public IActionResult ProcessPaymentWithStrategy(OrderInputModel model, [FromServices] IPaymentContext context,
        [FromServices] IPaymentStrategyFactory factory)
    {
        var strategy = factory.GetStrategy(model.PaymentInfo.PaymentMethod);

        var result = context
            .SetStrategy(strategy)
            .Process(model);

        return Ok(result);
    }


    [HttpPost("not-using-command")]
    public IActionResult NotPostUsingCommand(
        OrderInputModel model,
        [FromServices] IPaymentFraudCheckService fraudCheckService)
    {
        var total = model.Items.Sum(i => i.Price * i.Quantity);

        var isFraud =
            fraudCheckService.IsFraudV2(total, model.Customer.Id, model.Customer.FullName, model.Customer.Document);

        if (isFraud)
            return BadRequest();

        var message = new
        {
            total,
            customerId = model.Customer.Id,
            fullName = model.Customer.FullName,
            document = model.Customer.Document
        };

        // Chamar um serviço de mensageria para enviar esse objeto como JSON
        // Guardar um log desse objeto

        return NoContent();
    }

    [HttpPost("using-command")]
    public IActionResult PostUsingCommand(
        OrderInputModel model,
        [FromServices] IPaymentFraudCheckService fraudCheckService)
    {
        var total = model.Items.Sum(i => i.Price * i.Quantity);
        var command = new FraudCheckModel(total, model.Customer.Id, model.Customer.FullName, model.Customer.Document);

        var isFraud = fraudCheckService.IsFraudV2UsingCommand(command);

        if (isFraud)
            return BadRequest();

        // Chamar um serviço de mensageria para enviar esse objeto como JSON
        // Guardar um log desse objeto

        return NoContent();
    }

    [HttpPost("state")]
    public IActionResult OrderState(OrderInputModel model)
    {
        var items = model.Items.Select(i => i.ProductId).ToList();

        var context = new OrderStateContext(new OrderStartedState(items));

        context.Handle();

        context.Add(Guid.NewGuid());

        context.Handle();
        context.Handle();
        context.Handle();

        return NoContent();
    }

    [HttpPost("template-method")]
    public IActionResult OrderTemplate(OrderInputModel model)
    {
        WarehouseTemplateMethod templateMethod;

        if (model.IsExternal)
            templateMethod = new WarehouseExternalService(model);
        else
            templateMethod = new WarehouseInternalService(model);

        templateMethod.ExtractOrderData();
        templateMethod.SeparateStockQuantities();
        templateMethod.Notify();

        return NoContent();
    }
}