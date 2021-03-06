using Microsoft.EntityFrameworkCore;
using Entity = StoreDL.Entities;
using Model = StoreModels;
using Xunit;
using StoreDL;
using System.Linq;

namespace StoreTests
{

    /// <summary>
    /// This is going to be my test class for the data access methods in my repo.
    /// </summary>
    public class CustomerRepoTest
    {
        private readonly DbContextOptions<Entity.wssdbContext> options;
        private IMapper mapper;
        //XUnit creates new instances of test classes, so you need to make sure that your db is seeded for each test class

        public CustomerRepoTest()
        {
            options = new DbContextOptionsBuilder<Entity.wssdbContext>()
            .UseSqlite("Filename = Test.db")
            .Options;
            Seed();
            mapper = new StoreMapper();
        }

        //testing read operation
        [Fact]
        public void GetAllCustomersShouldReturnAllCustomers()
        {
            using(var context = new Entity.wssdbContext(options))
            {
                //Arrange the test context
                CustomerRepoDB _repo = new CustomerRepoDB(context, mapper);

                //Act
                var customers = _repo.GetAllCustomers();
                //Assert
                Assert.Equal(3, customers.Count);
            }
        }

        [Fact]
        public void AddCustomerShouldAddCustomer()
        {
            using (var context = new Entity.wssdbContext(options))
            {
                CustomerRepoDB _repo = new CustomerRepoDB(context, mapper);
                //Act with a test context
                _repo.AddNewCustomer
                (
                    new Model.Customer("Test User")
                );
            }
            //use a diff context to check if changes persist to db
            using (var assertContext = new Entity.wssdbContext(options))
            {
                //Assert with a different context
                var result = assertContext.Customers.FirstOrDefault(cust => cust.Id == 4);
                Assert.NotNull(result);
                Assert.Equal("Test User", result.CName);
            }
        }

        private void Seed()
        {
            //Seed our DB with this method
            //example of using block
            using (var context = new Entity.wssdbContext(options))
            {
                //this makes sure that the state of the db gets recreated each time to maintain the modularity of the test
                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();
                context.Customers.AddRange
                (
                    new Entity.Customer {
                        Id = 1,
                        CName = "Auryn"
                    },
                    new Entity.Customer {
                        Id = 2,
                        CName = "Melix"
                    },
                    new Entity.Customer {
                        Id = 3,
                        CName = "Roaringsheep"
                    }
                );
                context.SaveChanges();
            }
        }
    }
}