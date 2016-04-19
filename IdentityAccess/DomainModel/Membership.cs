 
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
 

namespace IdentityAccess.DomainModel
{
    /// <summary>
    /// 安全认证类,只包含认证相关信息,不应该包含用户信息.
    /// 如果支持通过用户信息(如电话号码,邮箱)认证,那该如何设计?
    /// 答:包含在该类中.
    /// </summary>
  
    public class Membership:Common.Domain.IDomainEntity
    {
        protected Membership()
        {
             IsRegisterValidated = false;
             LastLoginTime=  TimeCreated = DateTime.Now;
            
        }
        protected Membership(string username,string encryptedPassword,string email,string phone):this()
        {
            this.UserName = username;
            this.Password = encryptedPassword;
            this.Email = email;
            this.Phone = phone;
        }
        public static  Membership Create(string username, string encryptedPassword, string email, string phone)
        {
            Membership member = new Membership(username, encryptedPassword, email, phone);
            member.Id = Guid.NewGuid();
            return member;
        }

        /// <summary>
        /// 创建一个认证凭据
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public  static Membership Create3rdUser(enum_LoginType type)
        {
            switch (type)
            {
                case enum_LoginType.original:break;
                case enum_LoginType.WeChat:
                    Membership newUserWechat = new DZMembershipWeChat();
                    return newUserWechat;
                case enum_LoginType.SinaWeiBo:
                    Membership newUserSinaWeibo = new DZMembershipSinaWeibo();
                    return newUserSinaWeibo;
                case enum_LoginType.TencentQQ:
                    Membership newUserQQ = new DZMembershipQQ();
                    return newUserQQ;

            }
            return null;
        }
        public virtual int LoginTimes { get;   set; }
        public virtual Guid Id { get; set; }
        public virtual string UserName { get; set; }
        //用||(双竖线)替换邮箱用户中的@符号
       
        public virtual string Password { get;protected set; }
        //todo: openfire用户验证只能用plain 无法使用md5.

        public virtual string PlainPassword { get; set; }
        public virtual DateTime TimeCreated { get; set; }
        public virtual DateTime LastLoginTime { get; set; }
        public virtual string Email{ get; set; }
        public virtual string  Phone{ get; set; }
      
        /// <summary>
        /// 注册验证码(邮箱验证链接,手机验证码)
        /// </summary>
        public virtual string RegisterValidateCode { get; set; }
        /// <summary>
        /// 是否通过了验证.
        /// </summary>
        public virtual bool IsRegisterValidated { get; set; }
        /// <summary>
        /// 找回密码时的验证码
        /// </summary>
        public virtual Guid RecoveryCode { get; set; }
        public virtual void CopyTo(Membership newMember)
        {
            newMember.Id = Id;
            newMember.UserName = UserName;
            newMember.Password = Password;
            newMember.TimeCreated = TimeCreated;
            newMember.LastLoginTime = LastLoginTime;
            newMember.Email = Email;
            newMember.Phone = Phone;
            
        }
        /// <summary>
        /// 头像图片相对路径.
        /// </summary>
        public virtual string AvatarUrl { get; set; }
       

        /// <summary>
        /// 用户类型
        /// </summary>
        public virtual string UserType { get; set; }

        /// <summary>
        /// 修改密码
        /// </summary>
        /// <param name="oldPassword"></param>
        /// <param name="newPassword"></param>
        /// <returns></returns>
        public virtual bool ChangePassword(string oldPassword, string newPassword,DomainService.IEncryptService encryptService)
        {
            string encryptedOldPsw = encryptService.Encrypt(oldPassword);
            string encryptedNewPsw = encryptService.Encrypt(newPassword);
            if (this.Password != encryptedOldPsw) return false;
            else {
                this.Password = encryptedNewPsw;
                this.PlainPassword = newPassword;
                return true;
            }

        }
  
    }
    
 
}
