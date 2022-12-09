using Commands;
using Commands.Query;
using Domains.DBModels;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScolptioCRMWebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ClientsController : BaseController
    {
        private readonly IMediator _mediator;
        private static Random random = new Random();

        public ClientsController(IMediator _mediator)
        {
            this._mediator = _mediator;
        }

        [HttpPost("[action]")]
        [Authorize]
        public async Task<ActionResult> GetAll([FromBody] GetAllClientsQuery GetAllClientsQuery)
        {
            GetAllClientsQuery.OrgId = SecurityContext.OrgId;
            var result = await _mediator.Send(GetAllClientsQuery);
            return Ok(result);
        }

        [HttpGet("[action]")]
        [Authorize]
        public async Task<ActionResult> GetTotalCount()
        {
            var getCountQuery = new GetCountQuery()
            {
                OrganizationId = SecurityContext.OrgId,
                EntityName = typeof(Clients)
            };
            var result = await _mediator.Send(getCountQuery);
            return Ok(result);
        }

        [HttpGet("[action]")]
        [Authorize]
        public async Task<ActionResult> GetById(string incomeId)
        {
            var getIncomeQuery = new GetIncomeQuery
            {
                OrgId = SecurityContext.OrgId,
                IncomeId = incomeId
            };

            var result = await _mediator.Send(getIncomeQuery);
            return Ok(result);
        }

        [HttpPost("[action]")]
        [Authorize]
        public async Task<ActionResult> Add([FromBody] CreateClientsCommand CreateClientsCommand)
        {
            //CreateClientsCommand.OrgId = SecurityContext.OrgId;
            //CreateClientsCommand.CreatedDate = DateTime.Now;
            SendInvitationCommand command = new SendInvitationCommand();
            command.InvitationEmail = CreateClientsCommand.Email;
            command.Name = CreateClientsCommand.FirstName;
            command.Phone = CreateClientsCommand.PhoneNumber;
            command.TeamId = CreateClientsCommand.TeamId;
            command.Address = CreateClientsCommand.CompanyAdress;
            command.Website = CreateClientsCommand.Website;
            command.CompanyAdress = CreateClientsCommand.CompanyAdress;
            command.Logo = CreateClientsCommand.Logo;
            command.InvitationEmail = CreateClientsCommand.Email;
            command.PhoneNumber = CreateClientsCommand.PhoneNumber;
            command.CompanyName = CreateClientsCommand.CompanyName;
            command.TeamId = CreateClientsCommand.TeamId;
            command.IsInvited = CreateClientsCommand.IsInvited;
            command.UserType = CreateClientsCommand.UserType;
            command.InvitedBy = SecurityContext.UserId;

            command.UserId = SecurityContext.UserId;
            command.UserDisplayName = SecurityContext.DisplayName;
            command.OrgId = SecurityContext.OrgId;
            command.OrgName = SecurityContext.OrgName;
            
            await _mediator.Send(command);
            //call user creation command 
            //if (CreateClientsCommand.InviteOption == Domains.Enum.Enums.InviteOption.Regsiterd)
            //{
            //    var CreateClientUserCommand = new CreateClientUserCommand
            //    {
            //        FirstName = CreateClientsCommand.FirstName,
            //        LastName = CreateClientsCommand.LastName,
            //        Address = CreateClientsCommand.CompanyAdress,
            //        Email = CreateClientsCommand.Email,
            //        OrgId = SecurityContext.OrgId,
            //        Password = CreateRandomPassword(10),
            //        ClientId = clientId,
            //        TeamId = CreateClientsCommand.TeamId,
            //        CompanyName = CreateClientsCommand.CompanyName

            //    };
            //    await _mediator.Send(CreateClientUserCommand);
            //}
            //else
            //{
            //    SendInvitationCommand command = new SendInvitationCommand();
            //    command.InvitationEmail = CreateClientsCommand.Email;
            //    command.Name = CreateClientsCommand.FirstName;
            //    command.Phone = CreateClientsCommand.PhoneNumber;
            //    command.TeamId = CreateClientsCommand.TeamId;
            //    command.Address = CreateClientsCommand.CompanyAdress;

            //    command.UserId = SecurityContext.UserId;
            //    command.UserDisplayName = SecurityContext.DisplayName;
            //    command.OrgId = SecurityContext.OrgId;
            //    command.OrgName = SecurityContext.OrgName;
            //    await _mediator.Send(command);
            //}

            return Ok();
        }

        [HttpPost("[action]")]
        [Authorize]
        public async Task<ActionResult> Update([FromBody] UpdateClientsCommand UpdateClientsCommand)
        {
            var oldClient = await _mediator.Send(UpdateClientsCommand);
            //if (oldClient.IsInvited == false && UpdateClientsCommand.IsInvited == true)
            //{
            //    SendInvitationCommand command = new SendInvitationCommand();
            //    command.InvitationEmail = UpdateClientsCommand.Email;
            //    command.Name = UpdateClientsCommand.FirstName;
            //    command.Phone = UpdateClientsCommand.PhoneNumber;
            //    command.Address = UpdateClientsCommand.CompanyAdress;

            //    command.UserId = SecurityContext.UserId;
            //    command.UserDisplayName = SecurityContext.DisplayName;
            //    command.OrgId = SecurityContext.OrgId;
            //    command.OrgName = SecurityContext.OrgName;
            //    await _mediator.Send(command);
            //}
            return Ok();
        }

        [HttpPost("[action]")]
        [Authorize]
        public async Task<ActionResult> Delete([FromBody] DeleteClientsCommand DeleteClientsCommand)
        {
            var isDeleted = await _mediator.Send(DeleteClientsCommand);
            return Ok(isDeleted);
        }

        private static string CreateRandomPassword(int length = 15)
        {
            // Create a string of characters, numbers, special characters that allowed in the password  
            string validChars = "ABCDEFGHJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789!@#$%^&*?_-";
            Random random = new Random();

            // Select one random character at a time from the string  
            // and create an array of chars  
            char[] chars = new char[length];
            for (int i = 0; i < length; i++)
            {
                chars[i] = validChars[random.Next(0, validChars.Length)];
            }
            var result = new string(chars);
            return char.ToLower(result[0]) + result.Substring(1) + "#" + "7";
        }

        private static string GetVoucherNumber(int length)
        {
            var chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789%#@#%#$#!&*";
            var result = new string(
                Enumerable.Repeat(chars, length)
                          .Select(s => s[random.Next(s.Length)])
                          .ToArray());

            return char.ToLower(result[0]) + result.Substring(1) + "#" + "7";
        }

        private string RandomString(int size, bool lowerCase)
        {
            StringBuilder builder = new StringBuilder();
            Random random = new Random();
            char ch;
            for (int i = 0; i < size; i++)
            {
                ch = Convert.ToChar(Convert.ToInt32(Math.Floor(26 * random.NextDouble() + 65)));
                builder.Append(ch);
            }
            if (lowerCase)
                return builder.ToString().ToLower();
            return builder.ToString();
        }
    }
}
