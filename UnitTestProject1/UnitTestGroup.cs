using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using TestingSystem.Data.Infrastructure;
using TestingSystem.Data.Repositories;
using TestingSystem.DataTranferObject;
using TestingSystem.Models;
using TestingSystem.Sevice;

namespace UnitTestProject1
{
    [TestClass]
    public class UnitTestGroup
    {
        private Mock<IGroupRepository> _mockRepository;
        private Mock<IUnitOfWork> _mockUnitOfWork;
        private IGroupService groupService;
        private List<Group> _listGroup;
        private List<Group> _listGroupExcepted;
        private List<Group> _listGroupFilterExcepted;
        private List<User> _listUserExcepted;

        [TestInitialize]
        public void Initialize()
        {
            _mockRepository = new Mock<IGroupRepository>();
            _mockUnitOfWork = new Mock<IUnitOfWork>();
            groupService = new GroupService(_mockRepository.Object, _mockUnitOfWork.Object);
            _listUserExcepted = new List<User>()
            {
                new User()
                {
                    UserId = 1,
                    RoleId = 1,
                    UserName = "admin",
                    Password = "21232f297a57a5a743894a0e4a801fc3",
                    CreatedDate = DateTime.Parse("1997-01-01 00:00:00.000"),
                    UpdatedDate = DateTime.Parse("1996-01-01 00:00:00.000"),
                    Status = 1,
                    Name = "Admin",
                    Phone = "",
                    Email = "admin@admin.com",
                    Address = "123",
                    Avatar = "123",
                    Note = "123"
                }
            };
            _listGroupFilterExcepted = new List<Group>()
            {
                new Group()
                {
                    GroupId = 52,
                    GroupName = "Group 2",
                    CreatedDate = DateTime.Parse("2019-03-04 08:52:10.000"),
                    UpdatedDate = DateTime.Now,
                    Description = "Description Group 2"
                },
            };
            _listGroupExcepted = new List<Group>()
            {
                new Group()
                {
                    GroupId = 52,
                    GroupName = "Group 2",
                    CreatedDate = DateTime.Parse("2019-03-04 08:52:10.000"),
                    UpdatedDate = DateTime.Now,
                    Description = "Description Group 2"
                },
            };
            _listGroup = new List<Group>()
            {
                new Group()
                {
                    GroupId = 52,
                    GroupName = "Group 2",
                    CreatedDate = DateTime.Parse("2019-03-04 08:52:10.000"),
                    UpdatedDate = DateTime.Now,
                    Description = "Description Group 2"
                },
                new Group()
                {
                    GroupId = 58,
                    GroupName = "Group 1",
                    CreatedDate = DateTime.Parse("2019-03-05 13:15:45.000"),
                    UpdatedDate = DateTime.Parse("2019-03-06 08:34:49.433"),
                    Description = "Description Group 1"
                },
                new Group()
                {
                    GroupId = 59,
                    GroupName = "Group 3",
                    CreatedDate = DateTime.Parse("2019-03-05 13:15:52.563"),
                    UpdatedDate = DateTime.Parse("2019-03-05 13:15:52.563"),
                    Description = "Description Group 3"
                },
                new Group()
                {
                    GroupId = 69,
                    GroupName = "Group 4",
                    CreatedDate = DateTime.Parse("2019-03-06 09:27:41.340"),
                    UpdatedDate = DateTime.Parse("2019-03-06 09:27:41.340"),
                    Description = "Description Group 4"
                },
        };
        }
        [TestMethod]
        public void Service_Group_GetAll()
        {
            _mockRepository.Setup(m => m.GetAllGroupC()).Returns(_listGroup);
            var result = groupService.GetAllGroupC();
            Assert.AreEqual(4, result.Count());
        }

        [TestMethod]
        public void Service_Group_Create_Test()
        {
            Group group = new Group();
            group.GroupName = "Group 5";
            group.CreatedDate = DateTime.Now;
            group.UpdatedDate = DateTime.Now;
            group.Description = "group 5";


            _mockRepository.Setup(m => m.CreateGroup(group)).Returns(1);
            var result = groupService.CreateGroup(group);
            Assert.IsNotNull(result);
            Assert.AreEqual(1, result);
        }

        [TestMethod]
        public void Service_Group_Delete_Test()
        {
            _mockRepository.Setup(m => m.DeleteGroup(78)).Returns(1);
            var result = groupService.DeleteGroup(78);
            Assert.AreEqual(1, result);
        }

        [TestMethod]
        //
        public void Service_Group_Update_Test()
        {
            Group group = new Group();
            group.GroupId = 78;

            group.GroupName = "Group 5";
            group.CreatedDate = DateTime.Now;
            group.UpdatedDate = DateTime.Now;
            group.Description = "group 5";

            _mockRepository.Setup(m => m.EditGroup(group)).Returns(1);
            var result = groupService.EditGroup(group);
            Assert.AreEqual(1, result);
        }
        [TestMethod]
        public void Service_Group_GetAllGroupByName_Test()
        {
            var stringName = "Group 2";
            _mockRepository.Setup(m => m.GetAllGroupByName(stringName)).Returns(_listGroupExcepted);
            var result = groupService.GetAllGroupByName(stringName);
            Assert.AreEqual(_listGroupExcepted, result);
        }

        [TestMethod]
        public void Service_Group_GetAllGroupFilter_Test()
        {
            GroupFilter groupFilter = new GroupFilter();
            groupFilter.StartCreatedDate = DateTime.Parse("2019-03-04 08:52:10.000");
            groupFilter.EndCreatedDate = DateTime.Parse("2019-03-05 08:52:10.000");
            _mockRepository.Setup(m => m.GetAllGroup(groupFilter)).Returns(_listGroupFilterExcepted);
            var result = groupService.GetAllGroup(groupFilter);
            Assert.AreEqual(_listGroupFilterExcepted, result);
        }

        [TestMethod]
        public void Service_Group_GetAllGroupByNameOutId_Test()
        {
            string stringName = "Group";
            int id = 52;
            _mockRepository.Setup(m => m.GetAllGroupByNameOutId(stringName, id)).Returns(_listGroupExcepted);
            var result = groupService.GetAllGroupByNameOutId(stringName, id);
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void Service_Group_GetAllUserInGroup_Test()
        {
            int id = 59;
            _mockRepository.Setup(m => m.GetAllUserInGroup(id)).Returns(_listUserExcepted);
            var result = groupService.GetAllUserInGroup(id);
            Assert.IsNotNull(result);
        }
    }
}
