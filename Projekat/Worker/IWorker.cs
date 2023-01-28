using Common;

namespace Worker_Cmp
{
    public interface IWorker
    {
        bool SendToDatabseCrud(PodatakPotrosnja podatak);
    }
}
