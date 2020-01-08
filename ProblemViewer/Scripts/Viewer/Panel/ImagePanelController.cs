using System;
using System.Collections.Generic;
using System.Linq;
using Domain.Problem.Data;
using ProblemViewer.Scripts.Viewer.Item;
using UnityEngine;
using UnityEngine.UI;

namespace ProblemViewer.Scripts.Viewer.Panel {
    public class ImagePanelController : GridLayoutController {
        [SerializeField] private GameObject imagePanelItemPrefab;

        private List<GameObject> images = new List<GameObject>();
        
        public void CreateImage(ImageData image) {
            var item = Instantiate(imagePanelItemPrefab, transform.position, Quaternion.identity);
            item.transform.SetParent(transform);
            var controller = item.GetComponent<ImagePanelItemController>();
            controller.image.preserveAspect = true;
            controller.image.sprite = image.sprite;
            var itemRect = item.GetComponent<RectTransform>();
            itemRect.sizeDelta = new Vector2(image.size.First, image.size.Second);
            itemRect.localScale = Vector3.one;
            images.Add(item);
        }

        public void SetMultipleCellSize(ImageData imageData) {
            StartCoroutine(SetCellSize(width => {
                width = GetWidthByRatio(width, 8);
                var height = imageData.GetResizedHeight(width);
                grid.cellSize = new Vector2(width, height);
            }));
        }

        public void SetCellSize(List<ImageData> imageDatas, Action onSucess) {
            StartCoroutine(SetCellSize(width => {
                Vector2 cellSize;
                if (imageDatas.Count == 1) {
                    var image = imageDatas.First();
                    var size = image.size;
                    if (size.First > size.Second) {
                        var percent = (size.Second * 1f / size.First * 1f) * 100;
                        width = GetWidthByRatio(width, percent <= 50 ? 6 : 4);
                        cellSize = new Vector2(width, image.GetResizedHeight(width));
                    } else {
                        width = GetWidthByRatio(width, 4);
                        cellSize = new Vector2(image.GetResizedWidth(width), width);
                    }
                } else {
                    width = GetWidthByRatio(width, 7);
                    var height = imageDatas.Select(image => image.GetResizedHeight(width / 2)).Min();
                    cellSize = new Vector2(width / 2, height);
                }

                grid.cellSize = cellSize;
                onSucess();
            }));
        }

        private float GetWidthByRatio(float width, int ratio) => width / 10 * ratio;
    }
}