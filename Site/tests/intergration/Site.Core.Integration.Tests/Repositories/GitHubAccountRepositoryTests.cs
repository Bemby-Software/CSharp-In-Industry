using System.Data;
using System.Threading.Tasks;
using NUnit.Framework;
using Nytte.Testing;
using Site.Core.DAL.Repositorys;
using Site.Core.Entities;
using Site.Testing.Common.Helpers;

namespace Site.Core.Integration.Tests.Repositories
{
    public class GitHubAccountRepositoryTests : ServiceUnderTest<IGitHubAccountRepository, GitHubAccountRepository>
    {
        public override void Setup()
        {
            Mocker.Use<IDbConnection>(DbHelper.TestConnection);
        }

        [Test]
        public async Task CreateAsync_Account_Creates()
        {
            //Arrange
            var sut = CreateSut();

            var account = new GitHubAccount()
            {
                Repository = "create-repository-owner/create-repo"
            };

            //Act
            var id = await sut.CreateAsync(account);

            //Assert
            var got = await sut.GetAsync(id);
            Assert.AreEqual(account.Repository, got.Repository);
        }
        
        [Test]
        public async Task GetAsync_Account_Gets()
        {
            //Arrange
            var sut = CreateSut();

            var account = new GitHubAccount()
            {
                Repository = "get-repository-owner/get-repo"
            };
            
            var id = await sut.CreateAsync(account);

            //Act
            var got = await sut.GetAsync(id);

            //Assert
            Assert.AreEqual(account.Repository, got.Repository);
        }

        [Test]
        public async Task Update_Account_Updates()
        {
            //Arrange
            var sut = CreateSut();

            var account = new GitHubAccount()
            {
                Repository = "update-repository-owner/update-repo"
            };
            
            var id = await sut.CreateAsync(account);

            account.IssuesCopied = 2;
            account.IsIssueCopyComplete = true;
            account.Id = id;

            //Act
            await sut.UpdateAsync(account);

            //Assert
            var got = await sut.GetAsync(id);
            Assert.AreEqual(account.Repository, got.Repository);
            Assert.AreEqual(account.IssuesCopied, got.IssuesCopied);
            Assert.AreEqual(account.IsIssueCopyComplete, got.IsIssueCopyComplete);
        }
    }
}