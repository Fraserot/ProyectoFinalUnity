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
        private byte[] _key;
        private bool _isHost = false;
        private byte[] _connectionData;
        private byte[] _hostConnectionData;
        private System.Guid _allocationId;
        private byte[] _allocationIdBytes;
        public bool IsHost { get { return _isHost; } }

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

            //No tengo ni puñetera idea de porque se llama dtlsEnpoint pero se supone que es para la seguridad
            RelayServerEndpoint dtlsEnpoint = allocation.ServerEndpoints.First(conn => conn.ConnectionType == "dtls");
            _ip = dtlsEnpoint.Host;
            _port = dtlsEnpoint.Port;

            _allocationId = allocation.AllocationId;
            _allocationIdBytes = allocation.AllocationIdBytes;
            _connectionData = allocation.ConnectionData;
            _isHost = true;
            _key = allocation.Key;

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
            _allocationIdBytes = allocation.AllocationIdBytes;
            _connectionData = allocation.ConnectionData;
            _hostConnectionData = allocation.HostConnectionData;

            _key = allocation.Key;

            return true;

        }

        public (byte[]AllocationId, byte[]Key, byte[] ConnectionData, string _dtlsAddress, int _dtlsPort) GetHostConnectionInfo()
        {
            return (_allocationIdBytes, _key, _connectionData, _ip, _port);
        }
        public (byte[] AllocationId, byte[] Key, byte[] ConnectionData, byte[]HostConnectionData, string _dtlsAddress, int _dtlsPort) GetClientConnectionInfo()
        {
            return (_allocationIdBytes, _key, _connectionData,_hostConnectionData, _ip, _port);
        }

    }

}
