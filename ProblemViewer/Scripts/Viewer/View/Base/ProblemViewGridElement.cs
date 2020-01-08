using ProblemViewer.Scripts.Viewer.Panel;
using UnityEngine;

namespace ProblemViewer.Scripts.Viewer.View.Base {
    public class ProblemViewGridElement : ProblemViewElement {
        [SerializeField] protected GridPanelController gridPanelController;
        [SerializeField] protected GameObject gridPanel;
    }
}