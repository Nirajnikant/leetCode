// https://leetcode.com/problems/implement-router/?envType=daily-question&envId=2025-09-20

public class Router {

    public class Packet
    {
        public int Source {get; set;}
        public int Destination {get; set;}
        public int Timestamp {get; set;}

        public override bool Equals(object obj) 
        {
            if (obj is Packet other) {
                return Source == other.Source &&
                    Destination == other.Destination &&
                    Timestamp == other.Timestamp;
            }
            return false;
        }

        public override int GetHashCode() 
        {
            return HashCode.Combine(Source, Destination, Timestamp);
        }
    }

    private readonly int _memoryLimit;
    private readonly LinkedList<Packet> _packets;
    private readonly HashSet<Packet> _packetSet;
    private readonly Dictionary<int, List<int>> _destinationMap;

    public Router(int memoryLimit) 
    {
        _memoryLimit = memoryLimit;
        _packets = new LinkedList<Packet>();
        _packetSet = new HashSet<Packet>();
        _destinationMap = new Dictionary<int, List<int>>();
    }

    public bool AddPacket(int source, int destination, int timestamp) 
    {
        var packet = new Packet { Source = source, Destination = destination, Timestamp = timestamp };
        if (_packetSet.Contains(packet)) 
        {
            return false;
        }

        if (_packets.Count >= _memoryLimit) 
        {
            var old = _packets.First.Value;
            _packets.RemoveFirst();
            _packetSet.Remove(old);
            _destinationMap[old.Destination].Remove(old.Timestamp);
        }
        
        _packets.AddLast(packet);
        _packetSet.Add(packet);

        if (!_destinationMap.ContainsKey(destination)) 
        {
            _destinationMap[destination] = new List<int>();
        }
        _destinationMap[destination].Add(timestamp);
        return true;
    }
    
    public int[] ForwardPacket() 
    {
        if (_packets.Count <= 0) 
        {
            return [];
        }

        var first = _packets.First.Value;
        _packets.RemoveFirst();
        _packetSet.Remove(first);
        var timeStamps = _destinationMap[first.Destination];
        timeStamps.Remove(first.Timestamp);
        if (timeStamps.Count <=0) {
            _destinationMap.Remove(first.Destination);
        }

        return new int[] {first.Source, first.Destination, first.Timestamp};
    }
    
    public int GetCount(int destination, int startTime, int endTime) {
        if (!_destinationMap.ContainsKey(destination)) {
            return 0;
        }

        int sId = _destinationMap[destination].BinarySearch(startTime);
        if (sId >= 0)
        {
            while (sId >= 0 && _destinationMap[destination][sId] == startTime)
            {
                sId--;
            }

            sId++;
        }
        else
            sId = ~sId;

        int eId = _destinationMap[destination].BinarySearch(endTime);
        if (eId >= 0)
        {
            while (eId < _destinationMap[destination].Count && _destinationMap[destination][eId] == endTime)
            {
                eId++;
            }
        }
        else
            eId = ~eId;

        return eId-sId;
    }
}

/**
 * Your Router object will be instantiated and called as such:
 * Router obj = new Router(memoryLimit);
 * bool param_1 = obj.AddPacket(source,destination,timestamp);
 * int[] param_2 = obj.ForwardPacket();
 * int param_3 = obj.GetCount(destination,startTime,endTime);
 */