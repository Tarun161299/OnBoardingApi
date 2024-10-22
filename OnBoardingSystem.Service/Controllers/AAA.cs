//-----------------------------------------------------------------------
// <copyright file="EmailController.cs" company="NIC">
// Copyright (c) NIC. All rights reserved.
// </copyright>
//-------------------------------------------------------------------

namespace OnBoardingSystem.Service.Controllers
{
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using OnBoardingSystem.Data.Abstractions.Behaviors;
    using OnBoardingSystem.Data.Abstractions.Models;
    using OnBoardingSystem.Data.EF.Models;

    /// <summary>
    /// EmailController .
    /// </summary>
    //[ApiController]
    //[Route("api/[controller]")]
    public class AAAController : Controller
    {
        private readonly IMailServiceDirector mailService;

        public AAAController(IMailServiceDirector mailService)
        {
            this.mailService = mailService;
        }

        [HttpPost("V28112023")]
        public async Task<IActionResult> Send()
        {
            try
            {
                return Ok("Version Send");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        //[Route("api/[controller]/DeleteMe")]
        public async Task<IActionResult> DeleteAsync()
        {
            try
            {
                return Ok("Version Send");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
