using System;
using System.Collections;
using System.Linq;
using Domain.Problem.Data;
using ProblemViewer.Scripts.Problem.Data;
using ProblemViewer.Scripts.Viewer.Panel;
using UnityEngine;
using UnityEngine.UI;

namespace ProblemViewer.Scripts.Viewer.Item {
    public class GridPanelItemController : MonoBehaviour {
        [SerializeField] private Image imageContent;
        [SerializeField] private HorizontalLayoutGroup horizontal;
        [SerializeField] private TEXDraw tex;
        [SerializeField] private Outline outline;
        [SerializeField] private RectTransform panelRect;
        [SerializeField] private RectTransform texRect;

        public Image ImageContent => imageContent;

        private ImageData imageData = null;

        public GridLayoutType layoutType;

        public void CreateContent(BaseProblemData data, float fontSize, bool isTable) {
            tex.text = data.content;
            tex.size = fontSize;
            if (isTable) outline.enabled = true;
            if (data.images.Count > 0) {
                var image = data.images.First();
                imageData = image;
                imageContent.sprite = image.sprite;
                imageContent.preserveAspect = true;
                var imageRect = imageContent.GetComponent<RectTransform>();
                var imageWidth = (float) image.size.First;
                imageWidth = imageWidth < 100f ? 100f : imageWidth;
                var imageHeight = image.GetResizedHeight(imageWidth);
                imageRect.sizeDelta = new Vector2(imageWidth, imageHeight);
                imageContent.gameObject.SetActive(true);
                SetWidthImage();
            } else
                SetOnlyText();
        }

        public float SetCellSize(float width) {
            var texWidth = width / 10f;
            texRect.sizeDelta = new Vector2(texWidth, texRect.rect.height);
            if (layoutType.both || layoutType.onlyImage) {
                var imageWidth = width / 10f * 8;
                float imageHeight;
                if (imageData.width > imageData.height) {
                    imageHeight = imageData.GetResizedHeight(imageWidth);
                } else {
                    imageHeight = imageWidth;
                    imageWidth = imageData.GetResizedWidth(imageHeight);
                }
                imageContent.GetComponent<RectTransform>().sizeDelta = new Vector2(imageWidth, imageHeight);
                return imageHeight;
            } else {
                return 100f;
            }
        }

        public float SetImageSize(float width) {
            var texWidth = width / 10f;
            texRect.sizeDelta = new Vector2(texWidth, texRect.rect.height); 
            var imageWidth = width / 10f * 8;
            float imageHeight;
            if (imageData.width > imageData.height) {
                imageHeight = imageData.GetResizedHeight(imageWidth);
            } else {
                imageHeight = imageWidth;
                imageWidth = imageData.GetResizedWidth(imageHeight);
            }
            imageContent.GetComponent<RectTransform>().sizeDelta = new Vector2(imageWidth, imageHeight);
            return imageHeight;
        }
        
        private void SetOnlyText() {
            InitLayout();
            horizontal.childControlWidth = true;
        }

        private void SetWidthImage() {
            InitLayout();
            horizontal.childScaleWidth = true;
            horizontal.childScaleHeight = true;
            horizontal.childForceExpandWidth = false;
        }

        private void InitLayout() {
            horizontal.childControlWidth = false;
            horizontal.childScaleWidth = false;
            horizontal.childScaleHeight = false;
            horizontal.childForceExpandWidth = true;
        }
    }
}