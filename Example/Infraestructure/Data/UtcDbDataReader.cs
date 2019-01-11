using System;
using System.Collections;
using System.Data;
using System.Data.Common;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace Example.Infraestructure.Data
{
    public class UtcDbDataReader : DbDataReader
    {
        private readonly DbDataReader SourceDataReader;

        /// <summary>
        /// Constructor por defecto
        /// </summary>
        public UtcDbDataReader(DbDataReader source)
        {
            SourceDataReader = source;
        }

        /// <inheritdoc />
        public override int Depth => SourceDataReader.Depth;

        /// <inheritdoc />
        public override int FieldCount => SourceDataReader.FieldCount;

        /// <inheritdoc />
        public override bool HasRows => SourceDataReader.HasRows;

        /// <inheritdoc />
        public override bool IsClosed => SourceDataReader.IsClosed;

        /// <inheritdoc />
        public override int RecordsAffected => SourceDataReader.RecordsAffected;

        /// <inheritdoc />
        public override int VisibleFieldCount => SourceDataReader.VisibleFieldCount;

        /// <inheritdoc />
        public override object this[string name] => SourceDataReader[name];

        /// <inheritdoc />
        public override object this[int ordinal] => SourceDataReader[ordinal];

        /// <inheritdoc />
        public override void Close()
        {
            SourceDataReader.Close();
        }

        /// <inheritdoc />
        public override bool GetBoolean(int ordinal)
        {
            return SourceDataReader.GetBoolean(ordinal);
        }

        /// <inheritdoc />
        public override byte GetByte(int ordinal)
        {
            return SourceDataReader.GetByte(ordinal);
        }

        /// <inheritdoc />
        public override long GetBytes(int ordinal, long dataOffset, byte[] buffer, int bufferOffset, int length)
        {
            return SourceDataReader.GetBytes(ordinal, dataOffset, buffer, bufferOffset, length);
        }

        /// <inheritdoc />
        public override char GetChar(int ordinal)
        {
            return SourceDataReader.GetChar(ordinal);
        }

        /// <inheritdoc />
        public override long GetChars(int ordinal, long dataOffset, char[] buffer, int bufferOffset, int length)
        {
            return SourceDataReader.GetChars(ordinal, dataOffset, buffer, bufferOffset, length);
        }

        /// <inheritdoc />
        public new IDataReader GetData(int ordinal)
        {
            return SourceDataReader.GetData(ordinal);
        }

        /// <inheritdoc />
        public override string GetDataTypeName(int ordinal)
        {
            return SourceDataReader.GetDataTypeName(ordinal);
        }

        /// <summary>
        /// Returns date time with Utc kind
        /// </summary>
        public override DateTime GetDateTime(int ordinal)
        {
            return DateTime.SpecifyKind(SourceDataReader.GetDateTime(ordinal), DateTimeKind.Utc).ToLocalTime();
        }

        /// <inheritdoc />
        public override decimal GetDecimal(int ordinal)
        {
            return SourceDataReader.GetDecimal(ordinal);
        }

        /// <inheritdoc />
        public override double GetDouble(int ordinal)
        {
            return SourceDataReader.GetDouble(ordinal);
        }

        /// <inheritdoc />
        public override IEnumerator GetEnumerator()
        {
            return SourceDataReader.GetEnumerator();
        }

        /// <inheritdoc />
        public override Type GetFieldType(int ordinal)
        {
            return SourceDataReader.GetFieldType(ordinal);
        }

        /// <inheritdoc />
        public override T GetFieldValue<T>(int ordinal)
        {
            return SourceDataReader.GetFieldValue<T>(ordinal);
        }

        /// <inheritdoc />
        public override Task<T> GetFieldValueAsync<T>(int ordinal, CancellationToken cancellationToken)
        {
            return SourceDataReader.GetFieldValueAsync<T>(ordinal, cancellationToken);
        }

        /// <inheritdoc />
        public override float GetFloat(int ordinal)
        {
            return SourceDataReader.GetFloat(ordinal);
        }

        /// <inheritdoc />
        public override Guid GetGuid(int ordinal)
        {
            return SourceDataReader.GetGuid(ordinal);
        }

        /// <inheritdoc />
        public override short GetInt16(int ordinal)
        {
            return SourceDataReader.GetInt16(ordinal);
        }

        /// <inheritdoc />
        public override int GetInt32(int ordinal)
        {
            return SourceDataReader.GetInt32(ordinal);
        }

        /// <inheritdoc />
        public override long GetInt64(int ordinal)
        {
            return SourceDataReader.GetInt64(ordinal);
        }

        /// <inheritdoc />
        public override string GetName(int ordinal)
        {
            return SourceDataReader.GetName(ordinal);
        }

        /// <inheritdoc />
        public override int GetOrdinal(string name)
        {
            return SourceDataReader.GetOrdinal(name);
        }

        /// <inheritdoc />
        public override Type GetProviderSpecificFieldType(int ordinal)
        {
            return SourceDataReader.GetProviderSpecificFieldType(ordinal);
        }

        /// <inheritdoc />
        public override object GetProviderSpecificValue(int ordinal)
        {
            return SourceDataReader.GetProviderSpecificValue(ordinal);
        }

        /// <inheritdoc />
        public override int GetProviderSpecificValues(object[] values)
        {
            return SourceDataReader.GetProviderSpecificValues(values);
        }

        /// <inheritdoc />
        public override DataTable GetSchemaTable()
        {
            return SourceDataReader.GetSchemaTable();
        }

        /// <inheritdoc />
        public override Stream GetStream(int ordinal)
        {
            return SourceDataReader.GetStream(ordinal);
        }

        /// <inheritdoc />
        public override string GetString(int ordinal)
        {
            return SourceDataReader.GetString(ordinal);
        }

        /// <inheritdoc />
        public override TextReader GetTextReader(int ordinal)
        {
            return SourceDataReader.GetTextReader(ordinal);
        }

        /// <inheritdoc />
        public override object GetValue(int ordinal)
        {
            return SourceDataReader.GetValue(ordinal);
        }

        /// <inheritdoc />
        public override int GetValues(object[] values)
        {
            return SourceDataReader.GetValues(values);
        }

        /// <inheritdoc />
        public override bool IsDBNull(int ordinal)
        {
            return SourceDataReader.IsDBNull(ordinal);
        }

        /// <inheritdoc />
        public override Task<bool> IsDBNullAsync(int ordinal, CancellationToken cancellationToken)
        {
            return SourceDataReader.IsDBNullAsync(ordinal, cancellationToken);
        }

        /// <inheritdoc />
        public override bool NextResult()
        {
            return SourceDataReader.NextResult();
        }

        /// <inheritdoc />
        public override bool Read()
        {
            return SourceDataReader.Read();
        }

        /// <inheritdoc />
        public override Task<bool> ReadAsync(CancellationToken cancellationToken)
        {
            return SourceDataReader.ReadAsync(cancellationToken);
        }

        #region Implementation of IDisposable

        /// <summary>
        /// Detecta llamados redundantes
        /// </summary>
        private bool disposed = false;

        ~UtcDbDataReader()
        {
            // Termina y no ejecuta el dispose
            Dispose(false);
        }

        /// <summary>
        /// Implementación of <see cref="IDisposable.Dispose"/>
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Implementación protegida del patrón Dispose.
        /// </summary>
        /// <param name="disposing">Flag para el destructor de la clase</param>
        protected override void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    SourceDataReader.Dispose();
                }

                disposed = true;
            }
        }

        #endregion
    }
}