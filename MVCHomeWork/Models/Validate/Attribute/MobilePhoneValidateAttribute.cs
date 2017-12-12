using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;

namespace MVCHomeWork.Models.Validate.Attribute
{
    /// <summary>
    /// 手機號碼驗證屬性
    /// </summary>
    public class MobilePhoneValidateAttribute : DataTypeAttribute
    {
        public MobilePhoneValidateAttribute() : base(DataType.Text)
        {
        }
        public override bool IsValid(object value)
        {
            if (value == null)
                return false;

            string phoneNumber = value as string;

            var regRule = @"09\d{2}-\d{6}"; //必須符合 0912-123456
            return Regex.IsMatch(phoneNumber, regRule);
        }
    }
}