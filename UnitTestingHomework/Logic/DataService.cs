using System.Collections.Generic;
using System.Linq;

namespace Logic
{
    public class DataService : IDataService
    {
        private List<int> _data;

        public DataService(int capacity)
        {
            _data = new List<int>(capacity);
        }

        public int ItemsCount
        {
            get
            {
                if (_data != null) return _data.Count;
                return 0;
            }
        }

        public void AddItem(int a)
        {
            _data.Add(a);
        }

        public int GetElementAt(int index)
        {
            return _data.ElementAt(index);
        }

        public IEnumerable<int> GetAllData()
        {
            return _data;
        }

        public void RemoveAt(int index)
        {
            _data.RemoveAt(index);
        }

        public void ClearAll()
        {
            _data.Clear();
        }

        public int GetMax()
        {
            return _data.Max();
        }
    }
}