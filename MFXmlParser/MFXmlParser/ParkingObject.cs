namespace MFXmlParser {
    public class ParkingObject {
        string VehicleType;
        public BoundingBox Box;
        
        public ParkingObject() {

        }

        public ParkingObject(string _VehicleType, BoundingBox _Box) {
            VehicleType = _VehicleType;
            Box = _Box;
        }
    }
}
