//public class MateAggregateTest
//{
//    public MateAggregateTest()
//    { }

//    [Fact]
//    public void Create_mate_item_success()
//    {
//        //Arrange    
//        var identity = new Guid().ToString();
//        var name = "fakeUser";
//        var birthdate = DateTime.Now;
//        var gender = 1;

//        //Act 
//        var fakeMateItem = new Mate(identity, name, birthdate, gender);

//        //Assert
//        Assert.NotNull(fakeMateItem);
//    }

//    [Fact]
//    public void Create_buyer_item_fail()
//    {
//        //Arrange    
//        var identity = string.Empty;
//        var name = "fakeUser";
//        var birthdate = DateTime.Now;
//        var gender = 1;

//        //Act - Assert
//        Assert.Throws<ArgumentNullException>(() => new Mate(identity, name, birthdate, gender));
//    }
//}