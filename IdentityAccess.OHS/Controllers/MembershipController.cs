using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using IdentityAccess.ApplicationService.Membership;
using IdentityAccess.OHS.Models;
namespace IdentityAccess.OHS.Controllers
{
    public class MembershipController : ApiController
    {
        IMembershipService membershipService;
        public MembershipController(IMembershipService membershipService)
        {
            this.membershipService = membershipService;
        }
        public MembershipController():this(new MembershipService(new IdentityAccess.Infrastructure.MembershipRepository())) { }
       

        [HttpGet]
        public  Response<MembershipDto> Get(string id)
        {
            
            Response<MembershipDto> response = new Response<MembershipDto>();
            try
            {
                response.Object = membershipService.FindById(id);
            }
            catch (Exception ex)
            {
                response.Errored = true;
                response.ErrorMessage = ex.Message;
            }
            return response;
        }


        // POST: Membership/Create
        [HttpPut]
        public Response<MembershipDto> Create([FromBody] MembershipDto member)
        {
            Response<MembershipDto> response = new Response<MembershipDto>();
            try
            {
                // TODO: Add insert logic here
               MembershipDto dto= membershipService.Register(member);
                response.Object = dto;
            }
            catch(Exception ex)
            {
                response.Errored = true;
                response.ErrorMessage = ex.Message;
            }
            return response;
        }

        
    }
}
