using Xunit;
using Moq;
using ShoeShop.DataAccess.Abstract;
using ShoeShop.Entities.Concrete;
using ShoeShop.Business.Concrete;
using ShoeShop.Business.Abstract;
using System.Collections.Generic;

public class OrderManagerTests
{
    private readonly Mock<IOrderRepository> _mockOrderRepository;
    private readonly Mock<IPaymentCardService> _mockPaymentCardService;
    private readonly Mock<IProductRepository> _mockProductRepository;
    private readonly OrderManager _orderManager;

    public OrderManagerTests()
    {
        _mockOrderRepository = new Mock<IOrderRepository>();
        _mockPaymentCardService = new Mock<IPaymentCardService>();
        _mockProductRepository = new Mock<IProductRepository>();
        _orderManager = new OrderManager(_mockOrderRepository.Object, _mockPaymentCardService.Object, _mockProductRepository.Object);
    }
    [Fact]
    public void GetOrders_ReturnsOrdersForUserFromRepository()
    {
        // Arrange
        int userId = 1;
        var orders = new List<Order> {
            new Order { ID = 1, UserID = userId },
            new Order { ID = 2, UserID = userId },
            new Order { ID = 3, UserID = 2 }
        };

        _mockOrderRepository.Setup(o => o.GetOrders(userId)).Returns(orders);

        // Act
        var result = _orderManager.GetOrders(userId);

        // Assert
        Assert.Equal(orders, result);
        _mockOrderRepository.Verify(o => o.GetOrders(userId), Times.Once);
    }
    [Fact]
    public void Add_AddsOrderToRepository()
    {
        // Arrange
        var order = new Order { ID = 1 };

        _mockOrderRepository.Setup(o => o.Add(order)).Returns(order.ID);

        // Act
        var result = _orderManager.Add(order);

        // Assert
        Assert.Equal(order.ID, result);
        _mockOrderRepository.Verify(o => o.Add(order), Times.Once);
    }


    [Fact]
    public void Update_UpdatesOrderInRepository()
    {
        // Arrange
        var order = new Order { ID = 1 };

        _mockOrderRepository.Setup(o => o.Update(order)).Returns(1);

        // Act
        var result = _orderManager.Update(order);

        // Assert
        Assert.Equal(1, result);
        _mockOrderRepository.Verify(o => o.Update(order), Times.Once);
    }

    [Fact]
    public void CheckpaymentByUser_ReturnsExpectedResult()
    {
        // Arrange
        int userId = 1;
        decimal amount = 100;
        var userCards = new List<PaymentCard>
        {
            new PaymentCard { UserID = userId, Amount = 200 },
            new PaymentCard { UserID = userId, Amount = 50 },
            new PaymentCard { UserID = 2, Amount = 100 }
        };

        _mockPaymentCardService.Setup(p => p.GetPaymentCardByUser(userId)).Returns(userCards);

        // Act
        var result = _orderManager.CheckpaymentByUser(userId, amount);

        // Assert
        Assert.True(result.ContainsKey(true));
        Assert.Equal("Ok", result[true]);
        _mockPaymentCardService.Verify(p => p.GetPaymentCardByUser(userId), Times.Once);
        _mockPaymentCardService.Verify(p => p.Update(It.IsAny<PaymentCard>()), Times.Once);
    }

    [Fact]
    public void GetOrderWithProductDetails_ReturnsOrderWithProductDetails()
    {
        // Arrange
        string orderNumber = "123456";
        var order = new Order { OrderNumber = orderNumber, ID = 1 };
        var orderWithDetails = new Order { OrderNumber = orderNumber, ID = 1, OrderItems = new List<OrderItem> { new OrderItem { Product = new Product { ID = 1, Name = "Product 1" }, Quantity = 2, Price = 10 } } };

        _mockOrderRepository.Setup(o => o.GetOrderWithProductDetails(orderNumber)).Returns(orderWithDetails);

        // Act
        var result = _orderManager.GetOrderWithProductDetails(orderNumber);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(orderNumber, result.OrderNumber);
        Assert.Equal(order.ID, result.ID);
        Assert.NotEmpty(result.OrderItems);
        Assert.Equal(1, result.OrderItems[0].Product.ID);
        Assert.Equal("Product 1", result.OrderItems[0].Product.Name);
        _mockOrderRepository.Verify(o => o.GetOrderWithProductDetails(orderNumber), Times.Once);
    }

    // Diğer metodlar için de benzer şekilde testler yazılabilir

}
