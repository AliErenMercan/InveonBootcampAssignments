using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InveonBootcamp.AssigmentW1.LSP.Compliant
{
    public interface ITakePhoto
    {
        void TakePhoto();
    }

    public interface IPlaySnake
    {
        void PlaySnake();
    }

    public abstract class Phone
    {
        public void Call()
        {
            Console.WriteLine("Phone Call");
        }
    }

    public class Iphone : Phone, ITakePhoto
    {
        public void FaceTime()
        {
            Console.WriteLine("Iphone FaceTime");
        }

        public void TakePhoto()
        {
            Console.WriteLine("Phone Take Photo");
        }
    }

    public class Nokia : Phone, IPlaySnake
    {

        public void PlaySnake()
        {
            Console.WriteLine("Playing Snake..");
        }

    }
}
