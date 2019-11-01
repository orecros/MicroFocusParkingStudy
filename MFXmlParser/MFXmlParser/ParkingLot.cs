using System.Collections.Generic;

namespace MFXmlParser {
    public class ParkingLot {
        List<ParkingSpot> associatedSpots;
        /// <summary>
        /// 2 parking events with bounding boxes that shve at least this much area are considered the same spot
        /// </summary>
        float spotDetectionThreshold;


        public ParkingLot(float _spotDetectionThreshold) {
            spotDetectionThreshold = _spotDetectionThreshold;
            associatedSpots = new List<ParkingSpot>();
        }

        public void AddParkingEvent(ParkingObject parkingObject) {

            // check against our current list of parking spots
            foreach(ParkingSpot spot in associatedSpots) {

                // if this new event significantly overlaps with another parking spot,
                if(BoundingBox.DirectionlessOverlap(spot.Box,parkingObject.Box) > spotDetectionThreshold) {
                    // add this event to this parking spot
                    spot.AddParkingObject(parkingObject);

                    // and then stop 
                    return;
                }
            }

            //if we didn't find a parking spot that overlaps enough, make a new spot.
            ParkingSpot newSpot = new ParkingSpot();
            newSpot.AddParkingObject(parkingObject);
            associatedSpots.Add(newSpot);
        }
    }
}
