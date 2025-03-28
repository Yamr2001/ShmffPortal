﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShmffPortal.Models
{
   public struct CitizenData
    {
        public int YearOfBirth { get; }
        public byte MonthOfBirth { get; }
        public byte DayOfBirth { get; }
        public string Governorate { get; }
        public string Gender { get; }


        public CitizenData(int yearOfBirth, byte monthOfBirth, byte dayOfBirth, string governorate, string gender)
        {
            YearOfBirth = yearOfBirth;
            MonthOfBirth = monthOfBirth;
            DayOfBirth = dayOfBirth;
            Governorate = governorate;
            Gender = gender;
        }
    }
}