using BenchmarkDotNet.Attributes;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Data;

namespace Benchmarks
{
    [MemoryDiagnoser]
    public class GetOrdinalVsGetStringDataBase : IDisposable
    {
        private readonly string _connectionString = "User Id=usrecv;Password=ecvadm;Data Source=orascan.ibrascan.com.br:1521/ecvtaf";
        private readonly OracleConnection _oracleConnection;
        private readonly OracleCommand _oracleCommand;
        private bool disposedValue;

        [Params(CommandBehavior.Default, CommandBehavior.SequentialAccess)]
        public CommandBehavior Behavior { get; set; }

        public GetOrdinalVsGetStringDataBase()
        {
            _oracleConnection = new OracleConnection(_connectionString);
            _oracleCommand = _oracleConnection.CreateCommand();
            _oracleConnection.Open();

            _oracleCommand.Parameters.Clear();
            _oracleCommand.BindByName = true;
            _oracleCommand.CommandText = @" SELECT CODIGO, DESCRICAO FROM CHECKLIST_CONDICAO";
        }

        [Benchmark]
        public void GetStringByPosition()
        {
            using var reader = _oracleCommand.ExecuteReader(Behavior);
            while (reader.Read())
            {
                _ = reader.GetString(0);
                _ = reader.GetString(1);
            }
        }

        [Benchmark]
        public void GetStringByOrdinalWithName()
        {
            using var reader = _oracleCommand.ExecuteReader(Behavior);
            while (reader.Read())
            {
                _ = reader.GetString(reader.GetOrdinal("CODIGO"));
                _ = reader.GetString(reader.GetOrdinal("DESCRICAO"));
            }
        }

        [Benchmark]
        public void GetStringByOrdinalWithName2()
        {
            using var reader = _oracleCommand.ExecuteReader(Behavior);
            while (reader.Read())
            {
                _ = reader.GetString(reader.GetOrdinal("CODIGO"));
                _ = reader.GetString(reader.GetOrdinal("DESCRICAO"));
            }
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    _oracleCommand?.Dispose();
                    _oracleConnection?.Dispose();
                }

                disposedValue = true;
            }
        }

        public void Dispose()
        {
            // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }
    }
}
