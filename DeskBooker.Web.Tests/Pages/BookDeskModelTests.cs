using DeskBooker.Core.Domain;
using DeskBooker.Core.Processor;
using DeskBooker.Web.Pages;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace DeskBooker.Web.Tests.Pages
{
    public class BookDeskModelTests
    {
        [Fact]
        public void ShouldCallBookDeskMethodOfProcessor()
        {
            // Arrange
            var processorMock = new Mock<IDeskBookingRequestProcessor>();
            var bookDeskModel = new BookDeskModel(processorMock.Object)
            {
                DeskBookingRequest = new DeskBookingRequest()
            };
            // Act
            bookDeskModel.OnPost();

            // Assert
            processorMock.Verify(x => x.BookDesk(bookDeskModel.DeskBookingRequest), Times.Once);



        }
    }
}
