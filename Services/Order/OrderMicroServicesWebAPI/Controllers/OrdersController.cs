﻿using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OnlineStudyShared.Controller;
using OnlineStudyShared.Services;
using OrderApplication.Commands;
using OrderApplication.Queries;

namespace OrderMicroServicesWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : CustomBaseController
    {
        private readonly IMediator _mediator;
        private readonly ISharedIdentity _sharedIdentity;
        
        public OrdersController(IMediator mediator, ISharedIdentity sharedIdentity)
        {
            _mediator = mediator;
            _sharedIdentity = sharedIdentity;
        }
        
        [HttpPost]
        public async Task<IActionResult> CreateOrder(CreateOrderCommand createOrderCommand)
        {
            createOrderCommand.BuyerId = _sharedIdentity.GetUserId;
            var response = await _mediator.Send(createOrderCommand);
            return CreateActionResultInstance(response);
        }
        
        [HttpGet]
        public async Task<IActionResult> GetOrders()
        {
            var response = await _mediator.Send(new GetOrdersByUserIdQuery{UserId = _sharedIdentity.GetUserId});
            return CreateActionResultInstance(response);
        }
        
    }
}