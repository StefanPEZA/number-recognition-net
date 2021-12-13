using System.Linq;

namespace Domain.Models
{
    public class Dataset : BaseEntity
    {
        public string Label { get; set; }
        public byte[] ImageMatrix { get; set; }
        public bool? IsTest { get; set; } = false;

        // override object.Equals
        public override bool Equals(object obj)
        {
            //       
            // See the full list of guidelines at
            //   http://go.microsoft.com/fwlink/?LinkID=85237  
            // and also the guidance for operator== at
            //   http://go.microsoft.com/fwlink/?LinkId=85238
            //

            if (obj == null || GetType() != obj.GetType())
            {
                return false;
            }

            Dataset other = obj as Dataset;

            bool equals = ImageMatrix.SequenceEqual(other.ImageMatrix) &&
                IsTest.Equals(other.IsTest) && Label.Equals(other.Label);

            return equals;
        }

        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }
    }
}
