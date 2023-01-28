using System.ServiceModel;

namespace Common
{
    [ServiceContract]
    public interface ISlanjePoruke
    {
        [OperationContract]
        void SlanjeMerenja(string merenje);
    }
}
