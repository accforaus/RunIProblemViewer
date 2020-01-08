using System;
using B83.Image.BMP;
using UnityEngine;
using Utils;

namespace Domain.Problem.Data {
    [Serializable]
    public class ImageData {
        [SerializeField] public string name;
        [SerializeField] public string extension;
        [SerializeField] public string data;
        [SerializeField] public Pair<int, int> size;
        public Sprite sprite;
        public float width => size.First;
        public float height => size.Second;
        public float ratio;
        
        
        public float GetResizedHeight(float resizedWidth) => (resizedWidth * height) / width;

        public float GetResizedWidth(float resizedHeight) => (resizedHeight * width) / height;

        public ImageData() {
            name = "";
            extension = "";
            data = "";
            size = new Pair<int, int>();
        }

        public ImageData(string name, string extension, string data, Pair<int, int> size) {
            this.name = name;
            this.extension = extension;
            this.data = data;
            this.size = size;
        }

        public void Base64ToSprite() {
            Texture2D texture;
            var imageBytes = Convert.FromBase64String(data);
            if (extension == "bmp") {
                var bmpLoader = new BMPLoader();
                if (imageBytes.Length != 0) {
                    var bmpImage = bmpLoader.LoadBMP(imageBytes);
                    texture = bmpImage.ToTexture2D();
                } else texture = Texture2D.whiteTexture;
            }
            else {
                texture = new Texture2D(size.First, size.Second);
                texture.LoadImage(imageBytes);
            }
            sprite = Sprite.Create(texture, 
                new Rect(0.0f, 0.0f, texture.width, texture.height),
                new Vector2(0.5f, 0.5f), 100.0f);
        }
    }
}