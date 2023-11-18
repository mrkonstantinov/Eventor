namespace Meetuping.UnitTests.Domain;

using Eventor.Services.Meetuping.Domain.AggregatesModel.MeetupAggregate;
using Meetuping.Domain.AggregatesModel.MeetupAggregate;

public class MeetupAggregateTest
{
    public MeetupAggregateTest()
    { }

    [Fact]
    public void Create_mate_success()
    {
        //Arrange    
        var userId = Guid.NewGuid();
        var mateName = "FakeMateName";
        var age = 33;
        var gender = 1;
        var pictureUrl = "FakeUrl";
        
        //Act 
        var fakeMate = new Mate(userId, mateName, age, gender, pictureUrl);

        //Assert
        Assert.NotNull(fakeMate);
    }

    [Fact]
    public void Invalid_number_of_units()
    {
        //Arrange    
        var productId = 1;
        var productName = "FakeProductName";
        var unitPrice = 12;
        var discount = 15;
        var pictureUrl = "FakeUrl";
        var units = -1;

        //Act - Assert
        //Assert.Throws<MeetupingDomainException>(() => new OrderItem(productId, productName, unitPrice, discount, pictureUrl, units));
    }

    //[Fact]
    //public void Invalid_total_of_order_item_lower_than_discount_applied()
    //{
    //    //Arrange    
    //    var productId = 1;
    //    var productName = "FakeProductName";
    //    var unitPrice = 12;
    //    var discount = 15;
    //    var pictureUrl = "FakeUrl";
    //    var units = 1;

    //    //Act - Assert
    //    Assert.Throws<OrderingDomainException>(() => new OrderItem(productId, productName, unitPrice, discount, pictureUrl, units));
    //}

    //[Fact]
    //public void Invalid_discount_setting()
    //{
    //    //Arrange    
    //    var productId = 1;
    //    var productName = "FakeProductName";
    //    var unitPrice = 12;
    //    var discount = 15;
    //    var pictureUrl = "FakeUrl";
    //    var units = 5;

    //    //Act 
    //    var fakeOrderItem = new OrderItem(productId, productName, unitPrice, discount, pictureUrl, units);

    //    //Assert
    //    Assert.Throws<OrderingDomainException>(() => fakeOrderItem.SetNewDiscount(-1));
    //}

    //[Fact]
    //public void Invalid_units_setting()
    //{
    //    //Arrange    
    //    var productId = 1;
    //    var productName = "FakeProductName";
    //    var unitPrice = 12;
    //    var discount = 15;
    //    var pictureUrl = "FakeUrl";
    //    var units = 5;

    //    //Act 
    //    var fakeOrderItem = new OrderItem(productId, productName, unitPrice, discount, pictureUrl, units);

    //    //Assert
    //    Assert.Throws<OrderingDomainException>(() => fakeOrderItem.AddUnits(-1));
    //}

    //[Fact]
    //public void when_add_two_times_on_the_same_item_then_the_total_of_order_should_be_the_sum_of_the_two_items()
    //{
    //    var address = new AddressBuilder().Build();
    //    var order = new OrderBuilder(address)
    //        .AddOne(1, "cup", 10.0m, 0, string.Empty)
    //        .AddOne(1, "cup", 10.0m, 0, string.Empty)
    //        .Build();

    //    Assert.Equal(20.0m, order.GetTotal());
    //}

    [Fact]
    public void Add_new_Meetup_raises_new_event()
    {
        ////Arrange
        //var userId = Guid.NewGuid();
        //var ownerName = "FakeOwnerName";
        //var age = 33;
        //var gender = 1;
        //var pictureUrl = "FakeUrl";

        //DateTime meetupDate = DateTime.Now.Date;


        //var street = "fakeStreet";
        //var city = "FakeCity";
        //var region = "fakeRegion";

        //var expectedResult = 1;

        ////Act 
        //var fakeOrder = new Meetup("1", "fakeName", new Address(street, city, state, country, zipcode), cardTypeId, cardNumber, cardSecurityNumber, cardHolderName, cardExpiration);

        ////Assert
        //Assert.Equal(fakeOrder.DomainEvents.Count, expectedResult);
    }

    [Fact]
    public void Add_event_Order_explicitly_raises_new_event()
    {
        ////Arrange   
        //var street = "fakeStreet";
        //var city = "FakeCity";
        //var region = "fakeRegion";
        
        //var expectedResult = 2;

        ////Act 
        //var fakeOrder = new Order("1", "fakeName", new Address(street, city, state, country, zipcode), cardTypeId, cardNumber, cardSecurityNumber, cardHolderName, cardExpiration);
        //fakeOrder.AddDomainEvent(new OrderStartedDomainEvent(fakeOrder, "fakeName", "1", cardTypeId, cardNumber, cardSecurityNumber, cardHolderName, cardExpiration));
        ////Assert
        //Assert.Equal(fakeOrder.DomainEvents.Count, expectedResult);
    }

    [Fact]
    public void Remove_event_Order_explicitly()
    {
        //Arrange    
        //var street = "fakeStreet";
        //var city = "FakeCity";
        //var state = "fakeState";
        //var country = "fakeCountry";
        //var zipcode = "FakeZipCode";
        //var cardTypeId = 5;
        //var cardNumber = "12";
        //var cardSecurityNumber = "123";
        //var cardHolderName = "FakeName";
        //var cardExpiration = DateTime.Now.AddYears(1);
        //var fakeOrder = new Order("1", "fakeName", new Address(street, city, state, country, zipcode), cardTypeId, cardNumber, cardSecurityNumber, cardHolderName, cardExpiration);
        //var @fakeEvent = new OrderStartedDomainEvent(fakeOrder, "1", "fakeName", cardTypeId, cardNumber, cardSecurityNumber, cardHolderName, cardExpiration);
        //var expectedResult = 1;

        ////Act         
        //fakeOrder.AddDomainEvent(@fakeEvent);
        //fakeOrder.RemoveDomainEvent(@fakeEvent);
        ////Assert
        //Assert.Equal(fakeOrder.DomainEvents.Count, expectedResult);
    }
}