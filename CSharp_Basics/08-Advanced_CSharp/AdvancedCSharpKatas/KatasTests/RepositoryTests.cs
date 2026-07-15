using GenericsImplementations;
using System;
using System.Collections.Generic;
using System.Text;

namespace KatasTests
{
    internal class RepositoryTests
    {
        Repository<User> _repo = new Repository<User>();

        [SetUp]
        public void Setup()
        {
            _repo.Add(new User(1, "Ada"));
            _repo.Add(new User(2, "Linus"));
        }
        [Test]
        public void TestRepoGetById_Id_User()
        {
            Assert.That(_repo.GetById(1)!.Name, Is.EqualTo("Ada"));
        }
        [Test]
        public void TestRepoGetAll_Void_Count()
        {
            Assert.That(_repo.GetAll().Count(), Is.EqualTo(2));
        }
        [Test]
        public void TestRepoRemove_UserId_Count()
        {
            _repo.Remove(1);

            Assert.That(_repo.GetAll().Count(), Is.EqualTo(1));
        }
        [Test]
        public void TestRepoGetById_UserId_UnsuccessfulNull()
        {
            _repo.Remove(1);

            Assert.Throws<InvalidOperationException>(()=>_repo.GetById(1));
        }
    }
}
