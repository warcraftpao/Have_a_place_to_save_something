using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodePieces.TeslaOnlineTest
{
     /// <summary>
     ///  这个是tesla的在线测试题，不能跑的，IQueryable对象需要自己mock
     /// </summary>
    public class InvoiceRepository
    {
        private IQueryable<Invoice> _invoices;
        public InvoiceRepository(IQueryable<Invoice> invoices)
        {

            _invoices = invoices ?? throw new NotImplementedException("the query is not Implemented!");
            // Console.WriteLine("Sample debug output");
        }

        /// <summary>
        /// Should return a total value of an invoice with a given id. If an invoice does not exist null should be returned.
        /// </summary>
        /// <param name="invoiceId"></param>
        /// <returns></returns>
        public decimal? GetTotal(int invoiceId)
        {
            if (invoiceId <= 0)
                throw new ArgumentException("invoiceId should greater than 0");

            var invoice = _invoices.SingleOrDefault(i => i.Id == invoiceId);
            if (invoice == null)
                return null;


            if (invoice.InvoiceItems == null)
                return 0;

            if (!invoice.InvoiceItems.Any())
                return 0;

            decimal total = 0;
            foreach (var item in invoice.InvoiceItems)
            {
                total += item.Count * item.Price;
            }
            return total;
        }

        /// <summary>
        /// Should return a total value of all unpaid invoices.
        /// </summary>
        /// <returns></returns>
        public decimal GetTotalOfUnpaid()
        {
            var unpaidInvoices = _invoices.Where(i => !i.AcceptanceDate.HasValue).ToList();
            if (!unpaidInvoices.Any())
            {
                return 0;
            }

            decimal? total = 0;

            foreach (var item in unpaidInvoices)
            {
                total += GetTotal(item.Id);
            }

            return total.Value;
        }

        /// <summary>
        /// Should return a dictionary where the name of an invoice item is a key and the number of bought items is a value.
        /// The number of bought items should be summed within a given period of time (from, to). Both the from date and the end date can be null.
        /// </summary>
        /// <param name="from"></param>
        /// <param name="to"></param>
        /// <returns></returns>
        public IReadOnlyDictionary<string, long> GetItemsReport(DateTime? from, DateTime? to)
        {
            var exp = _invoices;
            if (from.HasValue)
                exp = exp.Where(i => i.CreationDate >= from);

            if (to.HasValue)
                exp = exp.Where(i => i.CreationDate <= to);

            var invoices = exp.ToList();

            var dic = new Dictionary<string, long>();

            foreach (var item in invoices)
            {
                foreach (var InvoiceItem in item.InvoiceItems)
                {
                    if (dic.ContainsKey(InvoiceItem.Name))
                    {
                        dic[InvoiceItem.Name] += InvoiceItem.Count;
                    }
                    else
                    {
                        dic.Add(InvoiceItem.Name, InvoiceItem.Count);
                    }
                }
            }

            return dic;

        }
    }
}


public class Invoice
{
    // A unique numerical identifier of an invoice (mandatory)
    public int Id { get; set; }
    // A short description of an invoice (optional).
    public string Description { get; set; }
    //  A number of an invoice e.g. 134/10/2018 (mandatory).
    public string Number { get; set; }
    //  An issuer of an invoice e.g. Metz-Anderson, 600  Hickman Street,Illinois (mandatory).
    public string Seller { get; set; }
    //  A buyer of a service or a product e.g. John Smith, 4285  Deercove Drive, Dallas (mandatory).
    public string Buyer { get; set; }
    //  A date when an invoice was issued (mandatory).
    public DateTime CreationDate { get; set; }
    // A date when an invoice was paid (optional).
    public DateTime? AcceptanceDate { get; set; }
    //  A collection of invoice items for a given invoice (can be empty but is never null).
    public IList<InvoiceItem> InvoiceItems { get; }

    public Invoice()
    {
        InvoiceItems = new List<InvoiceItem>();
    }
}

public class InvoiceItem
{
    // A name of an item e.g. eggs.
    public string Name { get; set; }
    //  A number of bought items e.g. 10.
    public int Count { get; set; }
    //  A price of an item e.g. 20.5.
    public decimal Price { get; set; }
}

