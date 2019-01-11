// <copyright company="Aranda Software">
// © Todos los derechos reservados
// </copyright>

using Example.Infraestructure.Data.Entities;
using Example.Tasks.Repositories;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Example.Tasks.Queries
{
    /// <summary>
    ///
    /// </summary>
    public static class GetTaskQuery
    {
        /// <summary>
        /// <see cref="IRequestHandler{TRequest, TResponse}"/>
        /// </summary>
        public class Handler : IRequestHandler<Request, TaskInfo>
        {
            /// <summary>
            /// <see cref="ITaskQueryRepository"/>
            /// </summary>
            private readonly ITaskQueryRepository taskQueryRepository;

            /// <summary>
            /// Constructor por defecto
            /// </summary>
            public Handler(ITaskQueryRepository taskQueryRepository)
            {
                this.taskQueryRepository = taskQueryRepository;
            }

            /// <summary>
            /// <see cref="IRequestHandler{TRequest, TResponse}.Handle(TRequest, CancellationToken)"/>
            /// </summary>
            public async Task<TaskInfo> Handle(Request request, CancellationToken cancellationToken)
            {
                TaskInfo result = null;
                try
                {
                    result = await taskQueryRepository.Get(request.Id);
                }
                catch (Exception ex)
                {
                    throw;
                }

                return result;
            }
        }

        /// <summary>
        ///
        /// </summary>
        public class Request : IRequest<TaskInfo>
        {
            public int Id { get; set; }
        }
    }
}