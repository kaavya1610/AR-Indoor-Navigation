using TMPro;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using ZXing;

public class GetImageAlternative : MonoBehaviour
  {
      [SerializeField]
      private ARCameraBackground arCameraBackground;
      [SerializeField]
      private RenderTexture targetRenderTexture;
      [SerializeField]
      private TextMeshProUGUI qrCodeText;

      private Texture2D cameraImageTexture;

      private IBarcodeReader reader = new BarcodeReader(); // creates a harcode reader Instance

      private void update()
      {
         Graphics.Blit(null, targetRenderTexture, arCameraBackground.material);
         cameraImageTexture = new Texture2D(targetRenderTexture.width, targetRenderTexture.height, TextureFormat.RGBA32, false);
         Graphics.CopyTexture(targetRenderTexture, cameraImageTexture);

         //Detect and decode the barcode inside the bitmap
         var result = reader.Decoed(cameraImageRexture.GetPixel32(), cameraImageTexture.width, cameraImageTexture.height);

         //do something with the result

         if (result != null)
         {
           qrCodeText = result.Text;
           }
       }    
}
    
