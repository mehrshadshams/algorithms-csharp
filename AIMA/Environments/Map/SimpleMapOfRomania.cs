namespace AIMA.Environments.Map
{
    public class SimplifiedRoadMapOfRomania : ExtendableMap
    {
        public SimplifiedRoadMapOfRomania()
        {
            InitMap(this);
        }

        // The different locations in the simplified map of part of Romania
        public static readonly string ORADEA = "Oradea";
        public static readonly string ZERIND = "Zerind";
        public static readonly string ARAD = "Arad";
        public static readonly string TIMISOARA = "Timisoara";
        public static readonly string LUGOJ = "Lugoj";
        public static readonly string MEHADIA = "Mehadia";
        public static readonly string DOBRETA = "Dobreta";
        public static readonly string SIBIU = "Sibiu";
        public static readonly string RIMNICU_VILCEA = "RimnicuVilcea";
        public static readonly string CRAIOVA = "Craiova";
        public static readonly string FAGARAS = "Fagaras";
        public static readonly string PITESTI = "Pitesti";
        public static readonly string GIURGIU = "Giurgiu";
        public static readonly string BUCHAREST = "Bucharest";
        public static readonly string NEAMT = "Neamt";
        public static readonly string URZICENI = "Urziceni";
        public static readonly string IASI = "Iasi";
        public static readonly string VASLUI = "Vaslui";
        public static readonly string HIRSOVA = "Hirsova";
        public static readonly string EFORIE = "Eforie";

        /**
	 * Initializes a map with a simplified road map of Romania.
	 */
        public static void InitMap(ExtendableMap map)
        {
            // mapOfRomania
            map.Clear();
            map.AddBidirectionalLink(ORADEA, ZERIND, 71.0);
            map.AddBidirectionalLink(ORADEA, SIBIU, 151.0);
            map.AddBidirectionalLink(ZERIND, ARAD, 75.0);
            map.AddBidirectionalLink(ARAD, TIMISOARA, 118.0);
            map.AddBidirectionalLink(ARAD, SIBIU, 140.0);
            map.AddBidirectionalLink(TIMISOARA, LUGOJ, 111.0);
            map.AddBidirectionalLink(LUGOJ, MEHADIA, 70.0);
            map.AddBidirectionalLink(MEHADIA, DOBRETA, 75.0);
            map.AddBidirectionalLink(DOBRETA, CRAIOVA, 120.0);
            map.AddBidirectionalLink(SIBIU, FAGARAS, 99.0);
            map.AddBidirectionalLink(SIBIU, RIMNICU_VILCEA, 80.0);
            map.AddBidirectionalLink(RIMNICU_VILCEA, PITESTI, 97.0);
            map.AddBidirectionalLink(RIMNICU_VILCEA, CRAIOVA, 146.0);
            map.AddBidirectionalLink(CRAIOVA, PITESTI, 138.0);
            map.AddBidirectionalLink(FAGARAS, BUCHAREST, 211.0);
            map.AddBidirectionalLink(PITESTI, BUCHAREST, 101.0);
            map.AddBidirectionalLink(GIURGIU, BUCHAREST, 90.0);
            map.AddBidirectionalLink(BUCHAREST, URZICENI, 85.0);
            map.AddBidirectionalLink(NEAMT, IASI, 87.0);
            map.AddBidirectionalLink(URZICENI, VASLUI, 142.0);
            map.AddBidirectionalLink(URZICENI, HIRSOVA, 98.0);
            map.AddBidirectionalLink(IASI, VASLUI, 92.0);
            // addBidirectionalLink(VASLUI - already all linked
            map.AddBidirectionalLink(HIRSOVA, EFORIE, 86.0);
            // addBidirectionalLink(EFORIE - already all linked

            // distances and directions
            // reference location: Bucharest
            map.SetDistAndDirToRefLocation(ARAD, 366, 117);
            map.SetDistAndDirToRefLocation(BUCHAREST, 0, 360);
            map.SetDistAndDirToRefLocation(CRAIOVA, 160, 74);
            map.SetDistAndDirToRefLocation(DOBRETA, 242, 82);
            map.SetDistAndDirToRefLocation(EFORIE, 161, 282);
            map.SetDistAndDirToRefLocation(FAGARAS, 176, 142);
            map.SetDistAndDirToRefLocation(GIURGIU, 77, 25);
            map.SetDistAndDirToRefLocation(HIRSOVA, 151, 260);
            map.SetDistAndDirToRefLocation(IASI, 226, 202);
            map.SetDistAndDirToRefLocation(LUGOJ, 244, 102);
            map.SetDistAndDirToRefLocation(MEHADIA, 241, 92);
            map.SetDistAndDirToRefLocation(NEAMT, 234, 181);
            map.SetDistAndDirToRefLocation(ORADEA, 380, 131);
            map.SetDistAndDirToRefLocation(PITESTI, 100, 116);
            map.SetDistAndDirToRefLocation(RIMNICU_VILCEA, 193, 115);
            map.SetDistAndDirToRefLocation(SIBIU, 253, 123);
            map.SetDistAndDirToRefLocation(TIMISOARA, 329, 105);
            map.SetDistAndDirToRefLocation(URZICENI, 80, 247);
            map.SetDistAndDirToRefLocation(VASLUI, 199, 222);
            map.SetDistAndDirToRefLocation(ZERIND, 374, 125);
        }
    }
}