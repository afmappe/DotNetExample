// <copyright company="Aranda Software">
// © Todos los derechos reservados
// </copyright>

using Example.Infraestructure.Data.Entities;
using Example.Tasks.Repositories;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Example.Tasks.Command
{
    /// <summary>
    ///
    /// </summary>
    public static class CreateTaskCommand
    {
        /// <summary>
        /// <see cref="IRequestHandler{TRequest, TResponse}"/>
        /// </summary>
        public class Handler : IRequestHandler<Request, int>
        {
            private readonly ITaskRepository TaskRepository;

            public Handler(ITaskRepository taskRepository)
            {
                TaskRepository = taskRepository;
            }

            public async Task<int> Handle(Request request, CancellationToken cancellationToken)
            {
                int result = 0;

                try
                {
                    TaskInfo task = new TaskInfo
                    {
                        CreationDate = DateTime.Now,
                        Description = request.Description,
                        Name = request.Name
                    };

                    await TaskRepository.Create(task);

                    result = task.Id;
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
        public class Request : IRequest<int>
        {
            /// <summary>
            ///
            /// </summary>
            public string Description { get; set; }

            /// <summary>
            ///
            /// </summary>
            public string Name { get; set; }
        }
    }
}