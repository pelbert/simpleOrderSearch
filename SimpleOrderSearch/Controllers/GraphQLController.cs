using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GraphQL;
using GraphQL.Types;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SimpleOrderSearch.Model;
using SimpleOrderSearch.Service.Contracts;
using SimpleOrderSearch.Service.GraphQL.Query;
using SimpleOrderSearch.Service.GraphQL.Types;

namespace SimpleOrderSearch.Service.Controllers
{
    /// <summary>
    /// GraphQLController
    /// </summary>
    ////[Route("api/[controller]")]
    [Route("graphql")]
    [ApiController]
    public class GraphQLController : ControllerBase
    {
        private readonly IDataAccessor<OrderInfo> dataAccessor;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="dataAccessor"></param>
        public GraphQLController(IDataAccessor<OrderInfo> dataAccessor) => this.dataAccessor = dataAccessor;

        public async Task<IActionResult> Post([FromBody] GraphQLQuery query)
        {
            var inputs = query.Variables.ToInputs();

            var schema = new Schema
            {
               Query = new OrderGraphQLQuery(this.dataAccessor)
            };
            schema.RegisterType<OrderType>();
            


            var result = await new DocumentExecuter().ExecuteAsync(_ =>
            {
                _.Schema = schema;
                _.Query = query.Query;
                _.OperationName = query.OperationName;
                _.Inputs = inputs;
            });
            
           

            if (result.Errors?.Count > 0)
            {
                return BadRequest(result.Errors.Select(p => p.Message));
            }

            return Ok(result);
        }
    }
}