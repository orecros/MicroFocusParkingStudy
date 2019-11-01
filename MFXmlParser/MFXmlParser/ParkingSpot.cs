using System.Collections.Generic;

namespace MFXmlParser {
    public class ParkingSpot {
        List<ParkingObject> associatedParkings;
        BoundingBox averageBox;
        BoundingBox cumulativeBox;
        public BoundingBox Box {
            get { return averageBox; }
        }

        public ParkingSpot() {
            associatedParkings = new List<ParkingObject>();

            averageBox = new BoundingBox();
            cumulativeBox = new BoundingBox();
        }

        public void AddParkingObject(ParkingObject newObject) {

            // add our new object
            associatedParkings.Add(newObject);

            cumulativeBox.top += newObject.Box.top;
            cumulativeBox.left += newObject.Box.left;
            cumulativeBox.width += newObject.Box.width;
            cumulativeBox.height += newObject.Box.height;

            RecalculateAverageBox();
        }

        public void RecalculateAverageBox() {
            averageBox.top = (int)((float)cumulativeBox.top / associatedParkings.Count);
            averageBox.left = (int)((float)cumulativeBox.left / associatedParkings.Count);
            averageBox.width = (int)((float)cumulativeBox.width / associatedParkings.Count);
            averageBox.height = (int)((float)cumulativeBox.height / associatedParkings.Count);
        }
    }
}
