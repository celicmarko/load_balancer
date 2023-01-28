using Common;
using System.Data;

namespace DatabaseCRUD
{
    public interface IDatabaseCRUDComp
    {
        int UpisUBazuBrojilo(Podatak podatak, IDbConnection connection);
        int UpisUBazuBrPotrosnja(PodatakPotrosnja podatakPotrosnja, IDbConnection connection);

    }
}
