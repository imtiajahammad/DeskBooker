﻿using DeskBooker.Core.DataInterface;
using DeskBooker.Core.Domain;
using System;
using System.Linq;

namespace DeskBooker.Core.Processor
{
    public class DeskBookingRequestProcessor
    {
        private readonly IDeskBookingRepository _deskBookingRepository;
        private readonly IDeskRepository _deskRepository;

        public DeskBookingRequestProcessor(
            IDeskBookingRepository deskBookingRepository
            , IDeskRepository deskRepository)
        {
            this._deskBookingRepository = deskBookingRepository;
            this._deskRepository = deskRepository;
        }

        public DeskBookingResult BookDesk(DeskBookingRequest request)
        {
            if (request == null)
            {
                throw new ArgumentNullException(nameof(request));
            }

            var result = Create<DeskBookingResult>(request);

            var availableDesks = _deskRepository.GetAvailableDesks(request.Date);
            if (availableDesks.FirstOrDefault() is Desk availableDesk)
            {
                var deskBooking = Create<DeskBooking>(request);
                deskBooking.DeskId = availableDesk.Id;
                _deskBookingRepository.Save(deskBooking);

                result.Code = DeskBookingResultCode.Success;

                result.DeskBookingId = deskBooking.Id;

            }
            else
            {
                result.Code = DeskBookingResultCode.NoDeskAvailable;
            }
            return result;//Create<DeskBookingResult>(request);
        }

        private static T Create<T>(DeskBookingRequest request) where T : DeskBookingBase, new()
        {
            return new T
            {
                FirstName = request.FirstName,
                LastName = request.LastName,
                Email = request.Email,
                Date = request.Date
            };
        }
    }
}