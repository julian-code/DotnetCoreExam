using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DotnetCoreExam.Contracts;
using MassTransit;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DemoWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IRequestClient<ISubmitOrder> _submitOrderRequestClient;
        private readonly IRequestClient<ICheckOrder> _checkOrder;

        public OrderController(IRequestClient<ISubmitOrder> submitOrderRequestClient, IRequestClient<ICheckOrder> checkOrder)
        {
            _submitOrderRequestClient = submitOrderRequestClient;
            _checkOrder = checkOrder;
        }

        [HttpGet]
        public async Task<IActionResult> Get(Guid id)
        {
            var (status, notFound) = await _checkOrder.GetResponse<IOrderStatus, IOrderNotFound>(new
            {
                OrderId = id
            });

            if (status.IsCompletedSuccessfully)
            {
                var response = await status;
                return Ok(response.Message);
            } else
            {
                var response = await notFound;

                return NotFound(response.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Post(Guid id, string customerNumber)
        {
            var (accepted, rejected) = await _submitOrderRequestClient.GetResponse<IOrderSubmissionAccepted, IOrderSubmissionRejected>(new
            {
                OrderId = id,
                InVar.Timestamp,
                CustomerNumber = customerNumber
            });

            if (accepted.IsCompletedSuccessfully)
            {
                var response = await accepted;

                return Ok(response.Message);
            } else
            {
                var response = await rejected;
                return BadRequest(response.Message);
            }
        }
    }
}