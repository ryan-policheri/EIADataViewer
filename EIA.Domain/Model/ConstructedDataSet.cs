using EIA.Domain.Enums;
using PoliCommon.Extensions;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Text;

namespace EIA.Domain.Model
{
    public class ConstructedDataSet
    {
        private readonly Series _series;

        public ConstructedDataSet(Series series)
        {
            if (series == null) throw new ArgumentNullException(nameof(series));
            _series = series;
            Table = BuildDataTable();
        }

        public string Name => _series.Name;

        public string Description => _series.Description;

        public Frequency Frequency
        {
            get
            {
                switch (_series.Frequency.CapsAndTrim())
                {
                    case "M":
                        return Frequency.Monthly;
                    case "Q":
                        return Frequency.Quarterly;
                    case "A":
                        return Frequency.Yearly;
                    default:
                        throw new NotImplementedException("Frequency text to enum conversion not implemented");
                }
            }
        }

        public string Units => _series.Units;

        public DataTable Table { get; }

        private DataTable BuildDataTable()
        {
            DataTable table = new DataTable();
            table.Columns.Add("PERIOD", typeof(DateTime));
            table.Columns.Add("VALUE", typeof(double));
            CultureInfo us = new CultureInfo("en-US");

            foreach (SeriesData dataPoint in _series.Data)
            {
                DataRow row = table.NewRow();
                row["PERIOD"] = DateTime.ParseExact(dataPoint.ColumnHeader,  "yyyyMM", us);
                row["VALUE"] = dataPoint.ColumnValue;
                table.Rows.Add(row);
            }

            return table;
        }
    }
}
