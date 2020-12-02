
using System.Collections.Generic;


namespace DataserviceLib
{
    public interface IDataService
    {
        IList<Searchhistory> GetSearchHistory(int page = 0, int pagesize = 50);

        Titlebasics GetTitle(string tconst);
    }
}
