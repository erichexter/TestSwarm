using System.Linq;
using NUnit.Framework;
using Should;
using nTestSwarm.Application.Commands.CompletedRun;
using nTestSwarm.Application.Commands.UpdateUser;
using nTestSwarm.Application.Domain;
using nTestSwarm.Application.Infrastructure.BusInfrastructure;

namespace nTestSwarmTests.Application.RunCompletion
{
    [TestFixture]
    public class UpdateUserHandlerTest : IntegrationTestBase
    {
    
        [Test]
        public void should_create_a_new_user()
        {
                 var updateUser = new UpdateUser
                {
                  Username = "a",
                  EmailAddress = "f",
                  Name = "fo",
                  Password = "dodo"
                };
            User user=null;
            WithDbContext(context => user= GetInstance<UpdateUserHandler>().Handle(updateUser));

            WithDbContext(context =>
                              {
                                  var foundUser = context.Find<User>(user.Id);

                                  foundUser.ShouldNotBeNull();
                                  var compare = new KellermanSoftware.CompareNetObjects.CompareObjects();

                                  // savedJob is a proxy
                                  compare.MaxDifferences = 1;

                                  compare.Compare(user, foundUser);
                              });
        }

        [Test]
        public void should_update_an_existing_user()
        {
            var existingUser = new User();
            
            Save(existingUser);

            var updateUser = new UpdateUser
            {
                Id = existingUser.Id,
                Username = "a",
                EmailAddress = "f",
                Name = "fo",
                Password = "dodo",
                
            };
            User user = null;
            WithDbContext(context => user = GetInstance<UpdateUserHandler>().Handle(updateUser));

            WithDbContext(context =>
            {
                var foundUser = context.Find<User>(user.Id);
                foundUser.ShouldNotBeNull();      
                Compare(foundUser, user);
            });
        }
    }
}