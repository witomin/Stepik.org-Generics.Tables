using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Generics.Tables
{
    public class Table<T1, T2, T3>
    {
        public Table()
        {
            Data = new List<Cell>();
            Columns = new List<T2>();
            Rows = new List<T1>();
        }
        List<Cell> Data;
        public List<T2> Columns { get; set; }
        public List<T1> Rows { get; set; }
        public TableOpen Open { get => new TableOpen(this); }
        public TableExisted Existed { get => new TableExisted(this); }

        public void AddRow(T1 row)
        {
            if (!Rows.Contains(row)) Rows.Add(row);
        }
        public void AddColumn(T2 col)
        {
            if (!Columns.Contains(col)) Columns.Add(col);
        }
        public class Cell
        {
            public T1 Row { get; set; }
            public T2 Col { get; set; }
            public T3 Data { get; set; }
        }
        public class TableOpen
        {
            Table<T1, T2, T3> Table;
            public TableOpen(Table<T1, T2, T3> table)
            {
                Table = table;
            }
            public T3 this[T1 row, T2 col]
            {
                get
                {
                    var CurrentCell = Table.Data.SingleOrDefault(x => x.Col.Equals(col) && x.Row.Equals(row));
                    if (CurrentCell != default(Cell))
                    {
                        return Table.Data.SingleOrDefault(x => x.Col.Equals(col) && x.Row.Equals(row)).Data;
                    }
                    else
                    {
                        return default(T3);
                    }
                }
                set
                {
                    var CurrentCell = Table.Data.SingleOrDefault(x => x.Col.Equals(col) && x.Row.Equals(row));
                    if (CurrentCell != default(Cell))
                    {
                        CurrentCell.Data = value;
                    }
                    else
                    {
                        Table.AddRow(row);
                        Table.AddColumn(col);
                        Table.Data.Add(new Cell() { Row = row, Col = col, Data = value });
                    }
                }
            }

        }
        public class TableExisted
        {
            Table<T1, T2, T3> Table;
            public TableExisted(Table<T1, T2, T3> table)
            {
                Table = table;
            }
            public T3 this[T1 row, T2 col]
            {
                get
                {
                    var CurrentCell = Table.Data.SingleOrDefault(x => x.Col.Equals(col) && x.Row.Equals(row));
                    if (CurrentCell != default(Cell))
                    {
                        return Table.Data.SingleOrDefault(x => x.Col.Equals(col) && x.Row.Equals(row)).Data;
                    }
                    else
                    {
                        if (!Table.Rows.Contains(row)) throw new ArgumentException();
                        if (!Table.Columns.Contains(col)) throw new ArgumentException();
                        return default(T3);
                    }
                }
                set
                {
                    var CurrentCell = Table.Data.SingleOrDefault(x => x.Col.Equals(col) && x.Row.Equals(row));
                    if (CurrentCell != default(Cell))
                    {
                        CurrentCell.Data = value;
                    }
                    else
                    {
                        if (!Table.Rows.Contains(row)) throw new ArgumentException();
                        if (!Table.Columns.Contains(col)) throw new ArgumentException();
                        Table.Data.Add(new Cell() { Row = row, Col = col, Data = value });
                    }
                }
            }

        }
    }
}

