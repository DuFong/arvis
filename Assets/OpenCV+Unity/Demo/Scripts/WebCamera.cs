namespace OpenCvSharp.Demo
{
	using System;
	using UnityEngine;
	using UnityEngine.UI;
	using OpenCvSharp;
    using System.Threading;
    using System.Collections.Generic;

    // Many ideas are taken from http://answers.unity3d.com/questions/773464/webcamtexture-correct-resolution-and-ratio.html#answer-1155328

    /// <summary>
    /// Base WebCamera class that takes care about video capturing.
    /// Is intended to be sub-classed and partially overridden to get
    /// desired behavior in the user Unity script
    /// </summary>
    public /*abstract*/class WebCamera: MonoBehaviour
	{
        // ���� ó���� ���� ������
        private Thread _threadInput;
        private Thread _threadRemoveFace;
        private Thread _threadGetSkinMask;
        private Thread _threadGetHandPoint;
        private Thread _threadOutput;

        // �½�ũ ť
        private Queue<Task> _qInput2Face;
        private Queue<Task> _qFace2Mask;
        private Queue<Task> _qMask2Point;
        private Queue<Task> _qPoint2Output;

        // �� �½�ũ ť�� ���� ��
        private object _lockInput2Face;
        private object _lockFace2Mask;
        private object _lockMask2Point;
        private object _lockPoint2Output;

        //�ּ�
        //private SkinDetector _skinDetector;
        //private FaceDetector _faceDetector;
        //private HandDetector _handDetector;
        //private HandManager _handManager;

        private bool _isQuit;

        // ������(��ġ��) ������Ʈ
        [SerializeField, Header("Object to Move")]
        private GameObject _object;
        // ���� ���� �հ���
        [SerializeField, Header("Finger & Center")]
        private GameObject[] _handObject;

        System.Diagnostics.Stopwatch _stopWatch = new System.Diagnostics.Stopwatch();

        /// <summary>
        /// Target surface to render WebCam stream
        /// </summary>
        public GameObject Surface;

		private Nullable<WebCamDevice> webCamDevice = null;
		private WebCamTexture webCamTexture = null;
		private Texture2D renderedTexture = null;

		/// <summary>
		/// A kind of workaround for macOS issue: MacBook doesn't state it's webcam as frontal
		/// </summary>
		protected bool forceFrontalCamera = false;

		/// <summary>
		/// WebCam texture parameters to compensate rotations, flips etc.
		/// </summary>
		protected Unity.TextureConversionParams TextureParameters { get; private set; }

		/// <summary>
		/// Camera device name, full list can be taken from WebCamTextures.devices enumerator
		/// </summary>
		public string DeviceName
		{
			get
			{
				return (webCamDevice != null) ? webCamDevice.Value.name : null;
			}
			set
			{
				// quick test
				if (value == DeviceName)
					return;

				if (null != webCamTexture && webCamTexture.isPlaying)
					webCamTexture.Stop();

				// get device index
				int cameraIndex = -1;
				for (int i = 0; i < WebCamTexture.devices.Length && -1 == cameraIndex; i++)
				{
					if (WebCamTexture.devices[i].name == value)
						cameraIndex = i;
				}

				// set device up
				if (-1 != cameraIndex)
				{
					webCamDevice = WebCamTexture.devices[cameraIndex];
					webCamTexture = new WebCamTexture(/*webCamDevice.Value.name*/Screen.width, Screen.height);

					// read device params and make conversion map
					ReadTextureConversionParameters();

					webCamTexture.Play();
				}
				else
				{
					throw new ArgumentException(String.Format("{0}: provided DeviceName is not correct device identifier", this.GetType().Name));
				}
			}
		}

		/// <summary>
		/// This method scans source device params (flip, rotation, front-camera status etc.) and
		/// prepares TextureConversionParameters that will compensate all that stuff for OpenCV
		/// </summary>
		private void ReadTextureConversionParameters()
		{
			Unity.TextureConversionParams parameters = new Unity.TextureConversionParams();

            // frontal camera - we must flip around Y axis to make it mirror-like
            parameters.FlipHorizontally = forceFrontalCamera || webCamDevice.Value.isFrontFacing;
			
			// TODO:
			// actually, code below should work, however, on our devices tests every device except iPad
			// returned "false", iPad said "true" but the texture wasn't actually flipped

			// compensate vertical flip
			//parameters.FlipVertically = webCamTexture.videoVerticallyMirrored;
			
			// deal with rotation
			if (0 != webCamTexture.videoRotationAngle)
				parameters.RotationAngle = webCamTexture.videoRotationAngle; // cw -> ccw

			// apply
			TextureParameters = parameters;

			//UnityEngine.Debug.Log (string.Format("front = {0}, vertMirrored = {1}, angle = {2}", webCamDevice.isFrontFacing, webCamTexture.videoVerticallyMirrored, webCamTexture.videoRotationAngle));
		}

		/// <summary>
		/// Default initializer for MonoBehavior sub-classes
		/// </summary>
		protected virtual void Awake()
		{
            if(WebCamTexture.devices.Length > 0)
            {
                DeviceName = WebCamTexture.devices[WebCamTexture.devices.Length - 1].name;

                //�ּ�
                //_skinDetector = new SkinDetector();
                //_faceDetector = new FaceDetector();
                //_handDetector = new HandDetector();
                //_handManager = new HandManager(_object, _handObject, this.Surface);

                _threadInput = new Thread(new ThreadStart(RunInput));
                _threadRemoveFace = new Thread(new ThreadStart(RunRemoveFace));
                _threadGetSkinMask = new Thread(new ThreadStart(RunGetSkinMask));
                _threadGetHandPoint = new Thread(new ThreadStart(RunGetHandPoint));
                _threadOutput = new Thread(new ThreadStart(RunOutput));
                _threadInput.Start();
                _threadRemoveFace.Start();
                _threadGetSkinMask.Start();
                _threadGetHandPoint.Start();
                _threadOutput.Start();

                _qInput2Face = new Queue<Task>();
                _qFace2Mask = new Queue<Task>();
                _qMask2Point = new Queue<Task>();
                _qPoint2Output = new Queue<Task>();
            }
		}

		void OnDestroy() 
		{
			if (webCamTexture != null)
			{
				if (webCamTexture.isPlaying)
				{
					webCamTexture.Stop();
				}
				webCamTexture = null;
			}

			if (webCamDevice != null) 
			{
				webCamDevice = null;
			}

            if(_threadInput != null)
                _threadInput = null;
            if(_threadRemoveFace != null)
                _threadRemoveFace = null;
            if(_threadGetSkinMask != null)
                _threadGetSkinMask = null;
            if(_threadGetHandPoint != null)
                _threadGetHandPoint = null;
            if(_threadOutput != null)
                _threadOutput = null;
		}

        void OnApplicationQuit()
        {
            _isQuit = true;
        }

        /// <summary>
        /// Updates web camera texture
        /// </summary>
        /// �ּ�
  //      private void Update ()
		//{
  //          if (webCamTexture != null && webCamTexture.didUpdateThisFrame)
		//	{ 
		//		// this must be called continuously
		//		ReadTextureConversionParameters();

		//		// process texture with whatever method sub-class might have in mind
		//		if (ProcessTexture(webCamTexture, ref renderedTexture))
		//		{
		//			RenderFrame();
		//		}
		//	}
  //      }

        private void RunInput()
        {
            while(!_isQuit)
            {
                if(webCamTexture != null)
                {
                    ReadTextureConversionParameters();

                    // input ������ imgFrame, ���� ��µ� ���� ������ imgOrigin
                    Mat imgFrame = Unity.TextureToMat(webCamTexture, TextureParameters);
                    Mat imgOrigin = imgFrame.Clone();

                    lock(_lockInput2Face)
                    {
                        _qInput2Face.Enqueue(new Task(imgFrame, imgOrigin));
                    }
                }
            }
        }

        private void RunRemoveFace()
        {
            while(!_isQuit)
            {
                if(_qInput2Face.Count > 0)
                {
                    Task task;
                    Mat imgFrame;
                    FaceDetector faceDetector = new FaceDetector();

                    lock(_lockInput2Face)
                    {
                        task = _qInput2Face.Dequeue();
                    }
                    imgFrame = task.ImgFrame;

                    // �� ����
                    faceDetector.RemoveFaces(imgFrame, imgFrame);

                    lock(_lockFace2Mask)
                    {
                        _qFace2Mask.Enqueue(new Task(imgFrame, task.ImgOrigin));
                    }
                }
            }
        }

        private void RunGetSkinMask()
        {
            while(!_isQuit)
            {
                if(_qFace2Mask.Count > 0)
                {
                    Task task;
                    Mat imgFrame, imgMask;
                    SkinDetector skinDetector = new SkinDetector();

                    lock(_lockFace2Mask)
                    {
                        task = _qFace2Mask.Dequeue();
                    }
                    imgFrame = task.ImgFrame;

                    // �Ǻλ����� ����ũ �̹����� ����
                    imgMask = skinDetector.GetSkinMask(imgFrame);

                    lock(_lockMask2Point)
                    {
                        // �ش� ť�� ���� �ÿ��� imgMask�� �Բ� ����
                        _qMask2Point.Enqueue(new Task(imgFrame, task.ImgOrigin, imgMask));
                    }
                }
            }
        }

        private void RunGetHandPoint()
        {
            while(!_isQuit)
            {
                if(_qMask2Point.Count > 0)
                {
                    Task task, newTask;
                    Mat imgFrame, imgMask, imgHand;
                    HandDetector handDetector = new HandDetector();

                    lock(_lockMask2Point)
                    {
                        task = _qMask2Point.Dequeue();
                    }
                    imgFrame = task.ImgFrame;
                    imgMask = task.ImgOther;
                    
                    // ���� ������ ����
                    imgHand = handDetector.GetHandLineAndPoint(imgFrame, imgMask);

                    // ���� �۾��� ���� imgHand�� ����
                    newTask = new Task(imgFrame, task.ImgOrigin, imgHand);
                    // �ٸ� �����忡���� handDetector�� ����ϱ� ���� �½�ũ�� ����
                    newTask.HandDetector = handDetector;
                    lock(_lockPoint2Output)
                    {
                        _qPoint2Output.Enqueue(newTask);
                    }
                }
            }

        }

        private void RunOutput()
        {
            while(!_isQuit)
            {
                if(_qPoint2Output.Count > 0)
                {
                    Task task;
                    Mat imgHand;
                    HandDetector handDetector;
                    HandManager handManager = new HandManager(_object, _handObject, Surface);

                    lock(_lockPoint2Output)
                    {
                        task = _qPoint2Output.Dequeue();
                    }
                    imgHand = task.ImgOther;
                    handDetector = task.HandDetector;

                    // �� �ν��� ��Ȯ���� ������ ���� ���� ������Ʈ ���� ����
                    if(!handDetector.IsCorrectDetection)
                    {
                        renderedTexture = Unity.MatToTexture(task.ImgOrigin, renderedTexture);
                        return;
                    }

                    // �հ��� ������ �׸�
                    handDetector.DrawFingerPointAtImg(imgHand);

                    // ȭ����� �հ��� �� ��ǥ�� ���󼼰� ��ǥ�� ��ȯ
                    handManager.InputPoint(handDetector.FingerPoint, handDetector.Center);

                    // ���� ���� ������
                    handManager.MoveHand();

                    //�ּ�
                    //handDetector.MainPoint.Clear();
                    //handManager.Cvt3List.Clear();

                    renderedTexture = Unity.MatToTexture(task.ImgOrigin, renderedTexture);
                    return;
                }
            }
            RenderFrame();
        }

        /// <summary>
        /// Processes current texture
        /// This function is intended to be overridden by sub-classes
        /// </summary>
        /// <param name="input">Input WebCamTexture object</param>
        /// <param name="output">Output Texture2D object</param>
        /// <returns>True if anything has been processed, false if output didn't change</returns>
        /// �ּ�
        //protected abstract bool ProcessTexture(WebCamTexture input, ref Texture2D output);

		/// <summary>
		/// Renders frame onto the surface
		/// </summary>
		private void RenderFrame()
		{
			if (renderedTexture != null)
			{
				// apply
				Surface.GetComponent<RawImage>().texture = renderedTexture;

				// Adjust image ration according to the texture sizes 
				Surface.GetComponent<RectTransform>().sizeDelta = new Vector2(renderedTexture.width, renderedTexture.height);
			}
		}
	}
}