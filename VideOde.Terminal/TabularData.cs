using System.Text;

namespace VideOde.Terminal
{
    public class TabularData
    {
        private const string defaultSeparator = "|";

        private IEnumerable<IEnumerable<string>> _columns;

        private IEnumerable<string> _headings;

        private List<List<string>> _rows;

        public string GetTableData(int row, string property)
        {
            int indexOfProperty = _headings.ToList().IndexOf(property);

            return GetTableData(row, indexOfProperty);
        }

        public string GetTableData(int row, int column)
        {
            return _rows[row][column];
        }

        public TabularData(IEnumerable<IEnumerable<string>> columns, IEnumerable<string> headings)
        {
            _columns = columns;

            _headings = headings;

            _rows = new();

            foreach (var _ in _columns.ElementAt(0))
            {
                _rows.Add(new List<string>());
            }

            foreach (IEnumerable<string> column in _columns)
            {
                for (int rowCounter = 0; rowCounter < column.Count(); rowCounter++)
                {
                    _rows[rowCounter].Add(column.ElementAt(rowCounter));
                }
            }
        }

        public IEnumerable<string> GetPrintableRows(string separator = defaultSeparator, bool includeHeader = true)
        {
            if (includeHeader)
            {
                yield return GetHeader(separator);
            }

            foreach (var row in _rows)
            {
                yield return GetLine(row, separator);
            }
        }

        public string GetPrintableRow(int index, string separator = defaultSeparator)
        {
            return GetLine(_rows.ElementAt(index), separator);
        }

        public string GetHeader(string separator = defaultSeparator)
        {
            return GetLine(_headings, separator);
        }

        public string GetLine(IEnumerable<string> row, string separator)
        {
            StringBuilder sb = new();

            for (int i = 0; i < row.Count(); i++)
            {
                string cellData = row.ElementAt(i);
                int cellWidth = cellData.Length;
                int columnWidth = GetMaxLengthOfStrings(_columns.ElementAt(i));

                string header = _headings.ElementAt(i);
                int headerWidth = header.Length;

                string formattedData = cellData;

                if (cellWidth < columnWidth)
                {
                    int spaces = columnWidth - cellWidth;
                    int padLeft = spaces / 2 + cellWidth;

                    formattedData = cellData.PadLeft(padLeft).PadRight(columnWidth);
                }
                else if (cellWidth < headerWidth)
                {
                    int spaces = headerWidth - cellWidth;
                    int padLeft = spaces / 2 + cellWidth;

                    formattedData = cellData.PadLeft(padLeft).PadRight(headerWidth);
                }

                sb.Append($"{separator} {formattedData} ");
            }

            sb.Append(separator);

            return sb.ToString();
        }

        private static int GetMaxLengthOfStrings(IEnumerable<string> list)
        {
            return list.Max(x => x.Length);
        }
    }
}
