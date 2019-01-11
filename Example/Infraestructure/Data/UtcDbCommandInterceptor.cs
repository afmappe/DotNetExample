using System;
using System.Data;
using System.Data.Common;
using System.Data.Entity.Infrastructure.Interception;
using System.Linq;

namespace Example.Infraestructure.Data
{
    /// <summary>
    ///
    /// </summary>
    public class UtcDbCommandInterceptor : DbCommandInterceptor
    {
        /// <inheritdoc />
        public override void NonQueryExecuting(DbCommand command, DbCommandInterceptionContext<int> interceptionContext)
        {
            ProcessParams(command.Parameters);

            base.NonQueryExecuting(command, interceptionContext);
        }

        /// <inheritdoc />
        public override void ReaderExecuted(DbCommand command, DbCommandInterceptionContext<DbDataReader> interceptionContext)
        {
            ProcessParams(command.Parameters);

            base.ReaderExecuted(command, interceptionContext);

            if (interceptionContext.Result != null && !(interceptionContext.Result is UtcDbDataReader))
            {
                interceptionContext.Result = new UtcDbDataReader(interceptionContext.Result);
            }
        }

        /// <inheritdoc />
        public override void ReaderExecuting(DbCommand command, DbCommandInterceptionContext<DbDataReader> interceptionContext)
        {
            ProcessParams(command.Parameters);

            base.ReaderExecuting(command, interceptionContext);
        }

        /// <inheritdoc />
        public override void ScalarExecuting(DbCommand command, DbCommandInterceptionContext<object> interceptionContext)
        {
            ProcessParams(command.Parameters);

            base.ScalarExecuting(command, interceptionContext);
        }

        private void ProcessParams(DbParameterCollection parameters)
        {
            DbType[] dateTypes = new DbType[] {
                    DbType.DateTime,
                    DbType.DateTime2,
                    DbType.Date
                };

            foreach (DbParameter param in parameters)
            {
                if (param.Value != null && dateTypes.Any(x => x == param.DbType))
                {
                    DateTime date = (DateTime)param.Value;
                    param.Value = date.ToUniversalTime();
                }
            }
        }
    }
}