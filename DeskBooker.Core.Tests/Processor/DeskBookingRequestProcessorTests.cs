using DeskBooker.Core.Domain;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace DeskBooker.Core.Processor
{
    public class DeskBookingRequestProcessorTests
    {
        private DeskBookingRequestProcessor _processor;
        public DeskBookingRequestProcessorTests()
        {
            this._processor = new DeskBookingRequestProcessor();

        }
        [Fact]
        public void ShouldReturnDeskBookingResultWithRequestValues() 
        {
            #region arrange
            var request = new DeskBookingRequest
            {
                FirstName = "Thomas",
                LastName = "Huber",
                Email = "thomas@thomasclaudiushuber.com",
                Date = new DateTime(2020, 1, 28)
            };

            //var processor = new DeskBookingRequestProcessor();
            #endregion arrange
            #region act

            DeskBookingResult result = _processor.BookDesk(request);

            #endregion act

            #region assert
            Assert.NotNull(result);
            Assert.Equal(request.FirstName, result.FirstName);
            Assert.Equal(request.LastName, result.LastName);
            Assert.Equal(request.Email, result.Email);
            Assert.Equal(request.Date, result.Date);
            #endregion assert
        }
        [Fact]
        public void ShouldThrowExceptionIfRequestIsNull()
        {
            //var processor = new DeskBookingRequestProcessor();

            var exception =Assert.Throws<ArgumentNullException>(() => _processor.BookDesk(null));

            Assert.Equal("request", exception.ParamName);
        }
    }
}
