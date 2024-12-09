namespace InveonBootcamp.AssigmentW1.LSP.NonCompliant
{
    public abstract class Phone
    {
        public void Call()
        {
            Console.WriteLine("Phone Call");
        }

        public abstract void TakePhoto();
    }

    public class Iphone : Phone
    {
        public void FaceTime()
        {
            Console.WriteLine("Iphone FaceTime");
        }

        public override void TakePhoto()
        {
            Console.WriteLine("Phone Take Photo");
        }
    }

    public class Nokia : Phone
    {
        public override void TakePhoto()
        {
            throw new NotImplementedException();
        }
    }
}
