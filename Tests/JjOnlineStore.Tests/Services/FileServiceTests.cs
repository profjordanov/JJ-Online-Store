using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using AutoFixture;
using JjOnlineStore.Common.ViewModels;
using JjOnlineStore.Data.EF;
using JjOnlineStore.Data.Entities;
using JjOnlineStore.Services.Business;
using JjOnlineStore.Services.Core;
using JjOnlineStore.Tests.Attributes;
using Microsoft.AspNetCore.Http;
using Moq;
using Optional;
using Shouldly;
using Xunit;

namespace JjOnlineStore.Tests.Services
{
    public class FileServiceTests : IDisposable
    {
        private readonly FileService _fileService;

        private readonly JjOnlineStoreDbContext _dbContext;
        private readonly Mock<IImageStorageService> _imageStorageServiceMock;

        public FileServiceTests()
        {
            _dbContext = DbContextProvider.GetInMemoryDbContext();
            _imageStorageServiceMock = new Mock<IImageStorageService>();
            _fileService = new FileService(
                _dbContext,
                _imageStorageServiceMock.Object);
        }

        public void Dispose()
        {
            _dbContext.Database.EnsureDeleted();
        }

        [Theory]
        [CustomAutoData]
        public async Task StoreImagesAsync_Should_Work_Correctly(Fixture fixture)
        {
            //Arrange
            var formFileCollection = new List<IFormFile>();
            for (var i = 0; i < 100; i++)
            {
                var fileMock = new Mock<IFormFile>();
                var content = fixture.Build<string>();
                var fileName = $"test_img_{i}.png";
                var ms = new MemoryStream();
                var writer = new StreamWriter(ms);
                writer.Write(content);
                writer.Flush();
                ms.Position = 0;
                fileMock.Setup(_ => _.OpenReadStream()).Returns(ms);
                fileMock.Setup(_ => _.FileName).Returns(fileName);
                fileMock.Setup(_ => _.Length).Returns(ms.Length);

                _imageStorageServiceMock
                    .Setup(service => service.StoreImageAsync(fileName, ms.ToArray()))
                    .Returns(AsyncEmptyOptionValueFunction);

                formFileCollection.Add(fileMock.Object);
            }

            // Act
            var resultArray = await _fileService.StoreImagesAsync(formFileCollection);

            // Assert
            resultArray
                .Count()
                .ShouldBe(0);
        }

        private async Task<Option<string, Error>> AsyncEmptyOptionValueFunction() =>
            new Option<string, Error>();
    }
}