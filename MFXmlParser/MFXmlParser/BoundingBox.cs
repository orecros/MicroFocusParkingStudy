using System;

namespace MFXmlParser {
    public class BoundingBox {
        public int top;
        public int left;
        public int width;
        public int height;

        public BoundingBox() {

        }

        public BoundingBox(int _top = 0, int _left = 0, int _width = 0, int _height = 0) {
            top = _top;
            left = _left;
            width = _width;
            height = _height;
        }
        
        public static BoundingBox Average(BoundingBox[] boxes) {
            BoundingBox newBox = new BoundingBox(0,0,0,0);

            foreach(BoundingBox b in boxes) {
                newBox.top += b.top;
                newBox.left += b.left;
                newBox.width += b.width;
                newBox.height += b.height;
            }

            return newBox;
        }
        public int area {
            get {
                return width * height;
            }
        }
        public int bottom {
            get {
                return top + height;
            }
        }
        public int right {
            get {
                return left + width;
            }
        }

        /// <summary>
        /// get the fraction of other that overlaps with this bounding box
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public float Overlap(BoundingBox other) {
            return (float)OverlappingArea(other) / (float)area;
        }
        
        /// <summary>
        /// gets the fraction of overlap between the 2 boxes, choosing the larger of the 2 possible values
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public static float DirectionlessOverlap(BoundingBox a, BoundingBox b) {
            return Math.Max(a.Overlap(b),b.Overlap(a));
        }

        public int OverlappingArea(BoundingBox other) {

            // construct a new rectangle between the inner edges of both bounding boxes
            int closestLeft = Math.Max(left, other.left);
            int closestRight = Math.Min(right, other.right);
            int closestTop = Math.Max(top, other.top);
            int closestBottom = Math.Min(bottom, other.bottom);

            // if any of those edges are negative, clamp to zero (the boxes do not overlap at all in these cases)
            int width = Math.Max(0, closestRight - closestLeft);
            int height = Math.Max(0, closestBottom - closestTop);

            // return our new area
            return width * height;
        }
    }
}
