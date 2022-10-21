using System.Text;

namespace VideOde.Terminal
{
    public class TabularData<T>
    {
        private const string defaultSeparator = "|";

        private IEnumerable<T> _data;
        private IEnumerable<IEnumerable<string>> _columns;
        private IEnumerable<string> _headings;
        private ICollection<ICollection<string>> _rows;

        public TabularData(IEnumerable<T> data, IEnumerable<IEnumerable<string>> columns, IEnumerable<string> headings)
        {
            _data = data;
            _columns = columns;
            _headings = headings;
            _rows = new List<ICollection<string>>();

            foreach (string _ in _columns.ElementAt(0))
            {
                _rows.Add(new List<string>());
            }

            foreach (IEnumerable<string> column in _columns)
            {
                for (int rowCounter = 0; rowCounter < column.Count(); rowCounter++)
                {
                    _rows.ElementAt(rowCounter).Add(column.ElementAt(rowCounter));
                }
            }
        }

        public IEnumerable<string> GetPrintableRows(bool includeHeader = true, bool includeNumerals = true, string separator = defaultSeparator)
        {
            if (includeHeader)
            {
                yield return GetHeader(separator, includeNumerals);
                yield return GetDivider(separator, includeNumerals);
            }

            int rowNumber = 1;
            foreach (IEnumerable<string> row in _rows)
            {
                int numeral = 0;
                if (includeNumerals)
                {
                    numeral = rowNumber;
                }
                yield return GetFormattedLine(row, separator, numeral);
                rowNumber++;
            }
        }

        public IEnumerable<string> GetPrintableRow(int index, string separator = defaultSeparator, bool includeHeader = true)
        {
            if (includeHeader)
            {
                yield return GetHeader(separator, false);
                yield return GetDivider(separator, false);
            }

            yield return GetFormattedLine(_rows.ElementAt(index), separator, 0);
        }

        private string GetHeader(string separator, bool includeNumerals)
        {
            int numeral = 0;
            if (includeNumerals)
            {
                numeral = -1;
            }

            return GetFormattedLine(_headings, separator, numeral);
        }

        private string GetDivider(string separator, bool includeNumerals)
        {
            int numeral = 0;
            if (includeNumerals)
            {
                numeral = -1;
            }

            List<string> emptyRow = new();

            foreach (string _ in _headings)
            {
                emptyRow.Add(string.Empty);
            }

            return GetFormattedLine(emptyRow, separator, numeral, '-');
        }

        private string GetFormattedLine(IEnumerable<string> row, string separator, int numeral, char paddingCharacter = ' ')
        {
            StringBuilder sb = new();

            if (numeral > 0) //A valid numeral
            {
                sb.Append($"{separator} {numeral.ToString().PadRight(5)}");
            }
            else if (numeral == -1) //No content, use padding character
            {
                sb.Append($"{separator} {paddingCharacter.ToString().PadRight(4, paddingCharacter)} ");
            }

            for (int i = 0; i < row.Count(); i++)
            {
                string cellData = row.ElementAt(i);
                int cellWidth = cellData.Length;

                int columnHeadingWidth = _headings.ElementAt(i).Length;
                int ColumnDataMaxWidth = _columns.ElementAt(i).Max(cell => cell.Length);
                int contentWidest = Math.Max(columnHeadingWidth, ColumnDataMaxWidth);

                string formattedData = cellData;
                if (cellWidth < contentWidest)
                {
                    int spaces = contentWidest - cellWidth;
                    int padLeft = spaces / 2 + cellWidth;

                    formattedData = cellData.PadLeft(padLeft, paddingCharacter).PadRight(contentWidest, paddingCharacter);
                }

                sb.Append($"{separator} {formattedData} ");
            }

            sb.Append(separator);

            return sb.ToString();
        }
    }
}
