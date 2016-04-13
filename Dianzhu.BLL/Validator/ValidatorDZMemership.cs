﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FluentValidation;
using Dianzhu.Model;
using FluentValidation.Validators;
using System.Text.RegularExpressions;
namespace Dianzhu.BLL.Validator
{
    public class ValidatorDZMembership : AbstractValidator<DZMembership>
    {
        public ValidatorDZMembership()
        {
            RuleFor(x => x.Email).EmailAddress().WithMessage("邮箱格式有误");
            RuleFor(x => x.Phone).SetValidator(new PhoneValidator());
            RuleFor(x => x.Password).Length(6, 99).WithMessage("密码长度至少6位");
            
        }
    }
    
    public class PhoneValidator : PropertyValidator
    {
        public PhoneValidator()
            : base("电话格式有误")
        {
        }
        protected override bool IsValid(PropertyValidatorContext context)
        {
            object value = context.PropertyValue;
            if (value == null) return true;
            string pattern = @"^((\d{11})|^((\d{7,8})|(\d{4}|\d{3})-(\d{7,8})|(\d{4}|\d{3})-(\d{7,8})-(\d{4}|\d{3}|\d{2}|\d{1})|(\d{7,8})-(\d{4}|\d{3}|\d{2}|\d{1})))$";
            bool isMatch = Regex.IsMatch(value.ToString(),pattern);
            return isMatch;

        }
    }
}
