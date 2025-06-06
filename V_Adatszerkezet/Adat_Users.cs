﻿using System;

namespace Villamos.Adatszerkezet
{
    public class Adat_Users
    {
        public int UserId { get; private set; }
        public string UserName { get; private set; }
        public string WinUserName { get; private set; }
        public string Dolgozószám { get; private set; }
        public string Password { get; private set; }
        public DateTime Dátum { get; private set; }
        public bool Frissít { get; private set; }
        public bool Törölt { get; private set; }
        public string Szervezetek { get; private set; }

        public Adat_Users(
            int userId,
            string userName,
            string winUserName,
            string dolgozószám,
            string password,
            DateTime dátum,
            bool frissít,
            bool törölt,
            string szervezetek)
        {
            UserId = userId;
            UserName = userName;
            WinUserName = winUserName;
            Dolgozószám = dolgozószám;
            Password = password;
            Dátum = dátum;
            Frissít = frissít;
            Törölt = törölt;
            Szervezetek = szervezetek;
        }
    }
}
