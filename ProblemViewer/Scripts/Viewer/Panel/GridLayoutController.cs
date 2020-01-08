using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace ProblemViewer.Scripts.Viewer.Panel {
    public class GridLayoutController : MonoBehaviour {
        [SerializeField] protected GridLayoutGroup grid;
        [SerializeField] protected RectTransform rect;
        
        protected IEnumerator SetCellSize(Action<float> onSuccess) {
            while (rect.rect.width < 50) yield return null;
            var originWidth = rect.rect.width;
            var padding = grid.padding;
            var spacing = grid.spacing;
            var spaceW = (padding.left + padding.right) + (spacing.x * 2);
            float maxWidth = originWidth - spaceW;
            var width = Mathf.Min(originWidth - (padding.left + padding.right) - (spacing.x * 2), maxWidth);
            onSuccess(width);
        }
    }
}