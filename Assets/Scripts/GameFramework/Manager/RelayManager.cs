using Unity.Services.Relay.Models;
using System.Threading.Tasks;
using Unity.Services.Relay;
using System.Linq;


namespace GameFramework.Core.GameFramework.Manager
{
    public class RelayManager : Singleton<RelayManager>
    {

        private string _joinCode;
        private string _ip;
        private int _port;
        private byte[] _connectionData;
        private System.Guid _allocationId;


        public string GetAllocationId()
        {
            return _allocationId.ToString();
        }

        public string GetConnectionData()
        {
            return _connectionData.ToString();
        }

        public async Task<string> CreateRelay(int maxConnection)
        {
            Allocation allocation = await RelayService.Instance.CreateAllocationAsync(maxConnection);
            _joinCode = await RelayService.Instance.GetJoinCodeAsync(allocation.AllocationId);

            //No tengo ni pu�etera idea de porque se llama dtlsEnpoint pero se supone que es para la seguridad
            RelayServerEndpoint dtlsEnpoint = allocation.ServerEndpoints.First(conn => conn.ConnectionType == "dtls");
            _ip = dtlsEnpoint.Host;
            _port = dtlsEnpoint.Port;

            _allocationId = allocation.AllocationId;
            _connectionData = allocation.ConnectionData;

            return _joinCode;
        }
        public async Task<bool> JoinRelay(string joinCode)
        {
            _joinCode = joinCode;
            JoinAllocation allocation = await RelayService.Instance.JoinAllocationAsync(joinCode);

            RelayServerEndpoint dtlsEnpoint = allocation.ServerEndpoints.First(conn => conn.ConnectionType == "dtls");
            _ip = dtlsEnpoint.Host;
            _port = dtlsEnpoint.Port;

            _allocationId = allocation.AllocationId;
            _connectionData = allocation.ConnectionData;

            return true;

        }


    }

}
