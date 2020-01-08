using System.Text;
using UnityEngine;

namespace ProblemViewer.Scripts.Utils {
    public static class ProblemHelper {
        public static void Visible(this CanvasGroup canvasGroup, bool value) {
            canvasGroup.alpha = value ? 1 : 0;
            canvasGroup.interactable = value;
            canvasGroup.blocksRaycasts = value;
        }
    }
}