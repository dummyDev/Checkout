using System.Collections.Generic;

namespace Checkout.DummyCommerce.DataAccessLayer.Repositories
{
    public class RepositoryReturnType<T>
    {
        private List<T> _returnList;
        public IList<T> ReturnList => _returnList;

        public void WriteReturnList(List<T> t)
        {
            _returnList = t;
        }

        public T ReturnData { get; set; }
        public string Message { get; set; }
        public int RecordsAffected { get; set; }
        public bool IsTransactionSuccess { get; set; }
    }
}