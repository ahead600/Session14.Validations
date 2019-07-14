﻿using System;
using System.Linq;
using System.Text.RegularExpressions;

namespace Session14.Validations.Infra
{
    public static class Helpers
    {
        public static string ValidateNationalId(this String nationalId)
        {

            if (String.IsNullOrEmpty(nationalId))
                return "لطفا کد ملی را صحیح وارد نمایید";

            if (nationalId.Length != 10)
                return "طول کد ملی باید ده کاراکتر باشد";

            var regex = new Regex(@"\d{10}");
            if (!regex.IsMatch(nationalId))
                return "کد ملی تشکیل شده از ده رقم عددی می‌باشد؛ لطفا کد ملی را صحیح وارد نمایید";

            //در صورتی که رقم‌های کد ملی وارد شده یکسان باشد
            var allDigitEqual = new[] { "0000000000", "1111111111", "2222222222", "3333333333", "4444444444", "5555555555", "6666666666", "7777777777", "8888888888", "9999999999" };
            if (allDigitEqual.Contains(nationalId)) return "کد ملی قابل قبول نمی باشد";


            //عملیات شرح داده شده در بالا
            var chArray = nationalId.ToCharArray();
            var num0 = Convert.ToInt32(chArray[0].ToString()) * 10;
            var num2 = Convert.ToInt32(chArray[1].ToString()) * 9;
            var num3 = Convert.ToInt32(chArray[2].ToString()) * 8;
            var num4 = Convert.ToInt32(chArray[3].ToString()) * 7;
            var num5 = Convert.ToInt32(chArray[4].ToString()) * 6;
            var num6 = Convert.ToInt32(chArray[5].ToString()) * 5;
            var num7 = Convert.ToInt32(chArray[6].ToString()) * 4;
            var num8 = Convert.ToInt32(chArray[7].ToString()) * 3;
            var num9 = Convert.ToInt32(chArray[8].ToString()) * 2;
            var a = Convert.ToInt32(chArray[9].ToString());

            var b = (((((((num0 + num2) + num3) + num4) + num5) + num6) + num7) + num8) + num9;
            var c = b % 11;

            //return (((c < 2) && (a == c)) || ((c >= 2) && ((11 - c) == a)));
            if (!(((c < 2) && (a == c)) || ((c >= 2) && ((11 - c) == a)))) return "کد ملی قابل قبول نمی باشد";
            return null;
        }
    }

}
