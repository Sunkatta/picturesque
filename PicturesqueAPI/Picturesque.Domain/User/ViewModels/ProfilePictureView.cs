namespace Picturesque.Domain
{
    public class ProfilePictureView
    {
        public ProfilePictureView(string img2Base64)
        {
            Image2Base64 = img2Base64;
        }

        public string Image2Base64 { get; set; }
    }
}
