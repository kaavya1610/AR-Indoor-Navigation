using Unity.Collections;
using System.Collections.Generic;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using UnityEngine;
using ZXing;

public class QrCodeRecenter : MonoBehaviour
 {
     [SerializeField]
     private ARSession session;
     [SerializeField]
     private ARSessionOrigin sessionOrigin;
     [SerializeField]
     private ARCameraManager cameraManager;
     [SerializeField]
     private List<Target> navigationTargetObjects = new List<Target>();

     private Texture2D cameraImageTexture;
     private IBarcodeReader reader = new BarcodeReader(); //create a barcode reader instance

     // Update is called once per frame
     public void Update()
     {
        if (Input.GetKeyDown(KeyCode.Space))
        {
             SetQrCodeRecenterTarget("HALL");


        }    
     }
     void OnEnable()
     {
        cameraManager.frameReceived += OnCameraFrameReceived;
      }

      void OnDisable()
      {
         cameraManager.frameReceived -= OnCameraFrameReceived;
      }

      private void OncameraFrameReceived(ARCameraFrameEventArgs eventArgs)
      {
         if (!cameraManager. TryAcquireLatestCpuImage(out XRCpuImage image))
         {
            return;
         }
         var concersionParams = new XRCpuImage.ConversionParams
         {
           // Get the entire image.
           imputRect = new RextInt(0,0, image.width, image.height),

           // Downsample by 2.
           outputDimensions = new Vecto2Int(image.width / 2, image.height /2),

           //Choose RGBA format.
           outputFormat = TextureFormat.RGBA32,

           //Flip across the vertical axis(mirroe image).
           transformation = XRCpuImage.Transformation.MirrorY
       };

       //See how many bytes you need to store the final image;
       int size = image.GetConvertedDataSize(conversionParams);

       //Allocate a buffer to store the image.
       var buffer = new NativeArray<byte>(size, Allocator.Temp);

       // Extract the image data
       image.Convert(conversionParams,buffer(;

       // The image was converted to RGBA32 format and written into the provided buffer
       // Sp you can dispose of the XRCpuImage. You must do this or it will leak resources.
       image.Dispose();

       // At this poimt, you can process the image, pass it to a computer vision algorithm, etc.
       // In this example, you apply it to a texture to visualize it.

       // You've got the data; let's put it into a texture so you can visualize it.
       cameraIamgeTexture = new Texture2D(
          conversionParams.outputDimensions.x,
          conversionParams.outputDimensions.y,
          conversionParams.outputFormat,
          false);

        cameraImageTexture.LoadRawTextureData(buffer);
        cameraImageTexture.Apply();

        // Done with your temporary data, so you can dispose it.
        buffer.Dispose();
        var result = reader.Decode(cameraImageTexture.GetPixels32(), cameraImageTexture.width, cameraImageTexture.height);

        if(result !=null)
        {
            SetQrCodeRecenterTarget(result.Text);
        }
     }
     private void SetQRCodeRecenterTarget(string targetText)
     {
        Target currentTarget = navigationTargetObjects.Find(x => x.Name.ToLower().Equals(targetText.ToLower()));
        if (currentTarget != null)
        {
             session.Reset();
             sessionOrigin.transform.position = currentTarget.PositionObject.transform.position;
             sessionOrigin.transform.rotation = currentTarget.PositionObject.transform.rotation;
        }   

      }
}     
